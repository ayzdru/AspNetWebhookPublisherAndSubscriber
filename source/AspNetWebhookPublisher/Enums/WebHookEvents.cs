using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Enums
{
    public enum WebHookEvents
    {
        [Display(Name = "person.created")]
        PersonCreated,
        [Display(Name = "person.updated")]
        PersonUpdated,
        [Display(Name = "person.deleted")]
        PersonDeleted
    }
}
