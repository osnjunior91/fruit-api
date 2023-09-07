using FruitApi.BusinessLogic;
using FruitApi.DataAccess.Models;
using FruitApi.DataAccess.Repository;
using FruitApi.Entities;
using Moq;
using NUnit.Framework;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System;
using System.Runtime.InteropServices;
using FruitApi.Exceptions;

namespace FruitApi.Test.BusinessLogic
{
    public class BLFruitTest
    {
        private Mock<IRepository<Fruit>> _repository;
        private Mock<IBLFruitType> _bLFruitType;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<Fruit>>();
            _bLFruitType = new Mock<IBLFruitType>();
        }

        private FruitType fruitType = new FruitType(10, "FruitType", "Test Fruit Type");
        private Fruit fruit = new Fruit("Fruit", "Test Fruit");

        [TestCase("", "Test Description")]
        [TestCase("Test Name", "")]
        public void When_CreatedFruit_With_InvalidNameOrDescription(string name, string description)
        {
            var dto = new FruitDTO() { Name = name, Description = description, Type = 10 };
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            Assert.ThrowsAsync<ArgumentException>(() => blFruit.CreateAsync(dto));
        }
        [TestCase("Test Name", "Test Description")]
        public void When_CreatedFruit_With_InvalidType(string name, string description)
        {
            var dto = new FruitDTO() { Name = name, Description = description, Type = 10 };
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            Assert.ThrowsAsync<ArgumentException>(() => blFruit.CreateAsync(dto));
        }

        [TestCase("Test Name", "Test Description")]
        public void When_CreatedFruit_With_ValidParams(string name, string description)
        {
            _bLFruitType.Setup(m => m.GetByIdAync(It.IsAny<long>())).ReturnsAsync(fruitType);
            _repository.Setup(m => m.CreateAync(It.IsAny<Fruit>())).ReturnsAsync(fruit);
            var dto = new FruitDTO() { Name = name, Description = description, Type = 10 };
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            var result = blFruit.CreateAsync(dto).Result;
            Assert.That(result, Is.InstanceOf<long>());
        }

        [TestCase]
        public void When_DeleteFruit_NotExists()
        {
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            Assert.ThrowsAsync<NotFoundException>(() => blFruit.DeleteAync(10));
        }

        [TestCase("Test Name", "Test Description")]
        public void When_EditFruit_NotExists(string name, string description)
        {
            var dto = new FruitDTO() { Name = name, Description = description, Type = 10 };
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            Assert.ThrowsAsync<NotFoundException>(() => blFruit.EditAync(10, dto));
        }

        [TestCase("", "Test Description")]
        [TestCase("Test Name", "")]
        public void When_EditFruit_With_InvalidNameOrDescription(string name, string description)
        {
            _bLFruitType.Setup(m => m.GetByIdAync(It.IsAny<long>())).ReturnsAsync(fruitType);
            _repository.Setup(m => m.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(fruit);
            var dto = new FruitDTO() { Name = name, Description = description, Type = 10 };
            var blFruit = new BLFruit(_repository.Object, _bLFruitType.Object);
            Assert.ThrowsAsync<ArgumentException>(() => blFruit.EditAync(10, dto));
        }

    }
}
