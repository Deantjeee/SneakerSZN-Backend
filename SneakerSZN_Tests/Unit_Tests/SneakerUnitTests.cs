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

namespace SneakerSZN_Tests.Unit_Tests
{
    [TestClass]
    public class SneakerUnitTests
    {
        private readonly ISneakerService _service;
        private Mock<ISneakerRepository> _sneakerRepositoryMock;

        public SneakerUnitTests()
        {
            // Arrange
            _sneakerRepositoryMock = new Mock<ISneakerRepository>();

            _service = new SneakerService(_sneakerRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            List<Sneaker> mockSneakers = new List<Sneaker>();
            mockSneakers.Add(new Sneaker() { Name = "Airforce 1", Size = 41, Stock = 100 });
            mockSneakers.Add(new Sneaker() { Name = "Jordan 1", Size = 39, Stock = 67 });
            mockSneakers.Add(new Sneaker() { Name = "Campus 00", Size = 43, Stock = 104 });

            _sneakerRepositoryMock.Setup(repo => repo.GetAll()).Returns(mockSneakers);

            // Act
            List<Sneaker> sneakers = _service.GetAll();

            // Assert
            Assert.IsNotNull(sneakers);
            Assert.AreEqual(3, sneakers.Count);
            CollectionAssert.AreEqual(mockSneakers, sneakers);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            Sneaker mockSneaker = new Sneaker() { Name = "Nike" };


            _sneakerRepositoryMock.Setup(repo => repo.GetById(1)).Returns(mockSneaker);

            // Act
            Sneaker sneaker = _service.GetById(1);

            // Assert
            Assert.IsNotNull(sneaker);
            Assert.AreEqual(sneaker.Name, mockSneaker.Name);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            Sneaker mockSneaker = new Sneaker() { Name = "Airforce 1", Size = 41, Stock = 100 };


            _sneakerRepositoryMock.Setup(repo => repo.Create(mockSneaker)).Returns(true);

            // Act
            bool result = _service.Create(mockSneaker);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateWithEmptyName_ShouldFail()
        {
            // Arrange
            Sneaker mockSneaker = new Sneaker() { Name = "", Size = 41, Stock = 100 };


            _sneakerRepositoryMock.Setup(repo => repo.Create(mockSneaker)).Throws(new ArgumentException("Sneaker name is required."));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _service.Create(mockSneaker));
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            Sneaker mockSneaker = new Sneaker() {
                Name = "Airforce 2",
                Size = 41,
                Stock = 100 };

            _sneakerRepositoryMock.Setup(repo => repo.Update(mockSneaker)).Returns(true);

            // Act
            bool result = _service.Update(mockSneaker);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            _sneakerRepositoryMock.Setup(repo => repo.Delete(1)).Returns(true);

            // Act
            bool result = _service.Delete(1);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
