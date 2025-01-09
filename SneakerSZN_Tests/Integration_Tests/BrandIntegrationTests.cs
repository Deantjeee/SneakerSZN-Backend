using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;
using SneakerSZN_BLL.Services;
using SneakerSZN_DAL.Data;
using SneakerSZN_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_Tests.Integration_Tests
{
    [TestClass]
    public class BrandIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private BrandService _brandService;

        public BrandIntegrationTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (ApplicationDbContext context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureCreated();
            }

            var brandRepository = new BrandRepository(new ApplicationDbContext(_options));
            _brandService = new BrandService(brandRepository);
        }

        private async Task SeedDatabase(ApplicationDbContext context)
        {
            //Setup Brands
            context.Brands.AddRange(
            new Brand { Name = "Nike" },
            new Brand { Name = "Adidas" },
            new Brand { Name = "Puma" }
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
                BrandRepository brandRepository = new BrandRepository(context);

                //Act
                IEnumerable<Brand> result = _brandService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 3);
                Assert.AreEqual("Nike", result.First().Name);
            }
        }

        [TestMethod]
        public async Task GetById()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandRepository brandRepository = new BrandRepository(context);

                //Act
                Brand result = _brandService.GetById(1);

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Name == "Nike");
                Assert.IsTrue(result.Id == 1);
            }
        }

        [TestMethod]
        public async Task GetById_IdDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandRepository brandRepository = new BrandRepository(context);

                //Act
                Brand result = _brandService.GetById(10);

                //Assert
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public async Task Create()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandService BrandService = new BrandService(new BrandRepository(context));

                //Act
                IEnumerable<Brand> result = _brandService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 3);

                //Act
                Brand newBrand = new Brand { Name = "OffWhite" };
                _brandService.Create(newBrand);

                result = _brandService.GetAll();

                //Fluent assertions
                //Assert
                Assert.IsNotNull(newBrand);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 4);
                Assert.IsTrue(result.Last().Name == newBrand.Name);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandService brandService = new BrandService(new BrandRepository(context));

                //Act
                IEnumerable<Brand> result = _brandService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 3);

                //Act
                _brandService.Delete(2);

                result = _brandService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
                Assert.IsTrue(result.First().Name == "Nike");
            }
        }

        [TestMethod]
        public async Task Delete_IdDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandService brandService = new BrandService(new BrandRepository(context));

                //Act
                IEnumerable<Brand> result = _brandService.GetAll();

                //Assert
                Assert.IsTrue(result.Count() == 3);

                //Act
                Assert.IsFalse(_brandService.Delete(5));

                result = _brandService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 3);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                //Arrange
                await SeedDatabase(context);
                BrandService brandService = new BrandService(new BrandRepository(context));

                //Act
                IEnumerable<Brand> result = _brandService.GetAll();
                Assert.IsTrue(result.First().Name == "Nike");

                Brand updatedBrand = _brandService.GetById(1);
                updatedBrand.Name = "Balenciaga";

                _brandService.Update(updatedBrand);

                result = _brandService.GetAll();

                //Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 3);
                Assert.IsTrue(result.First().Name == "Balenciaga");
            }
        }
    }
}
