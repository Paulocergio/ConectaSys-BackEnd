using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Users")]
public class User
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [Column("PasswordHash")]
    public string PasswordHash { get; set; }

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }

    [Column("Role")]
    public string? Role { get; set; }

    [Column("Phone")]
    public string? Phone { get; set; }

    [Column("IsActive")]
    public bool? IsActive { get; set; } = true; 

    public User(string name, string email, string passwordHash, string role , string phone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true; 
        Role = role;    
        Phone =phone;  

    }
}
