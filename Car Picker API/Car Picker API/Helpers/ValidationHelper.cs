namespace Car_Picker_API.Helpers
{
    public static class ValidationHelper
    {
        public static bool ISValidFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > 101)
                throw new Exception("Full Name is required and should not be more than 101 characters.");

            foreach (char c in fullName)
            {
                if (!char.IsLetter(c) && c != ' ')
                    throw new Exception("Full Name should contain only English letters and spaces.");
            }

            return true;
        }

        public static bool IsValidDateOfBirth(DateOnly birth)
        {
            if (birth == default)
                return false;

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (birth > today)
                return false;

            int age = today.Year - birth.Year;
            if (birth > today.AddYears(-age)) age--;

            if (age < 18)
                return false;

            return true;
        }


        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new Exception("Phone number is required.");

            if (phoneNumber.Length != 10 || !long.TryParse(phoneNumber, out _))
                throw new Exception("Phone number must be exactly 10 digits and contain only numbers.");

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email Is  Required");

            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');

            if (atIndex < 1 || dotIndex < atIndex + 2 || dotIndex >= email.Length - 2)
                throw new Exception("Email Is  Required");

            string domain = email.Substring(atIndex + 1, dotIndex - atIndex - 1);
            string extension = email.Substring(dotIndex + 1);

            if (domain.Length < 2 || extension.Length < 2)
                throw new Exception("Email Is  Required");

            foreach (char c in email.Substring(0, atIndex))
            {
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_' && c != '%' && c != '+' && c != '-')
                    throw new Exception("Email Is  Required");
            }
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) && password.Length >= 8)
                throw new Exception("Password Is Required");
            return true;
        }
    }
}
