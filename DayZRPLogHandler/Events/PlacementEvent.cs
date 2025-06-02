using System;
using System.Windows;

namespace DayZRPLogHandler.Events {
    public class PlacementEvent: Event {
        private readonly string _obj;

        public PlacementEvent(DateTime time, Player player, Vector location, string obj) : base(time, player, location) {
            _obj = obj;
        }

        public override string ToString() {
            return $"{base.ToString()} placed {_obj}";
        }
    }
}