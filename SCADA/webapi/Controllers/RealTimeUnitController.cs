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
    [Route("scada/rtu")]
    public class RealTimeUnitController : ControllerBase
    {
        private readonly ILogger<RealTimeUnitController> _logger;

        public RealTimeUnitController(ILogger<RealTimeUnitController> logger)
        {
            _logger = logger;
        }
        [HttpPost()]
        public IActionResult AddRTU(RtuModel model)
        {
            ScadaDBContext dbc = new ScadaDBContext();
            DbSet<IOAddress> addresses = dbc.Addresses;
            IOAddress address = addresses.FirstOrDefault(a=> a.Id==model.AddressId);
            if (address == null)
                return BadRequest();
            RealTimeUnit realTimeUnit = new RealTimeUnit();
            realTimeUnit.SetProperties(model.HighLimit, model.LowLimit, address);
            Console.WriteLine(address.Id);
            
            dbc.RealTimeUnits.Add(realTimeUnit);
            dbc.SaveChanges();
            return Ok();
        }

    }
    public class RtuModel
    {
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public int AddressId { get; set; }
    }
}
