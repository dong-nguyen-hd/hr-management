using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TokenRepository : BaseRepository<Token>, ITokenRepository
{
    #region Constructor
    public TokenRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public override async Task<Token> GetByIdAsync(int id) =>
        await Context.Tokens.SingleOrDefaultAsync(x => x.Id.Equals(id));

    public override int RemoveRange(IEnumerable<Token> tokens)
    {
            Context.Tokens.RemoveRange(tokens);

            return Enumerable.Count(tokens);
        }
    #endregion
}