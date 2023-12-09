using Microsoft.Data.Sqlite;

namespace FinalProjectTakeSix {
    public class DbService {
        private static DbService instance;

        private static SqliteConnection dbCon;

        private DbService() => InitializeDatabase();

        public static DbService Instance => instance ??= new DbService();

        private void InitializeDatabase() {
            var databasePath = Path.Combine(Environment
                .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gym.db");
            dbCon = new SqliteConnection($"Data Source={databasePath}");
            dbCon.Open();

            // Create the Member table if it doesn't exist
            var createTableCmd = new SqliteCommand("CREATE TABLE IF NOT EXISTS Member "
                + "(UserId INTEGER PRIMARY KEY, UserName TEXT, Password TEXT, "
                + "FirstName TEXT, LastName TEXT, Address TEXT, City TEXT, Region "
                + "TEXT, PostalCode TEXT, Country TEXT, PhoneNumber TEXT, Email TEXT, "
                + "Gender TEXT, DateOfBirth DATETIME, MembershipType TEXT, HealthInformation TEXT, "
                + "FitnessGoals TEXT, EmergencyContactName TEXT, EmergencyContactNumber TEXT, "
                + "PreferredWorkoutTime TEXT, TrainingSessionsPerWeek INTEGER, HasMedicalConditions "
                + "INTEGER, WantsPersonalTraining INTEGER, OptedInForNewsLetter INTEGER, "
                + "LastCheckInDateTime DATETIME)", dbCon);
            createTableCmd.ExecuteNonQuery();
        }

        public static void InsertMember(Member member) {
            var insertCommand = new SqliteCommand();

            foreach (var type in typeof(Member).GetProperties()) {
                var cmdText = $"INSERT INTO Member ({type.Name}) VALUES (@{type.Name})";
                insertCommand = new SqliteCommand(cmdText, dbCon);

                insertCommand.Parameters
                    .AddWithValue($"@{type.Name}", type.GetValue(member));
            }

            insertCommand.ExecuteNonQuery();


        }

        public void ClearMemberTable() {
            using (var clearTableCmd = new SqliteCommand("DELETE FROM Member", dbCon))
                clearTableCmd.ExecuteNonQuery();

            using (var resetSequenceCmd = new SqliteCommand(
                    "DELETE FROM sqlite_sequence WHERE name='Member'", dbCon))
                resetSequenceCmd.ExecuteNonQuery();
        }

        public Member GetMemberByUsernameAndPassword(string username, string password) {
            var selectCommand = new SqliteCommand("SELECT * FROM Member WHERE UserName = "
                + "@UserName AND Password = @Password", dbCon);
            selectCommand.Parameters.Add(new SqliteParameter("@UserName", username));
            selectCommand.Parameters.Add(new SqliteParameter("@Password", password));

            var reader = selectCommand.ExecuteReader();
            if (!reader.Read()) return null;
            var member = new Member(
                reader.GetInt32(reader.GetOrdinal("UserId")),
                reader.GetString(reader.GetOrdinal("UserName")),
                reader.GetString(reader.GetOrdinal("Password")),
                reader.GetString(reader.GetOrdinal("FirstName")),
                reader.GetString(reader.GetOrdinal("LastName")),
                reader.GetString(reader.GetOrdinal("Address")),
                reader.GetString(reader.GetOrdinal("City")),
                reader.GetString(reader.GetOrdinal("Region")),
                reader.GetString(reader.GetOrdinal("PostalCode")),
                reader.GetString(reader.GetOrdinal("Country")),
                reader.GetString(reader.GetOrdinal("PhoneNumber")),
                reader.GetString(reader.GetOrdinal("Email")),
                reader.GetString(reader.GetOrdinal("Gender")),
                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                reader.GetString(reader.GetOrdinal("MembershipType")),
                reader.GetString(reader.GetOrdinal("HealthInformation")),
                reader.GetString(reader.GetOrdinal("FitnessGoals")),
                reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                reader.GetString(reader.GetOrdinal("EmergencyContactNumber")),
                reader.GetString(reader.GetOrdinal("PreferredWorkoutTime")),
                reader.GetInt32(reader.GetOrdinal("TrainingSessionsPerWeek")),
                reader.GetInt32(reader.GetOrdinal("HasMedicalConditions")) == 1,
                reader.GetInt32(reader.GetOrdinal("WantsPersonalTraining")) == 1,
                reader.GetInt32(reader.GetOrdinal("OptedInForNewsLetter")) == 1,
                reader.GetDateTime(reader.GetOrdinal("LastCheckInDateTime")));

            return member;
        }

        public static SqliteConnection GetDatabaseConnection() => dbCon;
    }
}
