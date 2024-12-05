using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;
using ProdajnikWeb.Controllers;
using ProdajnikWeb.Data;
using ProdajnikWeb.Models;
using ProdajnikWeb.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Tests
{
    [TestClass]
    public class ConstructionObjectsControllerTests
    {
        private Mock<ProdajnikContext> _mockContext;
        private Mock<IMemoryCache> _mockCache;
        private CachedDataService _service;
        private ConstructionObjectsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            // Создаем заглушки для зависимостей
            _mockContext = new Mock<ProdajnikContext>();
            _mockCache = new Mock<IMemoryCache>();

            // Создаем экземпляр CachedDataService с моками
            _service = new CachedDataService(_mockContext.Object, _mockCache.Object);

            // Инициализируем контроллер, передавая оба параметра
            _controller = new ConstructionObjectsController(_mockContext.Object, _service);
        }

        [TestMethod]
        public async Task Edit_ValidId_ShouldUpdateObject()
        {
            // Arrange
            var constructionObject = new ConstructionObject { ObjectId = 1, ObjectName = "Object1" };
            _mockContext.Setup(ctx => ctx.ConstructionObjects).ReturnsDbSet(new List<ConstructionObject> { constructionObject }.AsQueryable());

            // Act
            var result = await _controller.Edit(1, constructionObject) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task Edit_InvalidId_ShouldReturnNotFound()
        {
            // Act
            var result = await _controller.Edit(99, new ConstructionObject { ObjectId = 1 }) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
