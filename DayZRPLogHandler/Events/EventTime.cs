using System;

namespace DayZRPLogHandler.Events {
    public class EventTime {
        private readonly DateTime _time;

        public EventTime(DateTime time) {
            this._time = time;
        }

        public override string ToString() {
            return _time.ToString("HH:mm:ss");
        }
    }
}