using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refaction.Controllers.Api;
using Refaction.Core;
using Refaction.Core.Dtos;
using Refaction.Core.Models;
using Refaction.Core.Repositories;
using System;
using System.Web.Http.Results;

namespace Refaction.Tests.Controllers.Api
{
    [TestClass]
    public class UnitTest1
    {
        private ProductsController _controller;
        private Mock<IProductRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize mock ProductRepository.
            _mockRepository = new Mock<IProductRepository>();

            // Initialize mock UnitOfWork.
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Products).Returns(_mockRepository.Object);

            // Initialize mock ProductsController.
            _controller = new ProductsController(mockUoW.Object);
        }

        [TestMethod]
        public void Create_ExistingProduct_ShouldReturnConflictResult()
        {
            var productDto = new ProductDto
            {                
                Name = "Test Existing Product",
                Description = "This product already exists.",
                Price = new decimal(999.99),
                DeliveryPrice = new decimal(5.00)
            };

            productDto.Id = new Guid();

            _mockRepository.Setup(p => p.IsExisting(new Guid())).Returns(true);

            var result = _controller.CreateProduct(productDto);

            result.Should().BeOfType<ConflictResult>();
        }

        [TestMethod]
        public void Create_NewProduct_ShouldReturnOk()
        {            
            var productDto = new ProductDto
            {  
                Id = Guid.NewGuid(),              
                Name = "Test Product",
                Description = "Add new product.",
                Price = new decimal(999.99),
                DeliveryPrice = new decimal(5.00)
            };

            _mockRepository.Setup(p => p.IsExisting(Guid.NewGuid())).Returns(false);

            var result = _controller.CreateProduct(productDto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]                
        public void Delete_ProductNotExisting_ShouldReturnNotFound()
        {
            var result = _controller.DeleteProduct(Guid.NewGuid());

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_ExistingProduct_ShouldReturnOk()
        {
            var product = new Product();            

            _mockRepository.Setup(p => p.Get(product.Id)).Returns(product);

            var result = _controller.DeleteProduct(product.Id);

            result.Should().BeOfType<OkResult>();
        }
    }
}
