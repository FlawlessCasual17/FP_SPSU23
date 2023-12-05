using SQLite;

namespace FinalProject {
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A class for editing and creating
    /// databases related to Gym Management.
    /// 'GymManDB' is just short
    /// for 'GymManagementDatabase'.
    /// </summary>
    public class GymManDB {
        /// <summary>
        /// A global private readonly variable for use
        /// within the <see cref="GymManDB"/> class.
        /// </summary>
        private readonly SQLiteAsyncConnection database;

        /// <summary>
        /// A constructor for initializing a new
        /// instance of the <see cref="GymManDB"/> class.
        /// </summary>
        /// <param name="dbPath">
        /// The path to the database file (*.db/*.sqlite),
        /// can be an absolute or relative path.
        /// </param>
        public GymManDB(string dbPath = "") {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GymMember>().Wait();
        }

        /// <summary>
        /// Gets a user from the database based on
        /// their email and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>
        /// A <see cref="GymMember"/>
        /// <see cref="Task{TResult}"/> object.
        /// </returns>
        public async Task<GymMember> GetUserAsync(string email, string password)
            => await database.Table<GymMember>().Where(m => m.Email.Equals(email)
                && m.Password.Equals(password)).FirstOrDefaultAsync();
    }
}
