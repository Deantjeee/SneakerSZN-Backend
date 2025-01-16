using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Models;
using SneakerSZN_BLL.Services;
using SneakerSZN_DAL.Data;
using SneakerSZN_DAL.Repositories;

namespace SneakerSZN_Tests.IntegrationTests
{
    [TestClass]
    public class SneakerIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private SneakerService _sneakerService;

        public SneakerIntegrationTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (ApplicationDbContext context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }

            var sneakerRespository = new SneakerRepository(new ApplicationDbContext(_options));
            var brandRepository = new BrandRepository(new ApplicationDbContext(_options));
            _sneakerService = new SneakerService(sneakerRespository);
        }

        private async Task SeedDatabase(ApplicationDbContext context)
        {
            byte[] imageBytes = new byte[64];

            //Setup Sneakers
            context.Sneakers.AddRange(
            new Sneaker { Id = 1, Name = "Airforce 1", Size = 41, Price = 100, Stock = 300, Image = imageBytes },
            new Sneaker { Id = 2, Name = "Campus", Size = 41, Price = 120, Stock = 50, Image = imageBytes }
                );
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task GetAll()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerRepository sneakerRepository = new SneakerRepository(context);

                //Act
                IEnumerable<Sneaker> result = _sneakerService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
                Assert.AreEqual("Airforce 1", result.First().Name);
            }
        }

        [TestMethod]
        public async Task GetById()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerRepository sneakerRepository = new SneakerRepository(context);

                //Act
                Sneaker sneaker1 = _sneakerService.GetById(1);
                Sneaker sneaker2 = _sneakerService.GetById(2);

                //Assert
                Assert.IsNotNull(sneaker1);
                Assert.IsNotNull(sneaker2);
                Assert.AreEqual("Airforce 1", sneaker1.Name);
                Assert.AreEqual("Campus", sneaker2.Name);
            }
        }

        [TestMethod]
        public async Task GetById_IdDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerRepository sneakerRepository = new SneakerRepository(context);

                //Act
                Sneaker sneaker1 = _sneakerService.GetById(10);

                //Assert
                Assert.IsNull(sneaker1);
            }
        }

        [TestMethod]
        public async Task Create()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerService sneakerService = new SneakerService(new SneakerRepository(context));

                //Act
                IEnumerable<Sneaker> result = _sneakerService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 2);

                //Act
                byte[] imageBytes = new byte[64];

                Sneaker newSneaker = new Sneaker { Name = "Jordan 4", Size = 41, Price = 100, Stock = 300, Image = imageBytes };
                _sneakerService.Create(newSneaker);

                result = _sneakerService.GetAll();

                //Fluent assertions
                //Assert
                Assert.IsNotNull(newSneaker);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 3);
                Assert.IsTrue(result.Last().Name == newSneaker.Name);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerService sneakerService = new SneakerService(new SneakerRepository(context));

                //Act
                IEnumerable<Sneaker> result = _sneakerService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 2);

                //Act
                _sneakerService.Delete(2);

                result = _sneakerService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 1);
                Assert.IsTrue(result.First().Name == "Airforce 1");
            }
        }

        [TestMethod]
        public async Task Delete_IdDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerService sneakerService = new SneakerService(new SneakerRepository(context));

                //Act
                IEnumerable<Sneaker> result = _sneakerService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 2);

                //Act
                Assert.IsFalse(_sneakerService.Delete(10));

                result = _sneakerService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                SneakerService sneakerService = new SneakerService(new SneakerRepository(context));

                //Act
                IEnumerable<Sneaker> result = _sneakerService.GetAll();
                Assert.IsTrue(result.First().Name == "Airforce 1");

                byte[] imageBytes = new byte[64];

                Sneaker updatedSneaker = _sneakerService.GetById(1);
                updatedSneaker.Name = "Jordan 4";

                _sneakerService.Update(updatedSneaker);

                result = _sneakerService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
                Assert.IsTrue(result.First().Name == "Jordan 4");
            }
        }
    }
}