using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookSendAttempt
    {
        public Guid WebhookEventId { get; set; }
        public Guid WebhookSubscriptionId { get; set; }
        public string Response { get; set; }
        public int? ResponseStatusCode { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
