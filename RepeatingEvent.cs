using nCroner.Common.Attributes;
using nCroner.Common.Events;
using nCroner.Common.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nCroner.Clock
{
    public class RepeatingEvent : IEvent
    {
        private static readonly ConcurrentDictionary<Guid, DateTime> RunTimes =
            new ConcurrentDictionary<Guid, DateTime>();

        public void Dispose()
        {
        }

        [Input("Interval", "Run time interval")]
        public Task<EventResponse> DoWork(Guid id, IDictionary<string, object> input)
        {
            var time = RunTimes.GetOrAdd(id, DateTime.Now);

            var inv = Convert.ToInt32(input["Interval"]);

            if (((DateTime.Now - time).TotalSeconds / 60) < inv)
            {
                return Task.FromResult(new EventResponse()
                {
                    Continue = false
                });
            }

            RunTimes[id] = DateTime.Now;

            return Task.FromResult(new EventResponse()
            {
                Continue = true
            });
        }
    }
}