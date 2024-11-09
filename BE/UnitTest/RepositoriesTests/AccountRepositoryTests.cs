using API.Domain.Models;
using API.Extensions;
using API.Resources.DTOs.Authentication;
using API.Resources.Enums;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using UnitTest.Tool;

namespace UnitTest.RepositoriesTests;

public class AccountRepositoryTests : TestWithSqlite<CoreContext>
{
    #region Private work
    private static void SeedDatabaseThreeAccount(CoreContext context)
    {
            context.Accounts.AddRange(
                new Account
                {
                    Id = 1,
                    UserName = "admin1",
                    Password = "10000.om7b7+wyo6oufN1g+bOnKQ==.3WbZ3GlZH6mz6JPohiNcH0UI0OmssZA9h/XeoodmDRY=", // Input: c93ccd78b2076528346216b3b2f701e6 (plain-text: admin1234) (hash function: MD5)
                    Email = "dong.nguyen.hdkt@gmail.com",
                    Role = eRole.Admin.ToDescriptionString(),
                    CreatedAt = DateTime.UtcNow,
                    Name = "Dong Nguyen 1",
                    LastActivity = DateTime.UtcNow,
                    IsDeleted = false,
                    Avatar = "default.jpg"
                },
                new Account
                {
                    Id = 2,
                    UserName = "admin2",
                    Password = "10000.om7b7+wyo6oufN1g+bOnKQ==.3WbZ3GlZH6mz6JPohiNcH0UI0OmssZA9h/XeoodmDRY=", // Input: c93ccd78b2076528346216b3b2f701e6 (plain-text: admin1234) (hash function: MD5)
                    Email = "dong.nguyen.hdkt@gmail.com",
                    Role = eRole.EditorQTDA.ToDescriptionString(),
                    CreatedAt = DateTime.UtcNow,
                    Name = "Dong Nguyen 2",
                    LastActivity = DateTime.UtcNow,
                    IsDeleted = false,
                    Avatar = "default.jpg"
                },
                new Account
                {
                    Id = 3,
                    UserName = "admin3",
                    Password = "10000.om7b7+wyo6oufN1g+bOnKQ==.3WbZ3GlZH6mz6JPohiNcH0UI0OmssZA9h/XeoodmDRY=", // Input: c93ccd78b2076528346216b3b2f701e6 (plain-text: admin1234) (hash function: MD5)
                    Email = "dong.nguyen.hdkt@gmail.com",
                    Role = eRole.Viewer.ToDescriptionString(),
                    CreatedAt = DateTime.UtcNow,
                    Name = "Dong Nguyen 3",
                    LastActivity = DateTime.UtcNow,
                    IsDeleted = false,
                    Avatar = "default.jpg"
                }
            );
            context.SaveChanges();
        }
    #endregion

    #region Test
    [Fact]
    public async Task GetWithPrimaryKeyAsync_WithThreeIds_ShouldEqual()
    {
            // Arrange
            using var context = new CoreContext(Options);
            context.Database.EnsureCreated();
            SeedDatabaseThreeAccount(context);

            var accountRepository = new AccountRepository(context);

            // Act
            List<int> ids = new() { 1, 2, 3 };
            var result = await accountRepository.GetWithPrimaryKeyAsync(ids);

            // Assert
            Assert.Equal(Enumerable.Count(result), ids.Count);
        }

    [Fact]
    public void RemoveRange_ShouldNotEqual()
    {
            // Arrange
            using var context = new CoreContext(Options);
            context.Database.EnsureCreated();
            SeedDatabaseThreeAccount(context);

            var accountRepository = new AccountRepository(context);

            // Act
            List<int> ids = new() { 1, 2, };
            var listAccount = accountRepository.GetWithPrimaryKeyAsync(ids).Result;
            accountRepository.RemoveRange(listAccount);
            context.SaveChanges();
            var accountDeleted = accountRepository.GetWithPrimaryKeyAsync(ids).Result;

            // Assert
            Assert.NotEqual(accountDeleted, listAccount);
        }

    [Fact]
    public void ValidateCredentialsAsync_ShouldEqual()
    {
            // Arrange
            using var context = new CoreContext(Options);
            context.Database.EnsureCreated();
            SeedDatabaseThreeAccount(context);

            var accountRepository = new AccountRepository(context);

            // Act
            LoginResource loginResource = new() { Password = "c93ccd78b2076528346216b3b2f701e6", UserName = "admin1" };
            var account = accountRepository.ValidateCredentialsAsync(loginResource).Result;

            // Assert
            Assert.Equal("admin1", account.UserName);
        }

    [Fact]
    public void ValidateUserNameAsync_ShouldInvalid()
    {
            // Arrange
            using var context = new CoreContext(Options);
            context.Database.EnsureCreated();
            SeedDatabaseThreeAccount(context);

            var accountRepository = new AccountRepository(context);

            // Act
            var isValid = accountRepository.ValidateUserNameAsync("admin1").Result;

            // Assert
            Assert.False(isValid);
        }
    #endregion
}