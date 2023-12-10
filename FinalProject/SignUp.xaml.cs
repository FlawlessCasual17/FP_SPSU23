using System.Text;
using System.Text.RegularExpressions;

namespace FinalProject {
    /// <summary>Adds functionality to the related XAML page.</summary>
    public partial class SignUp {
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Generates a randomized alphanumeric word of 5 characters.
        /// </summary>
        /// <returns>
        /// A randomized 5 character alphanumeric ID of 5 characters.
        /// </returns>
        private static string GenAlphanumericID() {
            // Create a new instance of `Random`.
            var rnd = new Random();
            // Creates a single random character.
            var rndChars = string.Concat(Enumerable
                    .Range('A', 26).Select(c => (char)c))
                + string.Concat(Enumerable
                    .Range('a', 26).Select(c => (char)c))
                + string.Concat(Enumerable
                    .Range('0', 10).Select(c => (char)c));
            // Creates a new instance of `StringBuilder`.
            var result = new StringBuilder(5);

            /*
             * Appends a random character to `result` until
             * it's value reaches a maximum of 5 characters.
             */
            for (var i = 0; i < 5; i++)
                result.Append(rndChars[rnd.Next(rndChars.Length)]);

            // Return a randomized alphanumeric ID.
            return result.ToString();
        }

        public SignUp() => InitializeComponent();

        /// <summary>
        /// A method that executes when the
        /// <see cref="Button"/> in the
        /// relevant XAML page is clicked.
        /// </summary>
        private async void OnClickedSignUp(object sender, EventArgs ev) {
            if (!ValidateInput()) return;

            // Creates a path to the custom database.
            var dbPath = Path.Join(
                Environment.GetEnvironmentVariable("USERPROFILE"), "creds.db");
            // Creates a new database.
            var database = new GMDatabse(dbPath);

            // Holds the value of a randomized id.
            var id = GenAlphanumericID();
            // Holds the value of the inputted username.
            var userName = Username.Text;

            // Creates a new gym member.
            var member = new GymMember {
                ID = id, Username = userName,
                Password = Password.Text,
                FirstName = FirstName.Text, LastName = LastName.Text,
                Address = Address.Text, City = City.Text,
                Region = Region.Text,
                PostalCode = PostalCode.Text, Country = Country.Text,
                PhoneNumber = PhoneNumber.Text,
                Email = EmailEntry.Text, Gender = Gender.Text,
                DateOfBirth = Convert.ToDateTime(DateOfBirth.Text)
            };

            // Saves the user to the database.
            await database.SaveUserAsync(member);
            // Displays a success message to inform the user.
            await DisplayAlert(
                "Success",
                $"You have successfully registered"
                + $" your account, {userName}!\n"
                + $"Your user ID is {id} (Please keep this in safe place! "
                + $"You will need it for the user login confirmation.))",
                "OK");
        }

        private bool ValidateInput() =>
            ValidateRequiredField(Username, "Username") &&
            ValidateConfirmPassword(Password, ConfirmPassword) &&
            ValidateRequiredField(FirstName, "First Name") &&
            ValidateRequiredField(LastName, "Last Name") &&
            ValidateRequiredField(Address, "Address") &&
            ValidateRequiredField(City, "City") &&
            ValidateRequiredField(Region, "Region") &&
            ValidatePostalCode(PostalCode.Text) &&
            ValidateRequiredField(Country, "Country") &&
            ValidatePhoneNumber(PhoneNumber.Text) &&
            ValidateEmail(EmailEntry.Text) &&
            !ValidateDateOfBirth(DateOfBirth.Text);

        private bool ValidateRequiredField(InputView entry, string fieldName) {
            if (!string.IsNullOrEmpty(entry.Text)) return true;

            DisplayAlert(
                "Validation Error",
                $"{fieldName} is required.",
                "OK");
            return false;
        }

        private bool ValidatePhoneNumber(string phoneNumber) {
            // Pattern to match exactly 10 digits.
            if (Regex.IsMatch(phoneNumber, @"^\d{10}$")) return true;
            DisplayAlert(
                "Validation Error",
                "Invalid Phone Number format. Please enter a 10-digit phone number.",
                "OK");
            return false;
        }


        private bool ValidateDateOfBirth(string dateOfBirthText) {
            if (string.IsNullOrEmpty(dateOfBirthText)) {
                DisplayAlert(
                    "Validation Error",
                    "Date of Birth is required.",
                    "OK");
                return false;
            }

            if (!DateTime.TryParse(dateOfBirthText, out var dateOfBirth)) {
                DisplayAlert(
                    "Validation Error",
                    "Invalid Date of Birth. Please enter a valid date.",
                    "OK");
                return false;
            }

            // Additional check for a reasonable age (e.g., not in the future)
            if (dateOfBirth <= DateTime.Now) return true;
            DisplayAlert(
                "Validation Error",
                "Date of Birth cannot be in the future.",
                "OK");
            return false;

        }

        private bool ValidateEmail(string email) {
            const string emailPattern = @"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
            if (Regex.IsMatch(email, emailPattern)) return true;
            DisplayAlert(
                "Validation Error",
                "Invalid Email format. Please enter a valid email address.",
                "OK");
            return false;

        }

        private bool ValidateConfirmPassword(InputView passwordEntry, InputView confirmPasswordEntry) {
            var password = passwordEntry.Text;
            var confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrEmpty(confirmPassword)) {
                DisplayAlert(
                    "Validation Error",
                    "Confirm Password is required.",
                    "OK");
                return false;
            }

            if (password == confirmPassword) return true;
            DisplayAlert(
                "Validation Error",
                "Password and Confirm Password do not match.",
                "OK");
            return false;

        }

        private bool ValidatePostalCode(string postalCode) {
            // Canadian postal code pattern: "A1A 1A1" or "A1A1A1"
            const string postalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$|^[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d$";

            if (Regex.IsMatch(postalCode, postalCodePattern)) return true;
            DisplayAlert(
                "Validation Error",
                "Invalid Canadian Postal Code format. Please enter a valid postal code.",
                "OK");
            return false;

        }

        // private bool ValidateDateField(Entry entry, string fieldName) {
        //     if (string.IsNullOrEmpty(entry.Text)) {
        //         DisplayAlert("Validation Error", $"{fieldName} is required.", "OK");
        //         return false;
        //     }
        //
        //     if (!DateTime.TryParse(entry.Text, out var fieldValue)) {
        //         DisplayAlert("Validation Error", $"Invalid input for {fieldName}. Please enter a valid date.", "OK");
        //         return false;
        //     }
        //
        //     return true;
        // }

        // private bool ValidateBooleanField(Entry entry, string fieldName) {
        //     if (string.IsNullOrEmpty(entry.Text)) {
        //         DisplayAlert(
        //             "Validation Error",
        //             $"{fieldName} is required.",
        //             "OK");
        //         return false;
        //     }
        //
        //     if (bool.TryParse(entry.Text.ToLower(), out var fieldValue)) return true;
        //     DisplayAlert(
        //         "Validation Error",
        //         $"{fieldValue} is an invalid input for {fieldName}. Please enter True or False.",
        //         "OK");
        //     return false;
        // }
    }
}
