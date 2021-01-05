using AspNetWebhookPublisher.Data;
using AspNetWebhookPublisher.Entities;
using AspNetWebhookPublisher.Enums;
using AspNetWebhookPublisher.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Services
{
    public class WebhookPublisher : IWebhookPublisher
    {
        protected const string SignatureHeaderKey = "sha256";
        protected const string SignatureHeaderValueTemplate = SignatureHeaderKey + "={0}";
        protected const string SignatureHeaderName = "webhook-signature";

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly HttpClient _httpClient;
        public WebhookPublisher(HttpClient httpClient, ApplicationDbContext applicationDbContext)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _applicationDbContext = applicationDbContext;
        }
        private string GetDisplayName(WebHookEvents webHookEvent)
        {
            var displayAttribute = typeof(WebHookEvents).GetMember(webHookEvent.ToString())[0].GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.Name))
            {
                return displayAttribute.Name;
            }
            return null;
        }
        public async Task Publish<T>(WebHookEvents webhookEvent, T data)
        {
            try
            {
                var webhookEventName = GetDisplayName(webhookEvent);
                var dataJson = JsonConvert.SerializeObject(data);
                var webhookSubscriptions = _applicationDbContext.WebhookSubscriptions.Include(q=> q.WebhookSubscriptionContentType).Where(q => q.IsActive == true && q.WebhookSubscriptionType.Name == WebhookSubscriptionTypes.All.ToString() || (q.WebhookSubscriptionAllowedEvents.Where(q => q.WebhookEvent.Name == webhookEventName).Any() == true)).ToList();
                foreach (var webhookSubscription in webhookSubscriptions)
                {
                    var webhookEventEntity = _applicationDbContext.WebhookEvents.Where(q => q.Name == webhookEventName).SingleOrDefault();
                    if (webhookEventEntity != null)
                    {
                        var webhookPayloadEntity = new WebhookPayload() { WebhookEventId = webhookEventEntity.Id, Data = dataJson, Attempt = 0, Created = DateTime.Now };
                        _applicationDbContext.WebhookPayloads.Add(webhookPayloadEntity);
                        _applicationDbContext.SaveChanges();

                        HttpContent httpContent = null;
                        if (webhookSubscription.WebhookSubscriptionContentType.Name == "application/json")
                        {
                            httpContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                        }
                        else
                        {
                            var formData = new Dictionary<string, string> { { "Data", dataJson }, { "Event", webhookEventName } };
                            httpContent = new FormUrlEncodedContent(formData);
                        }

                        var secretBytes = Encoding.UTF8.GetBytes(webhookSubscription.Secret);

                        using (var hasher = new HMACSHA256(secretBytes))
                        {
                            var hashData = Encoding.UTF8.GetBytes(dataJson);
                            var sha256 = hasher.ComputeHash(hashData);
                            var headerValue = string.Format(CultureInfo.InvariantCulture, SignatureHeaderValueTemplate, BitConverter.ToString(sha256));
                            httpContent.Headers.Add(SignatureHeaderName, headerValue);
                        }
                        var maxRetryAttempts = 3;
                        var pauseBetweenFailures = TimeSpan.FromSeconds(2);
                        var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
                        System.Diagnostics.Debug.WriteLine("STARTING...");
                        var retryPolicy = Policy
                            .Handle<Exception>()
                            .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures,
  onRetry: (response, delay, retryCount, context) =>
  {
      System.Diagnostics.Debug.WriteLine("RETRYING... - " + retryCount);
  });

                        await retryPolicy.WrapAsync(circuitBreakerPolicy).ExecuteAsync(async () =>
                        {
                            var response = await _httpClient.PostAsync(webhookSubscription.PayloadUrl, httpContent);
                            _applicationDbContext.WebhookResponses.Add(new WebhookResponse() { Data = await response.Content.ReadAsStringAsync(), HttpStatusCode = (int)response.StatusCode, WebhookPayloadId = webhookPayloadEntity.Id });
                            _applicationDbContext.SaveChanges();
                            if (!response.IsSuccessStatusCode)
                            {
                                var webhookPayload = _applicationDbContext.WebhookPayloads.Where(q => q.Id == webhookPayloadEntity.Id).SingleOrDefault();
                                if (webhookPayloadEntity != null)
                                {
                                    webhookPayload.Attempt += 1;
                                    _applicationDbContext.WebhookPayloads.Update(webhookPayload);
                                    _applicationDbContext.SaveChanges();
                                }
                            }
                            response.EnsureSuccessStatusCode();
                        });
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {

              
            }
           
        }
    }
}
