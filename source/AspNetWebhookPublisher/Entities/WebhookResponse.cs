using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookResponse : BaseEntity
    {       
        public Guid WebhookPayloadId { get; set; }
        public WebhookPayload WebhookPayload { get; set; }
        public string Data { get; set; }
        public int? HttpStatusCode { get; set; }       
    }
}
