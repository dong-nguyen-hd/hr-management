using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TokenRepository : BaseRepository<RefreshToken>, ITokenRepository
{
    #region Constructor
    public TokenRepository(CoreContext context) : base(context) { }
    #endregion

    #region Method
    public override async Task<RefreshToken> GetByIdAsync(int id) =>
        await Context.Tokens.SingleOrDefaultAsync(x => x.Id.Equals(id));

    public override int RemoveRange(IEnumerable<RefreshToken> tokens)
    {
            Context.Tokens.RemoveRange(tokens);

            return Enumerable.Count(tokens);
        }
    #endregion
}