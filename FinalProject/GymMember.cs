namespace FinalProject {
    /// <summary>Represents a member of a gym.</summary>
    public class GymMember {
        // ReSharper disable once InconsistentNaming
        /// <summary>The member's ID</summary>
        public string ID { get; init; }

        /// <summary>The member's username.</summary>
        public string Username { get; init; }

        /// <summary>The member's password.</summary>
        public string Password { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Address { get; init; }

        public string City { get; init; }

        public string Region { get; init; }

        public string PostalCode { get; init; }

        public string Country { get; init; }

        public string PhoneNumber { get; init; }

        /// <summary>The member's email.</summary>
        public string Email { get; init; }

        public string Gender { get; init; }

        public DateTime DateOfBirth { get; init; }

        public override string ToString()
            => $"User's Id: {ID} User Name: {Username} Name: {FirstName} {LastName} "
                + $"Gender: {Gender}\nDate of Birth (YYYY/MM/DD): {DateOfBirth}"
                + $"\nContact: {Address}, {City}, {Region} {PostalCode} {Country}"
                + $" - {PhoneNumber} - {Email}";
    }
}
