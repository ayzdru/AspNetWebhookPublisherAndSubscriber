using AspNetWebhookPublisher.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetWebhookPublisher.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
