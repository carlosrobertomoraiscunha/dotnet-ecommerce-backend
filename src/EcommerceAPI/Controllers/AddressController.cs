using Domain.Exceptions;
using Domain.Interfaces.Service;
using Domain.ViewModels;
using Domain.ViewModels.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;

namespace EcommerceAPI.Controllers
{
    [Route("api/v1/users/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Cadastra um endereço para o usuário logado
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     POST /api/v1/users/addresses
        ///     {
        ///         "cep": "64056-648",
        ///         "bairro": "Piçarreira",
        ///         "rua": "Rua Odete Soares Nunes",
        ///         "cidade": "Teresina",
        ///         "estado": "Piauí",
        ///         "numero": "156",
        ///         "complemento": "B"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <param name="address">Endereço a ser cadastrado</param>
        /// <response code="201">Se o endereço foi cadastrado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult CreateAddressForLoggedUser([FromBody] AddressViewModel address)
        {
            try
            {
                _addressService.CreateAddressForLoggedUser(Request.Headers["Authorization"],
                                                           address);

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
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Pega todos os endereços do usuário logado
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/users/addresses
        /// </remarks>
        /// <returns>Uma lista com todos os endereços do usuário logado</returns>
        /// <response code="200">Retorna todos os endereços do usuário logado</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult GetAllAddressesFromLoggedUser()
        {
            try
            {
                var addresses = _addressService.GetAllAddressesFromLoggedUser(Request.Headers["Authorization"]);

                return Ok(addresses);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Pega um determinado endereço, do usuário logado, pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/users/addresses/{id}
        /// </remarks>
        /// <returns>Um determinado endereço do usuário logado</returns>
        /// <response code="200">Retorna um determinado endereço do usuário logado</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver endereço com o id informado no usuário logado</response>
        [Authorize]
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult GetAddressByIdFromLoggedUser(long id)
        {
            try
            {
                var addresses = _addressService.GetAddressByIdFromLoggedUser(Request.Headers["Authorization"],
                                                                             id);

                return Ok(addresses);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um determinado endereço, do usuário logado, pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     DELETE /api/v1/users/addresses/{id}
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Se o endereço foi deletado com sucesso</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver endereço com o id informado no usuário logado</response>
        [Authorize]
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult RemoveAddressByIdFromLoggedUser(long id)
        {
            try
            {
                _addressService.RemoveAddressByIdFromLoggedUser(Request.Headers["Authorization"],
                                                                id);

                return NoContent();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um determinado endereço, do usuário logado, pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     PUT /api/v1/users/addresses/{id}
        ///     {
        ///         "cep": "72907-126",
        ///         "bairro": "Jardim de Alá",
        ///         "rua": "Quadra Quadra 53B",
        ///         "cidade": "Santo Antônio do Descoberto",
        ///         "estado": "Goiás",
        ///         "numero": "45",
        ///         "complemento": "Primeiro andar"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Se o endereço foi atualizado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver endereço com o id informado no usuário logado</response>
        [Authorize]
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult UpdateAddressByIdFromLoggedUser(long id, [FromBody] AddressViewModel address)
        {
            try
            {
                _addressService.UpdateAddressByIdFromLoggedUser(Request.Headers["Authorization"],
                                                                id,
                                                                address);

                return Ok();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
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
    }
}
