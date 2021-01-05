using AspNetWebhookPublisher.Data;
using AspNetWebhookPublisher.Entities;
using AspNetWebhookPublisher.Interfaces;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebhookPublisher _webhookPublisher;
        private readonly ApplicationDbContext _applicationDbContext;
        public IndexModel(ILogger<IndexModel> logger, IWebhookPublisher webhookPublisher, ApplicationDbContext applicationDbContext)
        {
            _webhookPublisher = webhookPublisher;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task<IActionResult>  OnGetAsync()
        {
            var personEntity = new Faker<Entities.Person>()
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                .RuleFor(u => u.Id, (f, u) => Guid.NewGuid())
                 .RuleFor(u => u.Created, (f, u) => DateTime.Now)
                .Generate();
            _applicationDbContext.Persons.Add(personEntity);
            _applicationDbContext.SaveChanges();
            await _webhookPublisher.Publish(Enums.WebHookEvents.PersonCreated, personEntity);
            return Page();
        }
    }
}
