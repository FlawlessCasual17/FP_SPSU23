using SQLite;

namespace FinalProject {
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A class for editing and creating
    /// databases related to Gym Management.
    /// 'GMDatabse' is just short
    /// for 'GymManagementDatabase'.
    /// </summary>
    public class GMDatabse {
        /// <summary>
        /// A global private readonly variable for use
        /// within the <see cref="GMDatabse"/> class.
        /// </summary>
        private readonly SQLiteAsyncConnection database;

        /// <summary>
        /// A constructor for initializing a new
        /// instance of the <see cref="GMDatabse"/> class.
        /// </summary>
        /// <param name="dbPath">
        /// The path to the database file (*.db/*.sqlite),
        /// can be an absolute or relative path.
        /// </param>
        public GMDatabse(string dbPath) {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GymMember>().Wait();
        }

        /// <summary>Saves the user's information to the SQLite database.</summary>
        /// <param name="user">A member of a gym.</param>
        public Task<int> SaveUserAsync(GymMember user) => database.InsertAsync(user);

        /// <summary>
        /// Gets a user from the database based on
        /// their email and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>
        /// A <see cref="GymMember"/> object.
        /// </returns>
        public async Task<GymMember> GetUserAsync(string email, string password)
            // The `==` operator is safer and faster instead of using `object.Equals()`.
            => await database.Table<GymMember>().Where(bool (gm) =>
                    gm.Email == email
                    && gm.Password == password)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Checks if a user exists in the database
        /// based on their email and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>
        /// A <see cref="bool"/> object that indicates
        /// if the user exists in the database or not.
        /// </returns>
        public async Task<bool> CheckUserCredentials(string email, string password)
            // The `==` operator is safer and faster instead of using `object.Equals()`.
            => await database.Table<GymMember>().Where(gm =>
                    gm.Email == email
                    && gm.Password == password)
                .FirstOrDefaultAsync() is not null;
    }
}
