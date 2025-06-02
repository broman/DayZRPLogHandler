namespace DayZRPLogHandler.Events {
    public class Player {
        public readonly string Name;
        public readonly string Id;

        public Player(string name, string id) {
            Name = name;
            Id = id;
        }

        public override string ToString() {
            return Name;
        }
    }
}