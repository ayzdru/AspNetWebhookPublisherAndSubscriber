﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookPayload : BaseEntity
    {
        public Guid WebhookEventId { get; set; }
        public WebhookEvent WebhookEvent { get; set; }
        public int Attempt { get; set; }
        public string Data { get; set; }
    }
}
