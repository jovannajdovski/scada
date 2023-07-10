using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Enum;
using webapi.model;
using webapi.Model;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IAnalogInputService _analogInputService;
        private readonly IDigitalInputService _digitalInputService;
        private readonly ITagValueService _tagValueService;
        private readonly IAlarmService _alarmService;

        public ReportController(IAnalogInputService analogInputService,
            IDigitalInputService digitalInputService,
            ITagValueService tagValueService,
            IAlarmService alarmService)
        {
            _analogInputService = analogInputService;
            _digitalInputService = digitalInputService;
            _tagValueService = tagValueService;
            _alarmService = alarmService;
        }

        // Alarm reports

        [HttpGet("alarms")]
        public IActionResult GetAllAlarms(DateTime startTime, DateTime endTime, bool isAscending = true)
        {
            List<Alarm> alarms = _alarmService.GetAlarms(startTime, endTime, isAscending);
            return Ok(alarms);
        }

        [HttpGet("alarms/{priority}")]
        public IActionResult GetAlarmsByPriority(string priority, bool isAscending = true)
        {
            AlarmPriority priorityEnum;
            if (priority == "noraml")
            {
                priorityEnum = AlarmPriority.NORMAL_PRIORITY;
            } else if (priority == "high")
            {
                priorityEnum = AlarmPriority.HIGH_PRIORITY;
            } else
            {
                priorityEnum = AlarmPriority.LOW_PRIORITY;
            }
            List <Alarm> alarms = _alarmService.GetAlarmsByPriority(priorityEnum, isAscending);
            return Ok(alarms);
        }


        // Tag reports

        [HttpGet("tagvalues")]
        public IActionResult GetAllTagValues(DateTime startTime, DateTime endTime, bool isAscending = true)
        {
            List<TagValue> tagValues = _tagValueService.GetTagValues(startTime, endTime);

            if (isAscending)
            {
                tagValues.Sort((x, y) => x.Date.CompareTo(y.Date));
            }
            else
            {
                tagValues.Sort((x, y) => y.Date.CompareTo(x.Date));
            }

            return Ok(tagValues);
        }

        [HttpGet("analoginputs/last")]
        public IActionResult GetLastAnalogInputs(bool isAscending = true)
        {
            List<AnalogInput> analogInputs = _analogInputService.GetAllAnalogInputs();
            analogInputs = analogInputs.Where(ai => GetLastTagValue(ai.Id) != null).ToList();

            if (isAscending)
            {
                analogInputs.Sort((x, y) => GetLastTagValue(x.Id).Date.CompareTo(GetLastTagValue(y.Id).Date));
            }
            else
            {
                analogInputs.Sort((x, y) => GetLastTagValue(y.Id).Date.CompareTo(GetLastTagValue(x.Id).Date));
            }

            return Ok(analogInputs);
        }

        [HttpGet("digitalinputs/last")]
        public IActionResult GetLastDigitalInputs(bool isAscending = true)
        {
            List<DigitalInput> digitalInputs = _digitalInputService.GetAllDigitalInputs();
            digitalInputs = digitalInputs.Where(di => GetLastTagValue(di.Id) != null).ToList();

            if (isAscending)
            {
                digitalInputs.Sort((x, y) => GetLastTagValue(x.Id).Date.CompareTo(GetLastTagValue(y.Id).Date));
            }
            else
            {
                digitalInputs.Sort((x, y) => GetLastTagValue(y.Id).Date.CompareTo(GetLastTagValue(x.Id).Date));
            }

            return Ok(digitalInputs);
        }

        [HttpGet("tagvalues/{id}")]
        public IActionResult GetTagValuesById(int id, bool isAscending = true)
        {
            List<TagValue> tagValues = _tagValueService.GetTagValuesByTagId(id);

            if (isAscending)
            {
                tagValues.Sort((x, y) => x.Value.CompareTo(y.Value));
            }
            else
            {
                tagValues.Sort((x, y) => y.Value.CompareTo(x.Value));
            }

            return Ok(tagValues);
        }

        private TagValue GetLastTagValue(int tagId)
        {
            return _tagValueService.GetLastTagValue(tagId);
        }
    }
}
