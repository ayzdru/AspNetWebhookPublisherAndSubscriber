using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookEvent
    {
        public string WebhookName { get; set; }
        public string Data { get; set; }
    }
}
