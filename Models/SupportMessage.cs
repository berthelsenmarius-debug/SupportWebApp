using System.ComponentModel.DataAnnotations;

namespace SupportWebApp.Models;

public class SupportMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Navn skal udfyldes")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email skal udfyldes")]
    [EmailAddress(ErrorMessage = "Ugyldig email")]
    public string Email { get; set; } = "";

    [Phone(ErrorMessage = "Ugyldigt telefonnummer")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "Beskrivelse skal udfyldes")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Beskrivelsen skal være 10-1000 tegn")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Vælg en kategori")]
    public string Category { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
