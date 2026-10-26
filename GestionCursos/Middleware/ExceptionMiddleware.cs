using GestionCursos.Models;

namespace GestionCursos.Middleware
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(ArgumentException ex)
            {
                context.Response.StatusCode = 400;
                var respuesta = new ResponseGenerico<string>
                {
                    Codigo = 400,
                    Message = "Error de validacion",
                    Response = ex.Message
                };
                await context.Response.WriteAsJsonAsync(respuesta);
            }
            catch(Exception ex) 
            {
                context.Response.StatusCode = 500;
                var respuesta = new ResponseGenerico<string>
                {
                    Codigo = 500,
                    Message = "Error Desconocido",
                    Response = ex.InnerException?.Message ?? ex.Message
                };
                await context.Response.WriteAsJsonAsync(respuesta);
            }
        }
    }
}
