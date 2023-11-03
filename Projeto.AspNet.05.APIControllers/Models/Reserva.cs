namespace Projeto.AspNet._05.APIControllers.Models
{
    // esta classe é o model domain da aplicação. É responsavel por estabelecer o "formato" com a qual os dados serão operados
    public class Reserva
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? PontoA { get; set; }
        public string? PontoB { get; set; }
    }
}
