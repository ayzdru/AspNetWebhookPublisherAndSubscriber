using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookSubscriptionAllowedEvent : BaseEntity
    {
        public Guid WebhookSubscriptionId { get; set; }
        public WebhookSubscription WebhookSubscription { get; set; }
        public Guid WebhookEventId { get; set; }
        public WebhookEvent WebhookEvent { get; set; }
    }
}
