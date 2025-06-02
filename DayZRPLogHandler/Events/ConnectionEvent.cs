using System;

namespace DayZRPLogHandler.Events {
    public class ConnectionEvent: Event {
        private readonly bool _connected;
        public ConnectionEvent(DateTime time, Player player, bool connected): base(time, player) {
            _connected = connected;
        }

        public override string ToString() {
            return $"{base.ToString()}{(_connected ? "is connected" : "has been disconnected")}";
        }
    }
}