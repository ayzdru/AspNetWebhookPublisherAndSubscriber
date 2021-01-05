using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Entities
{
    public class WebhookDefinition
    {
        public string Name { get; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public WebhookDefinition(string name, string displayName = null, string description = null)
        {
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            DisplayName = displayName;
            Description = description;
        }
    }
}
