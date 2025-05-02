using Infrastructure.Contexts;

namespace UnitTest.Tool;

public class AppDbContextTests : TestWithSqlite<AppDbContext>
{

    [Fact]
    public void CanConnect_ReturnTrue_AppDbContext()
    {
            // Arrange
            using var context = new AppDbContext(Options);
            context.Database.EnsureCreated();

            // Act
            var result = context.Database.CanConnect();

            // Assert
            Assert.True(result);
        }
}