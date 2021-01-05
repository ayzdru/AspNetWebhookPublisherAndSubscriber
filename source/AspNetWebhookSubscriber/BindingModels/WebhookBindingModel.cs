using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookSubscriber.BindingModels
{
    public class WebhookBindingModel
    {
        public string Data { get; set; }
        public string Event { get; set; }
    }
}
