using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoBari.Subscriber_Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoBari.Subscriber_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueuedAppService _queuedAppService;
        public QueueController(IQueuedAppService queuedAppService)
        {
            _queuedAppService = queuedAppService;
        }

        [HttpGet]
        public async Task<IActionResult> SelecionarQuantidadeMensagens()
        {
            var response = await _queuedAppService.SelecionarQuantidadeMensagens();
            return Ok(response);
        }
    }
}