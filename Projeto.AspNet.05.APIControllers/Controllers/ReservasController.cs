using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._05.APIControllers.Models;

namespace Projeto.AspNet._05.APIControllers.Controllers
{

    [ApiController] // este é o atributo que defini o "papel" deste controller para a aplicação - ser uma WebAPI. está sera uma RestFul. Serão usadas requisições HTTP.

    // será importante indicar - quando necessario - o "caminho" pelo qual cada requisição estabelecerá seu fluxo de dados: são as rotas !
    [Route("api/[controller]")] // aqui, o atributo Route indica qual a rota que precisa ser referenciada para que a WebAPI possa funcionar.
    public class ReservasController : ControllerBase // a pratica do mecanismo de herança com a superClasse ControllerBase é exercida porque - para esta parte do projeto - o controller não se relaciona com as views 
    {
        // 1º PASSO: é necessario definir os seguintes recursos: um objeto referencial para lidar com as instruções de CRUD.                                                                 Dessa forma, será possivel ter o auxilio deste objeto para acessarmos os recursos descritos na interface
        private IRepository _repositorio;

        // 2º PASSO: definir o construtor da classe e praticar a injeção de dependencia com o objeto referencial 
        public ReservasController(IRepository repo) => _repositorio = repo;

        /*
         =======================================================================================
            IMPLEMENTAÇÃO DAS OPERAÇÕES CRUD - CREATE, READ, UPDATE, PATCH, DELETE
         =======================================================================================
        */

        // 3º PASSO: implementação da requisição - Get - que recupera todos os dados da estrutura de armazenamento
        [HttpGet]
        public IEnumerable<Reserva> Get() => _repositorio.Reservas;

        // 4º PASSO: implementação da requisição - Get - que recupera um unico registro da base - desde que esteja devidamente identificado
        // http:xxxxx/api/Reservas/4
        [HttpGet("{id}")] // atributo [HttpGet("{id}") é definido com o elemento identificador do registro para que seja devidamente selecionado
        // o uso do tipo ActionResult - diferentemente de IActionResult - é colocado pois estas requisições não retornam Views();
        public ActionResult<Reserva> Get(int id)
        {
            // avaliar se o registro selecionado, realmente, existe.
            if (id == 0) // TRUE
            {
                return BadRequest("Algum valor de identificação deve ser passado como elemento da requisição.");
            }
            return Ok(_repositorio[id]);
        }

        // 5º passo: implementar a requisição que insere dados na base. Para este proposito será usado o atribudo [HttpPost]
        [HttpPost]
        // [FromBody] é um atributo de definição de local onde um valor para um parametro qualquer deve ser obtido - qual é o local onde se encontram os valores da requisição? R: no corpo(Body) desta mesma requisição 
        public Reserva Post([FromBody] Reserva registro) => _repositorio.AddReservation(new Reserva
            {
                Nome = registro.Nome,
                Sobrenome = registro.Sobrenome,
                PontoA = registro.PontoA,
                PontoB = registro.PontoB,
            });

        // 6º passo: definir a requisição de atualização dos dados desde que estejam armazenados e devidamente identificado 
        [HttpPut] // atributo/requisição responsavel por possibilitar a atualização de dados da estrutura de armazenamento
        // ao utilizar o atributo [FromForm] a obtenção dos dados terá origem em um formulario
        public Reserva Put([FromForm] Reserva registroAlt) => _repositorio.UpdateReservation(registroAlt);

        // 7º passo: definir a requsição de atualização de parte dos dados - essa parte dos dados que serão atualizados pode ocorrer em funçao do uso da requisição [HttpPatch]. Neste caso, fazendo uso da requisição HttpPatch, está sendo observado o conteudo de dados que estão em atualização. A partir do "pedaço" que o identifica (id) é possivel observar se o conteudo estão seguindo as regras do model se está no formato adequado para ser transportado. 
        [HttpPatch("{id}")] // método/requisição para observar um unico "pedaço" de atualização de dados
        public StatusCodeResult Patch(int id, [FromBody]
        JsonPatchDocument<Reserva> patch)
        {
            // definir uma var para receber como resultado...
            var res = (Reserva)((OkObjectResult)Get(id).Result).Value;
            // avaliar o valor da variavel res
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        // 8º passo: definir a requisição que exclui um registro - devidamente identificado
        [HttpDelete("{id}")]
        public void Delete(int id) => _repositorio.DeleteReservation(id);


    }
}