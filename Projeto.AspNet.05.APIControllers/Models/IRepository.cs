namespace Projeto.AspNet._05.APIControllers.Models
{
    // esta interface será responsavel por fazer uso do model e estabelecer os métodos de manipulação dos dados a partir do CRUD - Create, Read, Update, Delete 
    public interface IRepository
    {
        // 1º passo: os dados de registro de reserva precisam ser indicados como elementos enumeraveis
        IEnumerable<Reserva> Reservas { get; } // este é a instrução que recupera todos os dados da estrutura de armazenamento
        Reserva this[int id] { get; } // esta é a instrução que recupera um unico registro - da estrutura de armazenamento - desde que esteja devidamente identificado

        Reserva AddReservation(Reserva registroReserva); // esta instrução é responsavel por inserir um registro na estrutura de armazenamento

        Reserva UpdateReservation(Reserva registroAtualizado); // esta instrução é responsavel por atualizar um registro da estrutura de armazenamento

        void DeleteReservation(int id); // esta instrução é responsavel por excluir um registro da estrutura de armazenamento - aqui, o método é definido como void porque será apenas um ação a ser executada (sem necessidade 
    }
}
