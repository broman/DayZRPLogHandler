using System;
using System.Numerics;
using System.Windows;

namespace DayZRPLogHandler.Events {
    public class PlacementEvent: Event {
        private readonly string _obj;

        public PlacementEvent(DateTime time, Player player, Vector3 location, string obj) : base(time, player, location) {
            _obj = obj;
        }

        public override string ToString() {
            return $"{base.ToString()} placed {_obj}";
        }
    }
}