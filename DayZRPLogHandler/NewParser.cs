using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DayZRPLogHandler.Events;

namespace DayZRPLogHandler {
    public class NewParser: Parser {
        private List<Event> _events = new();
        
        public new void Parse(string fileName) {
            var fileText = File.ReadAllLines(fileName);
            foreach(var line in fileText) {
                if (string.IsNullOrEmpty(line)) continue;
                DateTime time;
                try {
                    time = DateTime.Parse(line.Substring(0, 8));
                }
                catch (FormatException) {
                    continue;
                }

                var playerRegex = new Regex("\"([^\"]+)\"");
                var match = playerRegex.Match(line);
                string id="";
                if (line.EndsWith("connected")) {
                    _events.Add(new ConnectionEvent(time, new Player(match.Groups[1].Value, id), !line.Contains("disconnected")));
                }
            }

            foreach (var e in _events) {Console.WriteLine(e);}
        }
    }
}