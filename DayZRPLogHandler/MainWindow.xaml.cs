using System.Linq;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace DayZRPLogHandler {
    public partial class MainWindow : Window {
        private Parser _parser = new();
        private readonly TextBox[] _logs;

        public MainWindow() {
            InitializeComponent();
            _logs = new[] { KillLogs, HitLogs, ChatLogs, BuildingLogs, PositionLogs, ConnectionLogs };
        }

        private void UploadButtonClick(object sender, RoutedEventArgs e) {
            _parser = new();
            var upload = new OpenFileDialog { Filter = "DayZ admin log files (*.ADM)|*.ADM;|Text files (*.txt)|*.txt" };
            if (upload.ShowDialog() != true) return;
            FilePathBox.Text = upload.FileName;
            _parser.Parse(upload.FileName);

            foreach (var log in _logs) {
                log.Text = "";
            }

            KillLogs.Text = string.Join("\n", _parser.Kills);
            HitLogs.Text = string.Join("\n", _parser.Hits);
            ChatLogs.Text = string.Join("\n", _parser.Chats);
            BuildingLogs.Text = string.Join("\n", _parser.Building);
            PositionLogs.Text = string.Join("\n", _parser.Position);
            ConnectionLogs.Text = string.Join("\n", _parser.Connection);

            IDBox.IsEnabled = true;
            PosBox.IsEnabled = true;
            SearchButton.IsEnabled = true;
            SearchText.IsEnabled = true;
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e) {
            Search(SearchText.Text);
        }

        private void Search(string text) {
            SetToDefault();
            if (IDBox.IsChecked == true) {
                HideIDs();
            }
            else if (PosBox.IsChecked == true) {
                HidePos();
            }

            foreach (var log in _logs) {
                if (log.Equals(ChatLogs)) {
                    // Explicit exception to the double-space removal for chat logs.
                    log.Text = string.Join("\n", log.Text.Split('\n').Where(s => s.ToLower().Contains(text.ToLower())));
                }

                log.Text = string.Join("\n", log.Text.Split('\n').Where(s => s.ToLower().Contains(text.ToLower())))
                    .Replace("  ", " ");
            }
        }

        private void HideIDs(object sender, RoutedEventArgs routedEventArgs) {
            HideIDs();
            Search(SearchText.Text);
        }

        private void HideIDs() {
            if (PosBox.IsChecked == true) {
                NoIDNoPos();
            }
            else {
                NoID();
            }
        }

        private void UnhideIDs(object sender, RoutedEventArgs e) {
            UnhideIDs();
            Search(SearchText.Text);
        }

        private void UnhideIDs() {
            if (PosBox.IsChecked == true) {
                NoPos();
            }
            else {
                SetToDefault();
            }
        }

        private void HidePos(object sender, RoutedEventArgs e) {
            HidePos();
            Search(SearchText.Text);
        }

        private void HidePos() {
            if (IDBox.IsChecked == true) {
                NoIDNoPos();
            }
            else {
                NoPos();
            }
        }

        private void UnhidePos(object sender, RoutedEventArgs e) {
            UnhidePos();
            Search(SearchText.Text);
        }

        private void UnhidePos() {
            if (IDBox.IsChecked == true) {
                NoID();
            }
            else {
                SetToDefault();
            }
        }

        private void SetToDefault() {
            KillLogs.Text = string.Join("\n", _parser.Kills);
            HitLogs.Text = string.Join("\n", _parser.Hits);
            ChatLogs.Text = string.Join("\n", _parser.Chats);
            BuildingLogs.Text = string.Join("\n", _parser.Building);
            PositionLogs.Text = string.Join("\n", _parser.Position);
            ConnectionLogs.Text = string.Join("\n", _parser.Connection);
        }

        private void NoID() {
            KillLogs.Text = string.Join("\n", _parser.KillsNoID);
            HitLogs.Text = string.Join("\n", _parser.HitsNoID);
            ChatLogs.Text = string.Join("\n", _parser.ChatsNoID);
            BuildingLogs.Text = string.Join("\n", _parser.BuildingNoID);
            PositionLogs.Text = string.Join("\n", _parser.PositionNoID);
            ConnectionLogs.Text = string.Join("\n", _parser.ConnectionNoID);
        }

        private void NoIDNoPos() {
            KillLogs.Text = string.Join("\n", _parser.KillsNoIDNoPos);
            HitLogs.Text = string.Join("\n", _parser.HitsNoIDNoPos);
            BuildingLogs.Text = string.Join("\n", _parser.BuildingNoIDNoPos);
        }

        private void NoPos() {
            KillLogs.Text = string.Join("\n", _parser.KillsNoPos);
            HitLogs.Text = string.Join("\n", _parser.HitsNoPos);
            BuildingLogs.Text = string.Join("\n", _parser.BuildingNoPos);
        }
    }
}
