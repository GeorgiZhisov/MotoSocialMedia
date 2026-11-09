namespace MotoGrid.Common.Constants;

/// <summary>
/// Length and range limits shared by the entities, the view models and the validation.
/// </summary>
public static class ValidationConstants
{
    public static class User
    {
        public const int DisplayNameMinLength = 2;
        public const int DisplayNameMaxLength = 50;
        public const int BioMaxLength = 500;
        public const int CityMaxLength = 80;
    }

    public static class Motorcycle
    {
        public const int MakeMaxLength = 50;
        public const int ModelMaxLength = 80;
        public const int NicknameMaxLength = 50;
        public const int DescriptionMaxLength = 2000;
        public const int MinYear = 1900;
        public const int MinEngineCc = 49;
        public const int MaxEngineCc = 3000;
    }

    public static class Post
    {
        public const int ContentMaxLength = 3000;
        public const int LocationMaxLength = 120;
        public const int CommentMaxLength = 1000;
    }

    public static class Group
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 80;
        public const int DescriptionMaxLength = 2000;
    }

    public static class Event
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 120;
        public const int DescriptionMaxLength = 3000;
        public const int LocationMaxLength = 200;
    }

    public static class Route
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 120;
        public const int DescriptionMaxLength = 3000;
        public const int PlaceMaxLength = 120;
        public const double MinDistanceKm = 1;
        public const double MaxDistanceKm = 5000;
    }

    public static class Listing
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 120;
        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 5000;
        public const int CityMaxLength = 80;
        public const int CategoryNameMaxLength = 60;
        public const int MaxMileageKm = 1_000_000;
        public const string MinPrice = "0.01";
        public const string MaxPrice = "10000000";
    }

    public static class Message
    {
        public const int ContentMaxLength = 2000;
    }

    public static class Review
    {
        public const int MinRating = 1;
        public const int MaxRating = 5;
        public const int CommentMaxLength = 1000;
    }

    public static class Media
    {
        public const int UrlMaxLength = 500;
        public const long MaxFileSizeBytes = 50 * 1024 * 1024; // 50 MB
    }
}
