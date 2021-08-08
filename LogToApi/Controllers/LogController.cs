using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogToApi.Common.Interfaces;
using LogToApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogToApi.Controllers
{
    [ApiController]
    [Route("log")]
    public class LogController : Controller
    {
        private readonly ILogParserService _logParserService;
        private readonly IMapper _mapper;
        private readonly ILogger<LogController> _logger;
        
        public LogController(
            ILogParserService logParserService,
            IMapper mapper,
            ILogger<LogController> logger)
        {
            _logParserService = logParserService;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IEnumerable<LogRecord>> Get([FromQuery] int? lastRecords = 10)
        {
            var result = await _logParserService.GetLog(lastRecords);

            return _mapper.Map<IEnumerable<LogRecord>>(result);
        }
    }
}