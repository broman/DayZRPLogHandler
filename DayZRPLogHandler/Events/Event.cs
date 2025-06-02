using System;
using System.Windows;

namespace DayZRPLogHandler.Events {
    public abstract class Event {
        private readonly Player _player;
        private readonly EventTime _time;
        private readonly Vector? _location = null;

        protected Event(DateTime time, Player player, Vector? location = null) {
            _player = player;
            _time = new EventTime(time);
            _location = location;
        }

        public override string ToString() {
            return $"{_time} | {_player} {(_location != null ? $"({_location})" : "")}";
        }
    }
}