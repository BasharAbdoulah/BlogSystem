namespace BlogSystem.AppModel
{
    public class UserModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Admin { get; set; }
        public string UserMessage { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
