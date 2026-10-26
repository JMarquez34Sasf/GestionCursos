using GestionCursos.Interfaces;
using GestionCursos.Models;
using GestionCursos.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GestionCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionService service;
        public InscripcionController(IInscripcionService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<InscripcionResponse>> Inscribir(InscripcionCreate dto)
        {
            var inscripcion = await service.CreateService(dto);
            var respuesta = new ResponseGenerico<InscripcionResponse>
            {
                Codigo = 201,
                Message = "Inscripción registrada",
                Response = inscripcion
            };
            return StatusCode(201, respuesta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionResponse>> GetInscripcion(int id)
        {
            var inscripcion = await service.Get(id);
            var respuesta = new ResponseGenerico<InscripcionResponse>
            {
                Codigo = 200,
                Message = "Inscripción obtenida",
                Response = inscripcion
            };
            return Ok(respuesta);
        }

        // Mis cursos (vista del estudiante)
        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<ActionResult<List<InscripcionResponse>>> GetByEstudiante(int idEstudiante)
        {
            var inscripciones = await service.GetByEstudianteService(idEstudiante);
            var respuesta = new ResponseGenerico<List<InscripcionResponse>>
            {
                Codigo = 200,
                Message = "Inscripciones del estudiante",
                Response = inscripciones
            };
            return Ok(respuesta);
        }

        // Inscritos de un curso (vista del instructor)
        [HttpGet("curso/{idCurso}")]
        public async Task<ActionResult<List<InscripcionResponse>>> GetByCurso(int idCurso)
        {
            var inscripciones = await service.GetByCursoService(idCurso);
            var respuesta = new ResponseGenerico<List<InscripcionResponse>>
            {
                Codigo = 200,
                Message = "Inscritos del curso",
                Response = inscripciones
            };
            return Ok(respuesta);
        }

        [HttpPut("{id}/cancelar")]
        public async Task<ActionResult<InscripcionResponse>> Cancelar(int id)
        {
            var inscripcion = await service.CancelarService(id);
            var respuesta = new ResponseGenerico<InscripcionResponse>
            {
                Codigo = 200,
                Message = "Inscripción cancelada",
                Response = inscripcion
            };
            return Ok(respuesta);
        }
    }
}
