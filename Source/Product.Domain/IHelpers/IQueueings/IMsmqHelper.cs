namespace Product.Domain.IHelpers.IQueueings
{
    public interface IMsmqHelper
    {
        void Save(string queueName, object message);
    }
}
