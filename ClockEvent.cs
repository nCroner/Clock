using nCroner.Common.Events;
using System;

namespace nCroner.Clock
{
    public class ClockEvent : EventCollection
    {
        public override Guid Id => Guid.Parse("6a7bf411-3b75-4c32-9350-4b860a0df611");
        public override string Title => "Clock";
        public override int Interval => 60;

        public ClockEvent()
        {
            AddEvent<OnTimeEvent>(
                Guid.Parse("6721b5e4-31f3-41c2-acde-56b3c2b2c203"),
                "Run an event at a specified time");

            AddEvent<RepeatingEvent>(
                Guid.Parse("081ee164-1b19-4a61-af01-c71ec30de268"),
                "Run at a recurring time interval");
        }
    }
}