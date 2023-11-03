namespace Projeto.AspNet._05.WebAPI.Front.Models
{
    // esta classe é o model domain da front-end. Possui o mesmo nome do model domain definido no back-end. Dessa forma, é possivel estabelecer uma forma de operação 
    public class Reserva 
    {
        // definir as props do model
      public int Id { get; set; }
      public string? Nome { get; set; }
      public string? Sobrenome { get; set; }
      public string? PontoA { get; set; }
      public string? PontoB { get; set; }
    }
}
