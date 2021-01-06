using Experimental.System.Messaging;
using Product.Domain.IHelpers.IQueueings;

namespace Product.Helpers.Queueings
{
    public class MsmqHelper : IMsmqHelper
    {
        private readonly MessageQueue _queue;
        public MsmqHelper(MessageQueue queue) {
            _queue = queue;
        }
        public void Save(string queueName,object message) {
            _queue.Path = string.Concat(@".\private$\", queueName);
            if (!MessageQueue.Exists(_queue.Path))
                MessageQueue.Create(_queue.Path);

            Message msg = new Message();
            msg.Body = message;
            _queue.Send(msg);
        }
    }
}
