using SQLite;

namespace FinalProject {
    /// <summary>Represents a member of a gym.</summary>
    public class GymMember {
        // ReSharper disable once InconsistentNaming
        /// <summary>The member's ID</summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>The member's username.</summary>
        public string Username { get; set; }

        /// <summary>The member's email.</summary>
        public string Email { get; init; }

        /// <summary>The member's password.</summary>
        public string Password { get; init; }
    }
}
