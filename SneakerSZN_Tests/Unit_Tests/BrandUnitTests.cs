using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;
using SneakerSZN_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using SneakerSZN_BLL.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace SneakerSZN_Tests.Unit_Tests
{
    [TestClass]
    public class BrandUnitTests
    {
        private readonly IBrandService _service;
        private Mock<IBrandRepository> _brandRepositoryMock;

        public BrandUnitTests()
        {
            // Arrange
            _brandRepositoryMock = new Mock<IBrandRepository>();

            _service = new BrandService(_brandRepositoryMock.Object); 
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            List<Brand> mockBrands = new List<Brand>();
            mockBrands.Add(new Brand() { Name = "Nike" });
            mockBrands.Add(new Brand() { Name = "Adidas" });
            mockBrands.Add(new Brand() { Name = "Puma" });

            _brandRepositoryMock.Setup(repo => repo.GetAll()).Returns(mockBrands);

            // Act
            List<Brand> brands = _service.GetAll();

            // Assert
            Assert.IsNotNull(brands);
            Assert.AreEqual(3, brands.Count);
            CollectionAssert.AreEqual(mockBrands, brands);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            Brand mockBrand = new Brand() { Name = "Nike" };
            

            _brandRepositoryMock.Setup(repo => repo.GetById(1)).Returns(mockBrand);

            // Act
            Brand brand = _service.GetById(1);

            // Assert
            Assert.IsNotNull(brand);
            Assert.AreEqual(brand.Name, mockBrand.Name);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            Brand mockBrand = new Brand() { Name = "Adidas" };

            _brandRepositoryMock.Setup(repo => repo.Create(mockBrand)).Returns(true);

            // Act
            bool result = _service.Create(mockBrand);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateWithEmptyName_ShouldFail()
        {
            // Arrange
            var mockBrand = new Brand() { Name = "" };

            _brandRepositoryMock.Setup(repo => repo.Create(It.IsAny<Brand>())).Throws(new ArgumentException("Brand name is required."));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _service.Create(mockBrand));
        }

        [TestMethod]
        public void CreateNameExceeds30Chars_ShouldFail()
        {
            // Arrange
            var mockBrand = new Brand() { Name = "123456789 123456789 1234567890 " };

            _brandRepositoryMock.Setup(repo => repo.Create(It.IsAny<Brand>())).Throws(new ArgumentException("Sneaker name cannot exceed 30 characters."));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _service.Create(mockBrand));
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            Brand mockBrand = new Brand() { Name = "Adidass" };

            _brandRepositoryMock.Setup(repo => repo.Update(mockBrand)).Returns(true);

            // Act
            bool result = _service.Update(mockBrand);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            _brandRepositoryMock.Setup(repo => repo.Delete(1)).Returns(true);

            // Act
            bool result = _service.Delete(1);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
