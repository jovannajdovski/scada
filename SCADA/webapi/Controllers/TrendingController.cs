using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using webapi.Enum;
using webapi.model;
using Microsoft.EntityFrameworkCore;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("scada/trending")]
    public class TrendingController : ControllerBase
    {
        private readonly IDigitalInputService _digitalInputService;
        private readonly IDigitalOutputService _digitalOutputService;
        private readonly IAnalogInputService _analogInputService;
        private readonly IAnalogOutputService _analogOutputService;
        private readonly IIOAddressRepository _ioAddressRepository;
        private readonly IAlarmTriggerService _alarmTriggerService;

        public TrendingController(
            IDigitalInputService digitalInputService,
            IDigitalOutputService digitalOutputService,
            IAnalogInputService analogInputService,
            IAnalogOutputService analogOutputService,
            IIOAddressRepository ioAddressRepository,
            IAlarmTriggerService alarmTriggerService)
        {
            _digitalInputService = digitalInputService;
            _digitalOutputService = digitalOutputService;
            _analogInputService = analogInputService;
            _analogOutputService = analogOutputService;
            _ioAddressRepository = ioAddressRepository;
            _alarmTriggerService = alarmTriggerService;
        }

        [HttpGet()]
        public IActionResult GetAllTrending()
        {
            List<AnalogInput> analogInputs = _analogInputService.GetAllScanningAnalogInputs();
            List<DigitalInput> digitalInputs = _digitalInputService.GetAllScanningDigitalInputs();
            List<TrendingResponseDTO> trendingData = new List<TrendingResponseDTO>();

            foreach (var analogInput in analogInputs)
                trendingData.Add(new TrendingResponseDTO(analogInput, _alarmTriggerService.GetAnalogInputPriority(analogInput)));

            foreach (var digitalInput in digitalInputs)
                trendingData.Add(new TrendingResponseDTO(digitalInput));

            return Ok(trendingData);
        }
    }
}