namespace GestionCursos.Models
{
    public class ResponseGenerico<T>
    {
        public int Codigo { get;set;  }
        public string Message { get; set; }
        public T Response { get; set; }
    }
}
