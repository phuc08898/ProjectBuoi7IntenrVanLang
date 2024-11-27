namespace ProjectBuoi7.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // In a real application, passwords should be hashed
        public string Role { get; set; } // User role for access control
    }
}
