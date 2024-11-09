using Infrastructure.Contexts;

namespace UnitTest.Tool;

public class CoreContextTests : TestWithSqlite<CoreContext>
{

    [Fact]
    public void CanConnect_ReturnTrue_AppDbContext()
    {
            // Arrange
            using var context = new CoreContext(Options);
            context.Database.EnsureCreated();

            // Act
            var result = context.Database.CanConnect();

            // Assert
            Assert.True(result);
        }
}