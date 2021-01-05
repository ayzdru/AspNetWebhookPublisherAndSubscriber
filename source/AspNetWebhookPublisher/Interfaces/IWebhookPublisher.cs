using AspNetWebhookPublisher.Enums;
using System.Threading.Tasks;

namespace AspNetWebhookPublisher.Interfaces
{
    public interface IWebhookPublisher
    {
        Task Publish<T>(WebHookEvents webhookEvent, T data);
    }
}