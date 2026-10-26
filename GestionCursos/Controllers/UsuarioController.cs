using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GestionCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService service;
        public UsuarioController(IUsuarioService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> CreateUsuario(UsuarioCreate dto)
        {
            var usuario = await service.CreateService(dto);
            var respuesta = new ResponseGenerico<UsuarioResponse>
            {
                Codigo = 201,
                Message = "Usuario registrado",
                Response = usuario
            };
            return StatusCode(201, respuesta);
        }

        
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponse>>> GetUsuarios([FromQuery] string? rol)
        {
            var usuarios = await service.GetService(rol);
            var respuesta = new ResponseGenerico<List<UsuarioResponse>>
            {
                Codigo = 200,
                Message = "Usuarios obtenidos",
                Response = usuarios
            };
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(int id)
        {
            var usuario = await service.GetByIdService(id);
            var respuesta = new ResponseGenerico<UsuarioResponse>
            {
                Codigo = 200,
                Message = "Usuario obtenido",
                Response = usuario
            };
            return Ok(respuesta);
        }
    }
}
