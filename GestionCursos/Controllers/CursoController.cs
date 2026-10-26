using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GestionCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService service;
        public CursoController(ICursoService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CursoResponse>> CreateCurso(CursoCreate dto)
        {
            var curso = await service.CreateService(dto);
            var respuesta = new ResponseGenerico<CursoResponse>
            {
                Codigo = 201,
                Message = "Curso Creado",
                Response = curso
            };
            return StatusCode(201, respuesta);
        }

        [HttpGet]
        public async Task<ActionResult<List<CursoResponse>>> GetCursos()
        {
            var cursos = await service.GetService();
            var respuesta = new ResponseGenerico<List<CursoResponse>>
            {
                Codigo = 200,
                Message = "Cursos Obtenidos",
                Response = cursos
            };
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoResponse>> GetCurso(int id)
        {
            var curso = await service.GetByIdService(id);
            var respuesta = new ResponseGenerico<CursoResponse>
            {
                Codigo = 200,
                Message = "Curso Obtenido",
                Response = curso
            };
            return Ok(respuesta);
        }
    }
}
