using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Projeto.AspNet._05.WebAPI.Front.Models;
using System.Text;


namespace Projeto.AspNet._05.WebAPI.Front.Controllers
{
    // AGORA, ABAIXO SERÃO DEFINIDAS AS OPERAÇÕES - NO FRONT-END - QUE ESTABELECE O FLUXO DE DADOS - FAZENDO USO ( CONSUMINDO ) DA API
    public class HomeController : Controller
    {
        /*
       ========================================================================================
           DEFINIÇÃO DAS OPERAÇÕES DE CRUD - NO FRONT - ACESSANDO 
                   (CONSUMINDO) A API - a ideia é que, aqui, no front 
                    os dados sejam recebidos e, posteriormente, 
                    manipulados de acordo com a funcionalidade de cada action/view
       =======================================================================================
        */
        // 1º tarefa CRUD - Get: tarefa assincrona que recupera todos os dados de armazenamento
        // aqui, o atributo [HttpPost] esta implicito
        public async Task<IActionResult> Index()
        {



            // 1º passo: recuperar todos os dados da estrutura de armazenamento. Para atingir este objetivo será preciso criar uma action que faça a referencia adequada a API
            List<Reserva> ListaReserva = new List<Reserva>();

            // 1ºA passo: consiste em definir um objeto que auxilie na criação da requisição que recuperará os dados da estrutura de armazenamento
            using (var clientHttp = new HttpClient())
            {
                // montar a requisição http para acessar a API e recuperar os dados da estrutura de armazenamento
                using (var resposta = await clientHttp.GetAsync("http://localhost:5281/api/Reservas"))
                {
                    // a requisição http se chama resposta. Agora, é necessario fazer uso desta requisição para ler os dados armazenados 
                    string apiResposta = await resposta.Content.ReadAsStringAsync();

                    // estabelecer, uma vez que os dados - em tese - ja foram "lidos", a deserialização do conteudo para este objetivo será acessado a prop listaReserva para receber como valor os dados desserializados

                    ListaReserva = JsonConvert.DeserializeObject<List<Reserva>>
                    (apiResposta);
                }
            }

            return View(ListaReserva);
        }

        // 2º tarefa CRUD - Get: tarefa assincrona que recupera um unico registro da estrutura de armazenamento - desde que esteja devidamente identificado 
        // definir o retorno da view para o registro
        public ViewResult GetReservation() => View(); // aqui, esta estabelecida a view para que o registro seja devidamente selecionado e, posteriormente, exibido como registro unico.

        // 2ºA passo: estabelecer a sobrecarga da action/método Get Reservation para a exibição do registro selecionado.
        [HttpPost] // atributo/requisição que possibilita o envio de dados para a estrutura de armazenamento - através do acesso a API. Ele esta aqui definindo uma action GetReservation porque será preciso inserir um id num input de texto para,então, seleciona-lo
        public async Task<IActionResult> GetReservation(int id)
        {
            // praticar uma instancia para gerar um objeto direto - a partir da classe Reserva (o model)
            Reserva reserva = new Reserva();

            // praticar a instancia da classe HttpClient() para gerar o objeto que fará parte da montagem da requisição
            using (var clientHttp = new HttpClient())
            {
                // montar a requisição http para acessar a API e recuperar o registro - devidamente identificado - da estrutura de armazenamento
                using (var resposta = await clientHttp.GetAsync("http://localhost:5281/api/Reservas/" + id))
                {
                    // acima, a requisição esta montada. Agora, é necessario observar se a resposta deseja - o acesso ao registro existente e identificado - foi obtida
                    if (resposta.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // a requisição http se chama resposta. Agora, é necessario fazer uso desta requisição para ler os dados armazenados 
                        string apiResposta = await resposta.Content.ReadAsStringAsync();

                        // estabelecer, uma vez que os dados - em tese - ja foram "lidos", a deserialização do conteudo para este objetivo será acessado a prop listaReserva para receber como valor os dados desserializados
                        reserva = JsonConvert.DeserializeObject<Reserva>
                            (apiResposta);
                    }
                    else
                    {
                        ViewBag.StatusCode = resposta.StatusCode;
                        // aqui, a var resposta traz e atribui como valor o StatusCode gerado pela requisição montada acima
                    }
                }
                return View(reserva);
            }
        }
        // 3º tarefa CRUD - Post: tarefa assincrona que envia dados obtidos pela view para a estrutura de armazenamento
        // o primeiro movimento é retornar a view para que seja possivel deixa-la a disposição para a inserção de dados 
        // esta chamada do método somente "mostra/monta" a tela do browser
        public ViewResult AddReservation() => View();
        // 1º passo: definir a sobrecarga do método/action para as instruções de inserção de dados
        [HttpPost] // atributo/requisição para a inserção de dados
        public async Task<IActionResult> AddReservation(Reserva insercaoRegistro)
        {
            // 2º passo: consiste em gerar um objeto - a partir da pratica da instancia da classe/model Reserva
            Reserva reservaRecebida = new Reserva();
            // 3º passo: definir o objeto da classe HttpClient para que seja possivel criar a requisição de forma adequada                                                       
            using (var clientHttp = new HttpClient())
            {
                // 4º passo: consiste em - uma vez que os dados foram recebidos - criar uma instrução para serializar e "empacotar" os dados no formato adequado para serem transportados para o back-end; a var conteudo é o pacote de dados formatado
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(insercaoRegistro),
                    Encoding.UTF8, "application/json");
                // 5º passo: consiste em ter a possibilidade de fazer leitura - na mesma view -dos dados que forma inseridos; a requisição http de envio de dados - acessando o objeto clientHttp e criando para auxiliar a criação da requisiçao
                using (var resposta = await clientHttp.PostAsync("http://localhost:5281/api/Reservas", conteudo)) // aqui, neste passo, os dados forma ja inseridos
                {
                    // a requisição http se chama resposta. Agora, é necessario fazer uso desta requisição para inserir os dados na estrutura de armazenados 
                    string apiResposta = await resposta.Content.ReadAsStringAsync();

                    // acessar o objeto criado a partir da instancia da classe/model Reserva - reservaRecebida
                    reservaRecebida = JsonConvert.DeserializeObject<Reserva>(apiResposta);

                }
            }
            return View(reservaRecebida);
        }
        // 4º passo CRUD - PUT/POST: tarefa assincrona que envia dados obtidos pela view para a estrutura de armazenamento agora, este registro sera "gerado" a partir de um registro ja existente  **                                                                          
        //o primeiro movimento é retornar a view para que seja possivel deixa-la a disposição para atualização/alteração de dados ***                                                              
        // esta chamada de método somente "mostra/monta" a tela no browser - mas, agora, com o conjunto de dados que foi selecionado para atualização/alteração **                          
        // esta action/método tambem precisa ser, diferentemente da action/método acima, uma tarefa assincrona ***
        public async Task<IActionResult> UpdateReservation(int id)
        {
            // 1º passo - disponibilizar os dados para a view: a geração de um objeto, a partir classe/model Reserva, para que os dados juntamente com seus respectivos valores - possam ser disponibilizados para view
            Reserva dadosObtidosBase = new Reserva();

            // 2º passo: montar a requisição - à base - para que, agora, seja possivel disponibilizar os dados na view
            using (var clientHttp = new HttpClient())
            {
                // 3º passo: montando a requisição 
                // http
                using (var resposta = await clientHttp.GetAsync("http://localhost:5281/api/Reservas/" + id))
                {
                    // 4º passo: precisa definir uma response (resposta da requisição que fi feita para a API )
                    string apiResposta = await resposta.Content.ReadAsStringAsync();                                    
                    // 5º passo: acessar o objeto dadosObtidosBase - para que seja possivel retornar na View(), o conteudo recuperado na estrutura de armazenamento, de acordo com as props do model
                    dadosObtidosBase = JsonConvert.DeserializeObject<Reserva> (apiResposta);

                }
            }

            // 7º passo: os dados são, neste momento, disponibilizados através do retorno da view.
            return View(dadosObtidosBase);
        }

