namespace CleanCode.Initial.Services
{
    public class UserRatingCache
    {
        private static UserRatingCache instance;

        private UserRatingCache()
        {
        }

        public static UserRatingCache GetInstance()
        {
            if (instance == null)
                instance = new UserRatingCache();
            return instance;
        }

        public UserRatingEntry FindById(string id)
        {
            // Some implementation
            return null;
        }
    }

    public class UserRatingEntry
    {
        public int Rating { get; set; }
    }
}
