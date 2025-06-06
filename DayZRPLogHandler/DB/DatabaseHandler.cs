using System.Data.SQLite;

namespace DayZRPLogHandler.DB {
    public class DatabaseHandler {
        public DatabaseHandler() {
            const string file = "Data Source=logs.db";
            
            // Placement events
            using (var connection = new SQLiteConnection(file)) {
                connection.Open();
                const string qry = @"
                    CREATE TABLE IF NOT EXISTS PlacementEvents(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        PlayerId INTEGER NOT NULL,
                        PosX REAL,
                        PosY REAL,
                        PosZ REAL,
                        Item TEXT
                    );
                ";
                using (var command = new SQLiteCommand(qry, connection)) {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            
            // Connection events
            using (var connection = new SQLiteConnection(file)) {
                connection.Open();
                const string qry = @"
                    CREATE TABLE IF NOT EXISTS ConnectionEvents(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        PlayerId INTEGER NOT NULL,
                        Connected BOOLEAN NOT NULL
                    );
                ";
                using (var command = new SQLiteCommand(qry, connection)) {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}