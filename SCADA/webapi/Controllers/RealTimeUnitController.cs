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
    [Route("scada/rtu")]
    public class RealTimeUnitController : ControllerBase
    {
        private readonly IIOAddressRepository _ioAddressRepository;
        private readonly IRealTimeUnitRepository _realTimeUnitRepository;

        public RealTimeUnitController(
            IIOAddressRepository ioAddressRepository, IRealTimeUnitRepository realTimeUnitRepository)
        {
            _ioAddressRepository = ioAddressRepository;
            _realTimeUnitRepository = realTimeUnitRepository;
        }
        [HttpPost()]
        public IActionResult AddRTU(RtuModel model)
        {
            IOAddress address = _ioAddressRepository.GetById(model.AddressId);
            if (address == null)
                return BadRequest();

            RealTimeUnit realTimeUnit = new RealTimeUnit();
            realTimeUnit.SetProperties(model.HighLimit, model.LowLimit, address);
            _realTimeUnitRepository.Add(realTimeUnit);
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
