using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.Its.Domain
{
    [DebuggerStepThrough]
    internal class AnonymousCommandScheduler<TAggregate> : ICommandScheduler<TAggregate> where TAggregate : IEventSourced
    {
        private readonly Func<IScheduledCommand<TAggregate>, Task> schedule;
        private readonly Func<IScheduledCommand<TAggregate>, Task> deliver;

        public AnonymousCommandScheduler(Func<IScheduledCommand<TAggregate>, Task> schedule, Func<IScheduledCommand<TAggregate>, Task> deliver)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException("schedule");
            }
            if (deliver == null)
            {
                throw new ArgumentNullException("deliver");
            }
            this.schedule = schedule;
            this.deliver = deliver;
        }

        public async Task Schedule(IScheduledCommand<TAggregate> scheduledCommand)
        {
            await schedule(scheduledCommand);
        }

        public async Task Deliver(IScheduledCommand<TAggregate> scheduledCommand)
        {
            await deliver(scheduledCommand);
        }
    }
}