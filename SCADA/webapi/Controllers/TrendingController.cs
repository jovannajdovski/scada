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

namespace webapi.Controllers
{
    [ApiController]
    [Route("scada/trending")]
    public class TrendingWebSocketController : ControllerBase
    {

        private readonly ILogger<TrendingWebSocketController> _logger;

        public TrendingWebSocketController(ILogger<TrendingWebSocketController> logger)
        {
            _logger = logger;
        }
        [HttpGet()]
        public IActionResult GetAllTrending()
        {
            ScadaDBContext dBContext = new ScadaDBContext();
            List<AnalogInput> analogInputs = dBContext.AnalogInputs.Include(ai => ai.Address)
                .Where(ai => ai.IsScanning).ToList(); 
            List<DigitalInput> digitalInputs = dBContext.DigitalInputs.Include(ai => ai.Address)
                .Where(di => di.IsScanning).ToList();;

            List<TrendingResponse> trendingData = new List<TrendingResponse>();

            foreach (var analogInput in analogInputs)
            {
                trendingData.Add(new TrendingResponse(analogInput));
            }

            foreach (var digitalInput in digitalInputs)
            {
                trendingData.Add(new TrendingResponse(digitalInput));
            }
            
            return Ok(trendingData);
        }

    }
    public class TrendingResponse
    {
        public int id { get; set; }
        public string description { get; set; }
        public int address { get; set; }
        public string value { get; set; }
        public string limit { get; set; }
        public string unit { get; set; }
        public double scanTime { get; set; }
        public TrendingResponse(int id, string description, int address, string value, string limit, string unit)
        {
            this.id = id;
            this.description = description;
            this.address = address;
            this.value = value;
            this.limit = limit;
            this.unit = unit;
        }
        public TrendingResponse(AnalogInput analogInput)
        {
            this.id = analogInput.Id;
            this.scanTime=analogInput.ScanTime;
            this.description= analogInput.Description;
            this.address = analogInput.Address.Id;
            if (analogInput.Address.Type == "double")
                this.value = Math.Round(Double.Parse(analogInput.Address.Value), 4).ToString();
            else if (analogInput.Address.Type == null)
                this.value = "/";
            else
                this.value = analogInput.Address.Value;
            this.limit=analogInput.LowLimit.ToString()+" - "+analogInput.HighLimit.ToString();
            this.unit = analogInput.Unit;
        }
        public TrendingResponse(DigitalInput digitalInput)
        {
            this.id = digitalInput.Id;
            this.scanTime= digitalInput.ScanTime;
            this.description = digitalInput.Description;
            this.address = digitalInput.Address.Id;
            if (digitalInput.Address.Type == null)
                this.value = "/";
            else
                this.value = digitalInput.Address.Value;
            this.limit = "/";
            this.unit = "/";
        }
    }

}


