using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DTO;
using webapi.model;
using webapi.Model;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IDigitalInputService _digitalInputService;
        private readonly IDigitalOutputService _digitalOutputService;
        private readonly IAnalogInputService _analogInputService;
        private readonly IAnalogOutputService _analogOutputService;
        private readonly IIOAddressRepository _ioAddressRepository;
        private readonly ITagValueService _tagValueService;
        private readonly ITagProcessingService _tagProcessingService;

        public TagController(
            IDigitalInputService digitalInputService,
            IDigitalOutputService digitalOutputService,
            IAnalogInputService analogInputService,
            IAnalogOutputService analogOutputService,
            IIOAddressRepository ioAddressRepository,
            ITagValueService tagValueService,
            ITagProcessingService tagProcessingService)
        {
            _digitalInputService = digitalInputService;
            _digitalOutputService = digitalOutputService;
            _analogInputService = analogInputService;
            _analogOutputService = analogOutputService;
            _ioAddressRepository = ioAddressRepository;
            _tagValueService = tagValueService;
            _tagProcessingService = tagProcessingService;
        }

        // DigitalInput
        [HttpGet("DigitalInputs")]
        public ActionResult<IEnumerable<DigitalInput>> GetDigitalInputs()
        {
            var digitalInputs = _digitalInputService.GetAllDigitalInputs();
            return Ok(digitalInputs);
        }

        [HttpGet("DigitalInputs/{id}")]
        public ActionResult<DigitalInput> GetDigitalInput(int id)
        {
            var digitalInput = _digitalInputService.GetDigitalInputById(id);
            if (digitalInput == null)
            {
                return NotFound();
            }
            return digitalInput;
        }

        [HttpPost("DigitalInputs")]
        public ActionResult<DigitalInput> CreateDigitalInput(DigitalInputCreateDTO digitalInputDTO)
        {
            IOAddress ioAddress = _ioAddressRepository.GetById(digitalInputDTO.AddressId);
            if (ioAddress != null)
            {

                var digitalInput = new DigitalInput
                {
                    Description = digitalInputDTO.Description,
                    ScanTime = digitalInputDTO.ScanTime,
                    Address = ioAddress,
                    IsScanning = true,
                    Values = new List<TagValue>()
                };

                _digitalInputService.CreateDigitalInput(digitalInput);
                _tagProcessingService.CreateDigitalTimer(digitalInput);
                return Ok(digitalInput);
            }
            else
                return NotFound("Address not found");
        }

        [HttpDelete("DigitalInputs/{id}")]
        public IActionResult DeleteDigitalInput(int id)
        {
            _digitalInputService.DeleteDigitalInput(id);
            _tagProcessingService.QuitTimer(id);
            return NoContent();
        }

        [HttpPut("DigitalInputs/{id}/IsScanning")]
        public IActionResult UpdateDigitalInputScanning(int id, ScanDTO scan)
        {
            var digitalInput = _digitalInputService.GetDigitalInputById(id);
            if (digitalInput == null)
            {
                return NotFound();
            }

            digitalInput.IsScanning = scan.IsScanning;
            _digitalInputService.UpdateDigitalInput(digitalInput);
            if (scan.IsScanning)
                _tagProcessingService.CreateDigitalTimer(digitalInput);
            else
                _tagProcessingService.QuitTimer(digitalInput.Id);


            return NoContent();
        }

        // DigitalOutput
        [HttpGet("DigitalOutputs")]
        public ActionResult<IEnumerable<DigitalOutput>> GetDigitalOutputs()
        {
            var digitalOutputs = _digitalOutputService.GetAllDigitalOutputs();
            var digitalOutputsToSend = digitalOutputs.Select(d => new
            {
                description = d.Description,
                id = d.Id,
                lastValue = d.Values.Count == 0 ? "-": d.Values[d.Values.Count - 1].Value,
            }).ToList();

            return Ok(digitalOutputsToSend);
        }

        [HttpGet("DigitalOutputs/{id}")]
        public ActionResult<DigitalOutput> GetDigitalOutput(int id)
        {
            var digitalOutput = _digitalOutputService.GetDigitalOutputById(id);
            if (digitalOutput == null)
            {
                return NotFound();
            }
            return digitalOutput;
        }

        [HttpPost("DigitalOutputs")]
        public ActionResult<DigitalOutput> CreateDigitalOutput(DigitalOutputCreateDTO digitalOutputDTO)
        {
            IOAddress ioAddress = _ioAddressRepository.GetById(digitalOutputDTO.AddressId);
            if (ioAddress != null)
            {
                List<TagValue> tagValues = new List<TagValue>();
                TagValue tagValue = new TagValue();
                tagValue.Date = DateTime.Now;
                tagValue.Value = digitalOutputDTO.InitialValue == Enum.DigitalValueType.ON ? "true" : "false";  
                tagValue.Type = "boolean";
                tagValues.Add(tagValue);
                var digitalOutput = new DigitalOutput
                {
                    Description = digitalOutputDTO.Description,
                    Address = ioAddress,
                    Values = tagValues
                };

                _digitalOutputService.CreateDigitalOutput(digitalOutput);
                var digitalOutputToSend = new
                {
                    description = digitalOutput.Description,
                    id = digitalOutput.Id,
                    lastValue = digitalOutput.Values.Count == 0 ? "-" : digitalOutput.Values[digitalOutput.Values.Count - 1].Value
                };

                return Ok(digitalOutputToSend);
            }
            else
                return NotFound("Address not found");


        }
        [HttpPost("DigitalOutputs/value")]
        public ActionResult<DigitalOutput> AddDigitalOutputValue(OutputTagNewValueDTO outputTagNewValue)
        {
            DigitalOutput digitalOutput = _digitalOutputService.GetDigitalOutputById(outputTagNewValue.Id);
            if (digitalOutput != null)
            {
                _digitalOutputService.AddNewValue(outputTagNewValue.Id, outputTagNewValue.Value);
                return Ok();
            }
            else
                return NotFound("Tag not found");
        }
        [HttpDelete("DigitalOutputs/{id}")]
        public IActionResult DeleteDigitalOutput(int id)
        {
            _digitalOutputService.DeleteDigitalOutput(id);
            return NoContent();
        }

        // AnalogInput
        [HttpGet("AnalogInputs")]
        public ActionResult<IEnumerable<AnalogInput>> GetAnalogInputs()
        {
            var analogInputs = _analogInputService.GetAllAnalogInputs();
            return Ok(analogInputs);
        }

        [HttpGet("AnalogInputs/{id}")]
        public ActionResult<AnalogInput> GetAnalogInput(int id)
        {
            var analogInput = _analogInputService.GetAnalogInputById(id);
            if (analogInput == null)
            {
                return NotFound();
            }
            return analogInput;
        }

        [HttpPost("AnalogInputs")]
        public ActionResult<AnalogInput> CreateAnalogInput(AnalogInputCreateDTO analogInputDTO)
        {
            IOAddress ioAddress = _ioAddressRepository.GetById(analogInputDTO.AddressId);
            if (ioAddress != null)
            {
                var analogInput = new AnalogInput
                {
                    Description = analogInputDTO.Description,
                    ScanTime = analogInputDTO.ScanTime,
                    LowLimit = analogInputDTO.LowLimit,
                    HighLimit = analogInputDTO.HighLimit,
                    Unit = analogInputDTO.Unit,
                    Address = ioAddress,
                    IsScanning = true,
                    Values = new List<TagValue>()
                };

                _analogInputService.CreateAnalogInput(analogInput);
                _tagProcessingService.CreateAnalogTimer(analogInput);
                return Ok(analogInput);
            }
            else
                return NotFound("Address not found");
            
        }

        [HttpDelete("AnalogInputs/{id}")]
        public IActionResult DeleteAnalogInput(int id)
        {
            _analogInputService.DeleteAnalogInput(id);
            _tagProcessingService.QuitTimer(id);
            return NoContent();
        }

        [HttpPut("AnalogInputs/{id}/IsScanning")]
        public IActionResult UpdateAnalogInputScanning(int id, ScanDTO scan)
        {
            var analogInput = _analogInputService.GetAnalogInputById(id);
            if (analogInput == null)
            {
                return NotFound();
            }


            analogInput.IsScanning = scan.IsScanning;
            _analogInputService.UpdateAnalogInput(analogInput);
            if (scan.IsScanning)
                _tagProcessingService.CreateAnalogTimer(analogInput);
            else
                _tagProcessingService.QuitTimer(analogInput.Id);
            return NoContent();
        }


        // AnalogOutput
        [HttpGet("AnalogOutputs")]
        public ActionResult<IEnumerable<AnalogOutput>> GetAnalogOutputs()
        {
            var analogOutputs = _analogOutputService.GetAllAnalogOutputs();
            var analogOutputsToSend = analogOutputs.Select(d => new
            {
                description = d.Description,
                highLimit = d.HighLimit,
                lowLimit = d.LowLimit,
                id = d.Id,
                unit = d.Unit,
                lastValue = d.Values.Count == 0 ? "-" : d.Values[d.Values.Count - 1].Value,
            }).ToList();

            return Ok(analogOutputsToSend);
        }

        [HttpGet("AnalogOutputs/{id}")]
        public ActionResult<AnalogOutput> GetAnalogOutput(int id)
        {
            var analogOutput = _analogOutputService.GetAnalogOutputById(id);
            if (analogOutput == null)
            {
                return NotFound();
            }
            return analogOutput;
        }

        [HttpPost("AnalogOutputs")]
        public ActionResult<AnalogOutput> CreateAnalogOutput(AnalogOutputCreateDTO analogOutputDTO)
        {
            IOAddress ioAddress = _ioAddressRepository.GetById(analogOutputDTO.AddressId);
            if (ioAddress != null)
            {
                List<TagValue> tagValues = new List<TagValue>();
                TagValue tagValue = new TagValue();
                tagValue.Date = DateTime.Now;
                tagValue.Value = analogOutputDTO.InitialValue.ToString();
                tagValue.Type = "double";
                tagValues.Add(tagValue);
                var analogOutput = new AnalogOutput
                {
                    Description = analogOutputDTO.Description,
                    LowLimit = analogOutputDTO.LowLimit,
                    HighLimit = analogOutputDTO.HighLimit,
                    Unit = analogOutputDTO.Unit,
                    Address = ioAddress,
                    Values = tagValues
                };

                _analogOutputService.CreateAnalogOutput(analogOutput);
                var analogOutputToSend = new
                {
                    description = analogOutput.Description,
                    id = analogOutput.Id,
                    lowLimit = analogOutputDTO.LowLimit,
                    highLimit = analogOutputDTO.HighLimit,
                    unit = analogOutputDTO.Unit,
                    Address = ioAddress,
                    lastValue = analogOutput.Values.Count == 0 ? "-" : analogOutput.Values[analogOutput.Values.Count - 1].Value
                };
                return Ok(analogOutputToSend);
            }
            else
                return NotFound("Address not found");
            
        }

        [HttpPost("AnalogOutputs/value")]
        public ActionResult<DigitalOutput> AddAnalogOutputValue(OutputTagNewValueDTO outputTagNewValue)
        {
            AnalogOutput analogOutput = _analogOutputService.GetAnalogOutputById(outputTagNewValue.Id);
            if (analogOutput != null)
            {
                _analogOutputService.AddNewValue(outputTagNewValue.Id, outputTagNewValue.Value);
                return Ok();
            }
            else
                return NotFound("Tag not found");
        }
        [HttpDelete("AnalogOutputs/{id}")]
        public IActionResult DeleteAnalogOutput(int id)
        {
            _analogOutputService.DeleteAnalogOutput(id);
            return NoContent();
        }
    }
}
