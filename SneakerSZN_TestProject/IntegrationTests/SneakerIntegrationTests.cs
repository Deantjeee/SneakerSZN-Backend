using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Models;
using SneakerSZN_BLL.Services;
using SneakerSZN_DAL.Data;
using SneakerSZN_DAL.Repositories;

namespace SneakerSZN_TestProject.UnitTests
{
    [TestClass]
    public class SneakerIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private SneakerService _service;

        public SneakerIntegrationTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (ApplicationDbContext context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }

            var respository = new SneakerRepository(new ApplicationDbContext(_options));
            _service = new SneakerService(respository);
        }

        private async Task SeedDatabase(ApplicationDbContext context)
        {
            context.Sneakers.AddRange(
                new Sneaker(1, "Airforce 1", 41, 100, 300, null, 1, null),
                new Sneaker(2, "Jordan 1", 42, 100, 250, null, 1, null)
                );
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public async void SneakerGetAll()
        {
            using(var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerRepository sneakerRepository = new SneakerRepository(context);

                //Act
                IEnumerable<Sneaker> result = _service.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
                Assert.Equals("Airforce 1", result.First().Name);
                Assert.Equals("Jordan 1", result.First().Name);
            }
        }
    }
}