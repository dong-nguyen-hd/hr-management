using Business.Resources.Account;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Authentication;

public class AccessTokenResource : AccountResource
{
    [Display(Name = "Token Resource")]
    [JsonProperty(Order = 1)]
    public TokenResource TokenResource { get; set; }
}

public class TokenResource
{
    public int Id { get; set; }

    [Display(Name = "Refresh Token")]
    public string RefreshToken { get; set; }

    [Display(Name = "Expire Time")]
    public DateTime ExpireTime { get; set; }

    [Display(Name = "Access Token")]
    public string AccessToken { get; set; }

    public string Role { get; set; }
}