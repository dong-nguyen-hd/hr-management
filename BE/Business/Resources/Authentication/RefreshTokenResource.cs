using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Resources.Authentication;

public class RefreshTokenResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Refresh Token")]
    public string RefreshToken { get; set; }

    [Required]
    public int AccountId { get; set; }

    [JsonIgnore]
    public string UserAgent { get; set; }
}