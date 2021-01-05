using AspNetWebhookSubscriber.BindingModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWebhookSubscriber.Controllers
{
    public class TestController : ControllerBase
    {
        [HttpPost("webhook-form-data-test")]
        public IActionResult WebhookFormDataTest([FromForm]WebhookBindingModel webhookBindingModel)
        {
            return Ok();
        }
        [HttpPost("webhook-json-data-test")]
        public IActionResult WebhookJsonDataTest([FromBody] WebhookBindingModel webhookBindingModel)
        {
            return Ok();
        }
    }
}
