namespace JwtFactory
{
    public class User
    {
        public User(string userID, string ascessKey)
        {
            UserID = userID;
            AccessKey = ascessKey;
        }

        public string UserID { get; set; }
        public string AccessKey { get; set; }
    }
}