        // 8º passo: definir a sobrecarga do método/action para as instruções de atualização/alteração de dados
        [HttpPost] // qual é o atributo adequado para definir a action abaixo ? R: [HttpPost]. Porque esta proxima action somente envia os dados para a API
        public async Task<IActionResult> UpdateReservation(Reserva dadosAtualizar)
        {
            // 1º passo: dentro do method/action overloading: gerar um novo objeto - a partir da classe/model Reserva. Para que seja reenvia-los para a base; e, tambem disponibiliza-los na view().
            Reserva reservaAtualizada = new Reserva();

            // 2º passo: criar a requisição de atualização dos dados - mas, estas instruções sejam implementadas num segundo momento. Neste primeiro momento é preciso indicar que vai ocorrer a atualização de dados disponiveis para a action. Como fazer a atualização dos dados?
            using (var clientHttp = new HttpClient())
            {
                // primeiro momento: fazer a atualização dos dados                                  como fazer ? R: definir uma prop para receber como valor uma instancia da classe embarcada/nativa                                                                 MultipartFormDataContent();
                var alterandoDados = new MultipartFormDataContent();

                // fazer uso do objeto e acessar o recurso adequado para alterar/atualizar os valores de props de dados ja existentes 

                alterandoDados.Add(new StringContent                      (dadosAtualizar.Id.ToString()),"Id");

                alterandoDados.Add(new StringContent                            (dadosAtualizar.Nome), "Nome");

                alterandoDados.Add(new StringContent                            (dadosAtualizar.Sobrenome), "Sobrenome");

                alterandoDados.Add(new StringContent                           (dadosAtualizar.PontoA), "PontoA");

                alterandoDados.Add(new StringContent                        (dadosAtualizar.PontoB), "PontoB");

                // segundo momento: criar a requisição à API para que seja possivel reenviar os dados para a estrutura de armazenamento
                using (var resposta = await clientHttp.PutAsync                                  ("http://localhost:5281/api/Reservas", alterandoDados))
                {
                    // terceiro momento: consiste em trazer os dados para serem, em tese, retornados para a view.
                    string apiResposta = await                        resposta.Content.ReadAsStringAsync();

                    ViewBag.Result = "Success";

                    reservaAtualizada = JsonConvert.DeserializeObject<Reserva>             (apiResposta);
                }
            }
            return View(reservaAtualizada);
        }

        // 9º passo: definir a action que praticará a exclusão de um determinado registro - armazenado e ideentificado da base

        [HttpPost] // atributo de envio de dados
        public async Task<IActionResult> DeleteReservation(int IdReserva)
        {
            // 1º passo: consiste em gerar o objeto que auxilia na criação da requisiçao à API
            using (var clientHttp = new HttpClient())
            {
                using (var resposta = await clientHttp.DeleteAsync("http://localhost:5281/api/Reservas/" + IdReserva))
                {
                    string apiResposta = await                            resposta.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}