using Domain.Interfaces.Service;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Domain.Exceptions;
using System;
using System.Net.Mime;
using Domain.ViewModels.User;

namespace EcommerceAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cadastra um usuário na base de dados
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     POST /api/v1/users
        ///     {
        ///         "foto": {
        ///             "nome": "foto.png",
        ///             "conteudo": "T2zDoSwgbXVuZG8h"
        ///         },
        ///         "nome": "User1",
        ///         "email": "user@email.com",
        ///         "senha": "password123",
        ///         "telefone": "(81) 90123-4567"
        ///     }
        /// </remarks>
        /// <returns>Um novo usuário criado</returns>
        /// <param name="user">Usuário a ser cadastrado</param>
        /// <response code="201">Retorna o novo usuário criado</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="409">Se o usuário já existir</response>
        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(UserTokenViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(string))]
        public IActionResult SignUp([FromBody] UserSignUpViewModel user)
        {
            try
            {
                var createdUser = _userService.SignUp(user);

                return Created("", createdUser);
            }
            catch (EntityInvalidException ex)
            {
                var errors = new ErrorViewModel
                {
                    Errors = ex.ErrorMessages
                };

                return BadRequest(errors);
            }
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Faz o login de um usuário
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     POST /api/users/v1/login
        ///     {
        ///         "email": "user@email.com",
        ///         "senha": "password123"
        ///     }
        /// </remarks>
        /// <param name="user">Credenciais do usuário</param>
        /// <returns>O usuário logado e seu token de acesso</returns>
        /// <response code="200">Retorna o usuário logado e seu token de acesso</response>
        /// <response code="401">Se as credenciais estiverem incorretas</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserTokenViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult LogIn([FromBody] UserLogInViewModel user)
        {
            try
            {
                var loggedUser = _userService.LogIn(user.Email, user.Password);

                return Ok(loggedUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Pega as informações do usuário logado
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/users
        /// </remarks>
        /// <returns>As informações do usuário logado</returns>
        /// <response code="200">Retorna as informações do usuário logado</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserOutputViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult GetLoggedUser()
        {
            try
            {
                var loggedUser = _userService.GetLoggedUser(Request.Headers["Authorization"]);

                return Ok(loggedUser);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Deleta o usuário logado
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     DELETE /api/v1/users
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Se o usuário foi deletado com sucesso</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult RemoveLoggedUser()
        {
            try
            {
                _userService.RemoveLoggedUser(Request.Headers["Authorization"]);

                return NoContent();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o usuário logado
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     PUT /api/v1/users
        ///     {
        ///         "foto": {
        ///             "nome": "imagem.png",
        ///             "conteudo": "dGV4dG8gZGUgdGVzdGU="
        ///         },
        ///         "nome": "User123",
        ///         "senha": "senha123",
        ///         "telefone": "(99) 97345-8876"
        ///     }
        /// </remarks>
        /// <param name="user">Usuário atualizado</param>
        /// <returns></returns>
        /// <response code="200">Se o usuário foi atualizado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserOutputViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(string))]
        public IActionResult UpdateLoggedUser([FromBody] UserUpdateViewModel user)
        {
            try
            {
                _userService.UpdateLoggedUser(Request.Headers["Authorization"], user);

                return Ok();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
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
