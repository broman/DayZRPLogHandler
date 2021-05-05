/*
 * Parser.cs
 * Created by Ryan Broman on 2021-04-24
 * ryan@broman.dev
 */

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DayZRPLogHandler {
    public class Parser {
        public ObservableCollection<string> Kills { get; } = new();
        public ObservableCollection<string> Hits { get; } = new();
        public ObservableCollection<string> Chats { get; } = new();
        public ObservableCollection<string> Building { get; } = new();
        public ObservableCollection<string> Position { get; } = new();
        public ObservableCollection<string> Connection { get; } = new();

        public ObservableCollection<string> KillsNoID { get; } = new();
        public ObservableCollection<string> HitsNoID { get; } = new();
        public ObservableCollection<string> ChatsNoID { get; } = new();
        public ObservableCollection<string> BuildingNoID { get; } = new();
        public ObservableCollection<string> PositionNoID { get; } = new();
        public ObservableCollection<string> ConnectionNoID { get; } = new();

        public ObservableCollection<string> KillsNoPos { get; } = new();
        public ObservableCollection<string> HitsNoPos { get; } = new();
        public ObservableCollection<string> BuildingNoPos { get; } = new();

        public ObservableCollection<string> KillsNoIDNoPos { get; } = new();
        public ObservableCollection<string> HitsNoIDNoPos { get; } = new();
        public ObservableCollection<string> BuildingNoIDNoPos { get; } = new();

        public void Parse(string fileName) {
            var fileText = File.ReadAllLines(fileName);
            foreach(var line in fileText) {
                if(string.IsNullOrEmpty(line)) continue;
                if(line.Contains("Chat")) {
                    Chats.Add(line);
                    var noID = RemoveID(line);
                    ChatsNoID.Add(noID);
                }
                else if(line.Contains("connected") || line.Contains("disconnected")) {
                    Connection.Add(line);
                    var noID = RemoveID(line);
                    ConnectionNoID.Add(noID);
                }
                else if(line.Contains("killed") || line.Contains("died") || line.Contains("suicide")) {
                    Kills.Add(line);
                    var noID = RemoveID(line);
                    var noPos = RemovePos(line);
                    KillsNoID.Add(noID);
                    KillsNoPos.Add(noPos);
                    KillsNoIDNoPos.Add(RemovePos(noID).Replace("()", ""));
                }
                else if(line.Contains("hit by") || line.Contains("conscious")) {
                    Hits.Add(line);
                    var noID = RemoveID(line);
                    var noPos = RemovePos(line);
                    HitsNoID.Add(noID);
                    HitsNoPos.Add(noPos);
                    HitsNoIDNoPos.Add(RemovePos(noID).Replace("()", ""));
                }
                else if(line.Contains("built") || line.Contains("placed") || line.Contains("deployed") ||
                        line.Contains("packed")) {
                    Building.Add(line);
                    var noID = RemoveID(line);
                    var noPos = RemovePos(line);
                    BuildingNoID.Add(noID);
                    BuildingNoPos.Add(noPos);
                    BuildingNoIDNoPos.Add(RemovePos(noID).Replace("()", ""));
                }
                else if(line.Contains("Player ") || line.Contains("####")) {
                    Position.Add(line);
                    PositionNoID.Add(RemoveID(line));
                }
            }
        }

        private static string RemoveID(string input) {
            Regex idRgx = new(@"\(id=[^) ]*\)|id=[^) ]* ");
            var matches = idRgx.Matches(input);
            return matches.Cast<Match>()
                .Aggregate(input, (current, match) => current.Replace(match.Value, "").Replace("  ", " "));
        }

        private static string RemovePos(string input) {
            Regex posRgx = new(@"pos=<[^>]*>");
            var matches = posRgx.Matches(input);
            return matches.Cast<Match>()
                .Aggregate(input, (current, match) => current.Replace(match.Value, "").Replace("  ", " "));
        }
    }
}