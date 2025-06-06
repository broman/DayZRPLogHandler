using System.Numerics;

namespace DayZRPLogHandler {
    public static class Utility {
        public static Vector3 ToVector3(string s) {
            var parts = s.Split(',');
            return new Vector3(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2])
            );
        }
    }
}