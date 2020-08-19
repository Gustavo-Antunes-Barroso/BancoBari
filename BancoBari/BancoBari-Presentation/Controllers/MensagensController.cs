using System;
using System.Threading.Tasks;
using BancoBari_Application.Interfaces.Mensagem;
using BancoBari_Domain.Dto.Mensagem;
using Microsoft.AspNetCore.Mvc;

namespace BancoBari_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagensController : ControllerBase
    {
        private readonly IMensagensAppServices _mensagensService;
        public MensagensController(IMensagensAppServices mensagensService)
        {
            _mensagensService = mensagensService;
        }

        [HttpGet, Route("{idMensagem}")]
        public async Task<IActionResult> SelecionarMensagem([FromRoute]Guid idMensagem)
        {
            var response = await _mensagensService.Selecionar(idMensagem);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> SelecionarTodosNaoIntegrados()
        {
            var response = await _mensagensService.SelecionarTodosNaoIntegrados();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarMensagem([FromBody] MensagemDto request)
        {
            var response = await _mensagensService.Atualizar(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> InserirMensagem([FromBody] MensagemDto request)
        {
            var response = await _mensagensService.Inserir(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> ApagarMensagem(Guid idMensagem)
        {
            var response = await _mensagensService.Excluir(idMensagem);
            return Ok(response);
        }
    }
}