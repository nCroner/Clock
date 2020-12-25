using nCroner.Common.Attributes;
using nCroner.Common.Events;
using nCroner.Common.Models;
using nCroner.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCroner.Clock
{
    public class OnTimeEvent : IEvent
    {
        public void Dispose()
        {
        }

        [Input("OnTime", "Clock (format: 16:30)")]
        [Input("TimeZone", "Run time based on time zone specified area (format: Pacific Standard Time)")]
        public Task<EventResponse> DoWork(Guid id, IDictionary<string, object> input)
        {
            var time = input["OnTime"].ToString()
                .Split(':')
                .Select(t => Convert.ToInt32(t));

            DateTime now;
            if (input.ContainsKey("TimeZone") && !string.IsNullOrWhiteSpace(input["TimeZone"].ToString()))
            {
                now = Commons.GetDateByTimeZone(input["TimeZone"].ToString());
            }
            else
            {
                now = DateTime.Now;
            }

            if (now.Hour == time.ElementAt(0) && now.Minute == time.ElementAt(1))
            {
                return Task.FromResult(new EventResponse()
                {
                    Continue = true
                });
            }

            return Task.FromResult(new EventResponse()
            {
                Continue = false
            });
        }

    }
}