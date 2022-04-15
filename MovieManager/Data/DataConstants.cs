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
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;
            public const int OverviewMinLength = 10;
            public const int OverviewMaxLength = 3000;
            public const int YearMinValue = 1890;
            public const int YearMaxValue = 2100;
        }

        public class Genre
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public class Review
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 150;
            public const int ContentMinLength = 20;
            public const int ContentMaxLength = 3000;
        }
        public class Playlist
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 150;

        }
        public class Actor
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;
            public const int OverviewMinLength = 10;
            public const int OverviewMaxLength = 3000;
            public const int YearMinValue = 1800;
            public const int YearMaxValue = 2100;
        }
    }
}
