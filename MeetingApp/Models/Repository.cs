namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users = new List<UserInfo>();
        static Repository()
        {
            _users.Add(new UserInfo { Id = 1, Name = "John Doe", Phone = "123-456-7890", Email = "abc@gmail.com", WillAttend = false });
            _users.Add(new UserInfo { Id = 2, Name = "Peter Parker", Phone = "123-456-7890", Email = "abc@gmail.com", WillAttend = true });
            _users.Add(new UserInfo { Id = 3, Name = "Tony Stark", Phone = "123-456-7890", Email = "abc@gmail.com", WillAttend = true });
        }
        public static List<UserInfo> Users
        {
            get
            {
                return _users;
            }

        }
        public static void CreateUser(UserInfo user)
        {
            _users.Add(user);
        }
        public static UserInfo GetbyId(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}