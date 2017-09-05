using AutoMapper;
using Refaction.Core;
using Refaction.Core.Dtos;
using Refaction.Core.Models;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Refaction.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        /// <summary>
        /// Gets all products.
        /// </summary>        
        [Route, HttpGet, ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProducts()
        {
            var products = _unitOfWork?.Products
                .GetAll()
                .Select(Mapper.Map<Product, ProductDto>);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        /// <summary>
        /// Finds all products matching the specified name.
        /// </summary>
        /// <param name="name"></param>        
        [Route, HttpGet, ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProducts(string name)
        {
            var products = _unitOfWork?.Products
                .SearchByName(name)
                .Select(Mapper.Map<Product, ProductDto>);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        /// <summary>
        /// Gets the product that matches the specified id.
        /// </summary>
        /// <param name="id"></param>        
        [Route("{id}"), HttpGet, ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProduct(Guid id)
        {
            var product = _unitOfWork?.Products.Get(id);

            if (product == null)
                return NotFound();

            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto"></param>        
        [Route, HttpPost, ResponseType(typeof(ProductDto))]
        public IHttpActionResult CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_unitOfWork.Products.IsExisting(productDto.Id))
                return Conflict();

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                DeliveryPrice = productDto.DeliveryPrice
            };

            try
            {
                _unitOfWork?.Products.Insert(product);
                _unitOfWork?.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>        
        [Route("{id}"), HttpPut, ResponseType(typeof(void))]
        public IHttpActionResult UpdateProduct(Guid id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (productDto == null)
                return NotFound();

            var product = Mapper.Map<ProductDto, Product>(productDto);
            product.Id = id;

            try
            {
                _unitOfWork?.Products.Update(product);
                _unitOfWork?.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a product and its options.
        /// </summary>
        /// <param name="id"></param>        
        [Route("{id}"), HttpDelete, ResponseType(typeof(void))]
        public IHttpActionResult DeleteProduct(Guid id)
        {
            var productToDelete = _unitOfWork?.Products.Get(id);

            if (productToDelete == null)
                return NotFound();

            try
            {
                _unitOfWork?.Products.Delete(id);
                _unitOfWork?.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }        
    }
}