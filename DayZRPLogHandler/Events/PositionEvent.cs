using System;
using System.Windows;

namespace DayZRPLogHandler.Events {
    public class PositionEvent: Event {
        public PositionEvent(DateTime time, Player player, Vector? location = null) : base(time, player, location) {
        }
    }
}