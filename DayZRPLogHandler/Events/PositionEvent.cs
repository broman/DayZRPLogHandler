using System;
using System.Numerics;
using System.Windows;

namespace DayZRPLogHandler.Events {
    public class PositionEvent: Event {
        public PositionEvent(DateTime time, Player player, Vector3? location = null) : base(time, player, location) {
        }
    }
}