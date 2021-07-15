using Domain.Exceptions;
using Domain.Interfaces.Service;
using Domain.ViewModels;
using Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace EcommerceAPI.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Pega as informações de todos os usuário da base de dados
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/admin/users
        /// </remarks>
        /// <returns>Uma lista com as informações de todos os usuários</returns>
        /// <response code="200">Retorna as informações de todos os usuários</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ICollection<UserOutputViewModel>))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllUsers()
        {
            var users = _adminService.GetAllUsers();

            return Ok(users);
        }

        /// <summary>
        /// Pega as informações de um determinado usuário pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     GET /api/v1/admin/users/{id}
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <returns>As informações do usuário com o id informado</returns>
        /// <response code="200">Retorna as informações do usuário com o id informado</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver usuário com o id informado</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ICollection<UserOutputViewModel>))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult GetUserById(long id)
        {
            try
            {
                var user = _adminService.GetUserById(id);

                return Ok(user);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um determinado usuário pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     DELETE /api/v1/admin/users/{id}
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        /// <response code="204">Se o usuário foi deletado com sucesso</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver usuário com o id informado</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult RemoveUserById(long id)
        {
            try
            {
                _adminService.RemoveUserById(id);

                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um determinado usuário pelo seu id
        /// </summary>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     PUT /api/v1/admin/users/{id}
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
        /// <param name="id">Id do usuário</param>
        /// <param name="user">Usuário atualizado</param>
        /// <returns></returns>
        /// <response code="200">Se o usuário foi atualizado com sucesso</response>
        /// <response code="400">Se houver algum campo preenchido incorretamente</response>
        /// <response code="401">Se o token de acesso for inválido</response>
        /// <response code="404">Se não houver usuário com o id informado</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorViewModel))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(string))]
        public IActionResult UpdateUserById(long id, [FromBody] UserUpdateViewModel user)
        {
            try
            {
                _adminService.UpdateUserById(id, user);

                return Ok();
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
