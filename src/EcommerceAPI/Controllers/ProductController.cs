using Domain.Exceptions;
using Domain.Interfaces.Service;
using Domain.ViewModels;
using Domain.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace EcommerceAPI.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Registra um novo produto
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     POST /api/v1/products
        ///     {
        ///         "imagem": {
        ///             "nome": "kindle.png",
        ///             "conteudo": "T2zDoSwgbXVuZG8h"
        ///         },
        ///         "nome": "Kindle 10a. geração",
        ///         "preco": 331.55,
        ///         "descricao": "Kindle 10a. geração com iluminação embutida e bateria de longa duração - Cor Preta",
        ///         "categoria": "Eletrônicos"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <param name="product">Produto a ser cadastrado</param>
        /// <response code="201">Se o produto foi criado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult RegisterProduct([FromBody] ProductViewModel product)
        {
            try
            {
                _productService.RegisterProduct(product);

                return Created("", null);
            }
            catch (EntityInvalidException ex)
            {
                var errors = new ErrorViewModel
                {
                    Errors = ex.ErrorMessages
                };

                return BadRequest(errors);
            }
        }

        /// <summary>
        /// Pega todos os produtos ou aqueles que pertencem a uma determinada categoria
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/products?category=eletrônicos
        /// </remarks>
        /// <returns>Um lista de produtos</returns>
        /// <param name="category">Nome da categoria</param>
        /// <response code="200">Retorna uma lista de produtos</response>
        [AllowAnonymous]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ICollection<ProductOutputViewModel>))]
        public IActionResult GetAllProducts([FromQuery] string category = null)
        {
            ICollection<ProductOutputViewModel> products;

            if (category is null)
                products = _productService.GetAllProducts();
            else
                products = _productService.GetAllProductsByCategory(category);

            return Ok(products);
        }

        /// <summary>
        /// Pega um produto pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/products/{id}
        /// </remarks>
        /// <param name="id">Id do produto</param>
        /// <returns>O produto com o id informado</returns>
        /// <response code="200">Retorna o produto com o id informado</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver nenhum produto com o id informado</response>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ProductOutputViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult GetProductById(long id)
        {
            try
            {
                var product = _productService.GetProductById(id);

                return Ok(product);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Remove um produto
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     DELETE /api/v1/products/{id}
        /// </remarks>
        /// <returns></returns>
        /// <param name="id">Id do produto</param>
        /// <response code="204">Se o produto foi removido com sucesso</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se o produto com o id informado não foi encontrado</response>
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult RemoveProductById(long id)
        {
            try
            {
                _productService.RemoveProductById(id);

                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um produto pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     PUT /api/v1/products/{id}
        ///     {
        ///         "imagem": {
        ///             "nome": "echo_dot.png",
        ///             "conteudo": "T2zDoSwgbXVuZG8h"
        ///         },
        ///         "nome": "Echo Dot 4a. geração",
        ///         "preco": 379.05,
        ///         "descricao": "Novo Echo Dot (4ª Geração): Smart Speaker com Alexa - Cor Preta",
        ///         "categoria": "Eletrônicos"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <param name="id">Id do produto</param>
        /// <param name="product">Produto atualizado</param>
        /// <response code="200">Se o produto foi atualizado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se o produto com o id informado não foi encontrado</response>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult UpdateProductById(long id, [FromBody] ProductViewModel product)
        {
            try
            {
                _productService.UpdateProductById(id, product);

                return Ok();
            }
            catch(EntityInvalidException ex)
            {
                var errors = new ErrorViewModel
                {
                    Errors = ex.ErrorMessages
                };

                return BadRequest(errors);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
