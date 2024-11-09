using AutoMapper;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Business.Resources.DTOs.Authentication;
using Business.Resources.DTOs.Information;
using Business.Results;

namespace Business.Services;

public class TokenManagementService : ResponseMessageService, ITokenManagementService
{
    #region Property
    private readonly IAccountRepository _accountRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly byte[] _secret;
    #endregion

    #region Constructor
    public TokenManagementService(IAccountRepository accountRepository,
        ITokenRepository tokenRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(responseMessage)
    {
        this._accountRepository = accountRepository;
        this._tokenRepository = tokenRepository;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._secret = Encoding.ASCII.GetBytes(JwtConfig.Secret);
    }
    #endregion

    #region Method
    public async Task<BaseResult<TokenResource>> GenerateNewTokensAsync(RefreshTokenResource refreshTokenResource, DateTime now)
    {
        // Validate Refresh Token
        var tempRefreshToken = await _tokenRepository.GetByIdAsync(refreshTokenResource.Id);
        if (tempRefreshToken is null ||
            !tempRefreshToken.RefreshToken.Equals(refreshTokenResource.RefreshToken) ||
            tempRefreshToken.IsUsed ||
            !(DateTime.Compare(tempRefreshToken.ExpireTime, now) > 0))
            return new BaseResult<TokenResource>(ResponseMessage.Values["Token_Not_Permitted"]);

        // Validate Account
        var tempAccount = await _accountRepository.GetByIdAsync(refreshTokenResource.AccountId);
        if (tempAccount is null || !tempAccount.Id.Equals(tempRefreshToken.AccountId))
            return new BaseResult<TokenResource>(ResponseMessage.Values["Account_NoData"]);

        // Get access-token
        var accessToken = GenerateAccessToken(tempAccount, now);

        // Get refresh-token
        var refreshToken = GenerateRefreshToken(now, refreshTokenResource.UserAgent);

        // Set Last-Activity value
        tempAccount.LastActivity = DateTime.UtcNow;

        try
        {
            tempRefreshToken.IsUsed = true; // Disable refresh-token older

            tempAccount.Tokens.Add(refreshToken);
            await _unitOfWork.CompleteAsync();

            var result = new TokenResource()
            {
                Id = refreshToken.Id,
                AccessToken = accessToken,
                ExpireTime = refreshToken.ExpireDatetimeUtc,
                RefreshToken = refreshToken.RefreshToken,
                Role = tempAccount.Role
            };

            return new BaseResult<TokenResource>(result);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Token_Saving_Error"], ex);
        }
    }

    public async Task<BaseResult<object>> LogoutAsync(LogoutResource logoutResource)
    {
        // Validate Refresh Token
        var tempRefreshToken = await _tokenRepository.GetByIdAsync(logoutResource.Id);
        if (tempRefreshToken is null || !tempRefreshToken.RefreshToken.Equals(logoutResource.RefreshToken))
            return new BaseResult<object>(ResponseMessage.Values["Token_Invalid"]);

        try
        {
            tempRefreshToken.IsUsed = true;
            await _unitOfWork.CompleteAsync();

            return new BaseResult<object>(true);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Token_Saving_Error"], ex);
        }
    }

    public async Task<BaseResult<AccessTokenResource>> GenerateTokensAsync(LoginResource loginResource, DateTime now, string userAgent)
    {
        // Validate Login-Resource
        var tempAccount = await _accountRepository.ValidateCredentialsAsync(loginResource);
        if (tempAccount is null)
            return new BaseResult<AccessTokenResource>(ResponseMessage.Values["Token_Invalid"]);

        // Get access-token
        var accessToken = GenerateAccessToken(tempAccount, now);

        // Get refresh-token
        var refreshToken = GenerateRefreshToken(now, userAgent);

        // Set Last-Activity value
        tempAccount.LastActivity = DateTime.UtcNow;

        try
        {
            tempAccount.Tokens = new() { refreshToken };
            await _unitOfWork.CompleteAsync();

            return new BaseResult<AccessTokenResource>(MappingTokenResoure(tempAccount, refreshToken, accessToken));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Token_Saving_Error"], ex);
        }
    }

    private AccessTokenResource MappingTokenResoure(Account account, RefreshToken refreshToken, string accessToken)
    {
        var tokenResource = _mapper.Map<Account, AccessTokenResource>(account);
        tokenResource.TokenResource = _mapper.Map<RefreshToken, TokenResource>(refreshToken);
        tokenResource.TokenResource.AccessToken = accessToken;

        return tokenResource;
    }

    private string GenerateAccessToken(Account account, DateTime now)
    {
        // Get claim value
        Claim[] claims = GetClaim(account);

        var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

        var jwtToken = new JwtSecurityToken(
            JwtConfig.Issuer,
            shouldAddAudienceClaim ? JwtConfig.Audience : string.Empty,
            claims,
            expires: now.AddMinutes(JwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return accessToken;
    }

    private static Claim[] GetClaim(Account account)
    {
        var claims = new[]
        {
            // Optional: you can add other Claims.
            // Note: You avoid sensitive information, because this is public.
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Name, account.UserName),
            new Claim(ClaimTypes.Role, account.Role),
        };

        return claims;
    }

    private RefreshToken GenerateRefreshToken(DateTime now, string userAgent)
        => new()
        {
            RefreshToken = GenerateRefreshTokenString(),
            ExpireDatetimeUtc = now.AddMinutes(JwtConfig.RefreshTokenExpiration),
            UserAgent = userAgent,
            IsUsed = false
        };

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
    #endregion
}