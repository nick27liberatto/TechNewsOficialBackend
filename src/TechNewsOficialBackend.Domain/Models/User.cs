namespace TechNewsOficialBackend.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }

    }
}
