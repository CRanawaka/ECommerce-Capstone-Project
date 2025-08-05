namespace ECommerce.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public string Role { get; set; } = "Customer"; // Default role is Customer

        public ShoppingCart? ShoppingCart { get; set; } // Navigation property for ShoppingCart

        // Additional properties and methods can be added here as needed
    }
}