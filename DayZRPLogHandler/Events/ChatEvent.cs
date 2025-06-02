using System;

namespace DayZRPLogHandler.Events {
    public class ChatEvent: Event {
        private readonly string _message;
        public ChatEvent(DateTime time, Player player, string message) : base(time, player) {
            _message = message;
        }

        public override string ToString() {
            return $"{base.ToString()}: {_message}";
        }
    }
}