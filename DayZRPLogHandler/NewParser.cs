using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
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
                if (line.EndsWith("connected")) {
                    var player = new Player(match.Groups[1].Value, "");
                    _events.Add(new ConnectionEvent(time, player, !line.Contains("disconnected")));
                } else if (line.Contains("placed")) {
                    var regex = new Regex(@"Player ""(?<name>[^""]+)"" \(id=(?<id>\d+) pos=<(?<pos>[\d.,\s]+)>\) placed (?<itemLabel>.*?)<(?<itemInternal>[^>]+)>");
                    var name = regex.Match(line).Groups["name"].Value;
                    var id = regex.Match(line).Groups["id"].Value;
                    var player = new Player(name, id);
                    var pos = regex.Match(line).Groups["pos"].Value;
                    var itemLabel = regex.Match(line).Groups["itemLabel"].Value;
                    var itemInternal = regex.Match(line).Groups["itemInternal"].Value;
                    var position = Utility.ToVector3(pos);

                    _events.Add(new PlacementEvent(time, player, position, itemLabel));
                }
            }

            foreach (var e in _events) {Console.WriteLine(e);}
        }
    }
}