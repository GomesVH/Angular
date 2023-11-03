namespace Projeto.AspNet._05.APIControllers.Models
{
    // 1º passo: praticar o mecanismo de herança com a interface IRepository para que as instruções do CRUD sejam implementadas 
    // ESTA CLASSE É A ESTRUTURA DE ARMAZENAMENTO DA APLICAÇÃO 
    public class Repository : IRepository
    {
        // 2º passo: definir um Dictionary - coleção de dados baseados em pares chave-valor - para que os dados possam ser armazenados
        private Dictionary<int, Reserva> _dados;

        // 3º passo: definir o construtor da classe para que seja possivel "priorizar" o conteudo que deve constar da aplicação assim que este construtor for chamado. Também é importante acessar a prop private e atribuir à ela um valor especifico
        public Repository() {
            // agora, praticar a instancia da classe Dictionary para fazer uso do objeto
            _dados = new Dictionary<int, Reserva>();

            // neste passo, será definido um pequeno conjunto de dados - de forma inicial - para que já possam fazer parte da aplicação
            new List<Reserva>()
            {
                // criar um objeto de dados para cada registro
                new Reserva
                {
                    Id = 1,
                    Nome = "Cauê",
                    Sobrenome = "Gomes",
                    PontoA = "São Paulo",
                    PontoB = "Dubai"
                },
                new Reserva
                {
                    Id = 2,
                    Nome = "Gustavo",
                    Sobrenome = "Gomes",
                    PontoA = "Osasco",
                    PontoB = "Barueri"
                },
                new Reserva
                {
                    Id = 3,
                    Nome = "Lourenzo",
                    Sobrenome = "Gomes",
                    PontoA = "São Paulo",
                    PontoB = "Tapirai"
                },
                new Reserva
                {
                    Id = 4,
                    Nome = "Victor",
                    Sobrenome = "Gomes",
                    PontoA = "Barueri",
                    PontoB = "Rio de Janeiro"
                },
                new Reserva
                {
                    Id = 5,
                    Nome = "Wagner",
                    Sobrenome = "Gomes",
                    PontoA = "Itaqua",
                    PontoB = "Maldivas"
                }
            }.ForEach(r => AddReservation(r)); // aqui, esta em curso a chamada do método AddReservation. Agora, para que essa chamada funcione e os dados-padrão possam ser inseridos é preciso indicar o método aqui, no repositorio.
        }

         // 4º passo: é necessario acessar a prop private e referencia-la para que o processo de armazenamento funcione de forma adequada. Abaixo, esta em curso uma consulta que seleciona um registro e observa se ele possui um elemento identificador
         public Reserva this[int id] => _dados.ContainsKey(id) ?
            _dados[id] : null;

        // agora, é preciso observar se o mesmo registro - no formato de dicionario (Dictionary) - possui, tambem, um valor associado ao elemento chave: key-value
        public IEnumerable<Reserva> Reservas => _dados.Values;
        // no passo acima foi observado se o Dictionary esta, devidamente composto por itens baseados em pares Key:Value

        // 5º passo: consiste em definir/implementar as instruçoes que cômpoem o método de inserção de dados - AddReservation()
        public Reserva AddReservation(Reserva registroReserva)
        {
            // avaliar se o registro associado ao parametro do método - possui algum elemento identificador 
            if (registroReserva.Id == 0)
            {
                // definir uma prop para receber como valor o Dictionary _dados e fazer uma contagem dos pares chave: valor que o compõem
                int key = _dados.Count;

                // estabelecimento do loop while para que - a partir do valor atribuido a key - possa ser incrementado - de uma-em-uma unidade os valores de Id de cada registro 
                while (_dados.ContainsKey(key))
                {
                    key++;
                    
                }
                // o registro - em especifico a prop Id - recebe o valor do incremento da prop key
                registroReserva.Id = key;
            }
            // o dictionary receber a reserva com todos os dados que cômpoes - key/value 
            _dados[registroReserva.Id] = registroReserva;

            // abaixo é retornado o registro para o armazenamento
            return registroReserva;
        }

        // 6º passo: definir o método de atualização do registro
        public Reserva UpdateReservation(Reserva registroAtualizado)
            => AddReservation(registroAtualizado);

        // 7º passo: definir um método de exclusão de registro
        public void DeleteReservation(int id) => _dados.Remove(id);

    }
}
