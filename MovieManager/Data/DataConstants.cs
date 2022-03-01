namespace MovieManager.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public class User
        {
            public const int UsernameMinLength = 4;
            public const int UsernameMaxLength = 32;
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
            public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        }

        public class Movie
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int YearMinValue = 2000;
            public const int YearMaxValue = 2050;
        }

        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Dealer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
