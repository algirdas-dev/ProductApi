using Product.Domain.IHelpers.IQueueings;

namespace Product.Infrastructure.Services
{
    public abstract class BaseService
    {
        protected readonly IMsmqHelper Msmq;
        protected BaseService(IMsmqHelper msmq) {
            Msmq = msmq;
        }
    }
}
