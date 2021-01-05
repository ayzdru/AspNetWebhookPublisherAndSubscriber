using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookSubscription
    {
        public string WebhookUri { get; set; }
        public string Secret { get; set; }
        public bool IsActive { get; set; }
        public List<string> Webhooks { get; set; }
        public IDictionary<string, string> Headers { get; set; }

        public WebhookSubscription()
        {
            IsActive = true;
            Headers = new Dictionary<string, string>();
            Webhooks = new List<string>();
        }
    }
}
