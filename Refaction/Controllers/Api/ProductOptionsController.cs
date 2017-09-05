using AutoMapper;
using Refaction.Core;
using Refaction.Core.Dtos;
using Refaction.Core.Models;
using Refaction.Persistence;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Refaction.Controllers.Api
{
    [RoutePrefix("api/products")]    
    public class ProductOptionsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public ProductOptionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds all options for a specified product.
        /// </summary>
        /// <param name="productId"></param>        
        [Route("{productId}/options"), HttpGet, ResponseType(typeof(ProductOptionDto))]
        public IHttpActionResult GetProductOptions(Guid productId)
        {
            var productOptions = _unitOfWork?.ProductOptions
                .GetAvailableOptionsForProduct(productId)
                .Select(Mapper.Map<ProductOption, ProductOptionDto>);

            if (productOptions == null)
                return NotFound();

            return Ok(productOptions);
        }

        /// <summary>
        /// Finds the specified product option for the specified product.
        /// </summary>
        /// <param name="productId"></param>           
        /// <param name="optionId"></param>        
        [Route("{productId}/options/{optionId}"), HttpGet, ResponseType(typeof(ProductOptionDto))]
        public IHttpActionResult GetProductOption(Guid productId, Guid optionId)
        {
            var productOption = _unitOfWork?.ProductOptions
                .Get(optionId, productId);

            if (productOption == null)
                return NotFound();

            return Ok(Mapper.Map<ProductOption, ProductOptionDto>(productOption));
        }

        /// <summary>
        /// Adds a new product option to the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productOptionDto"></param>        
        [Route("{productId}/options", Name = nameof(CreateProductOption)), HttpPost, ResponseType(typeof(ProductOptionDto))]
        public IHttpActionResult CreateProductOption(Guid productId, ProductOptionDto productOptionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productOption = Mapper.Map<ProductOptionDto, ProductOption>(productOptionDto);

            try
            {
                productOption.ProductId = productId;
                _unitOfWork?.ProductOptions.Insert(productOption);
                _unitOfWork?.Save();
            }
            catch (DbUpdateException)
            {
                if ((bool)_unitOfWork.ProductOptions.IsExisting(productOption.Id, productId))
                    return Conflict();
                else
                    throw;
            }                    

            return Created(new Uri(Request.RequestUri + "/" + productOption.Id), productOptionDto);
        }

        /// <summary>
        /// Updates the specified product option.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <param name="productOptionDto"></param>        
        [Route("{productId}/options/{optionId}"), HttpPut, ResponseType(typeof(void))]
        public IHttpActionResult UpdateProductOption(Guid productId, Guid optionId, ProductOptionDto productOptionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (productOptionDto == null)
                return NotFound();

            var productOption = Mapper.Map<ProductOptionDto, ProductOption>(productOptionDto);
            productOption.Id = optionId;
            productOption.ProductId = productId;

            try
            {
                _unitOfWork?.ProductOptions.Update(productOption);
                _unitOfWork?.Save();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Deletes the specified product option.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>        
        [Route("{productId}/options/{optionId}"), HttpDelete, ResponseType(typeof(void))]
        public IHttpActionResult DeleteProductOption(Guid productId, Guid optionId)
        {
            var productOptionToDelete = _unitOfWork?.ProductOptions
                .Get(optionId, productId);

            if (productOptionToDelete == null)
                return NotFound();

            try
            {
                _unitOfWork?.ProductOptions.Delete(optionId);
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