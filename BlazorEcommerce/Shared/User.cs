namespace BlazorEcommerce.Shared;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public DateTime DateCreated { get; set; } = DateTime.Now;



    
}