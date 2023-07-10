using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
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

            List<AlarmReportDTO> reportDTOs = new List<AlarmReportDTO>();

            foreach (Alarm alarm in alarms)
            {
                AlarmReportDTO reportDTO = new AlarmReportDTO(alarm);
                reportDTOs.Add(reportDTO);
            }
            return Ok(reportDTOs);
        }

        [HttpGet("alarms/priority")]
        public IActionResult GetAlarmsByPriority(int priority, bool isAscending = true)
        {
            AlarmPriority priorityEnum;
            if (priority == 1)
            {
                priorityEnum = AlarmPriority.NORMAL_PRIORITY;
            } else if (priority == 2)
            {
                priorityEnum = AlarmPriority.HIGH_PRIORITY;
            } else if (priority == 0)
            {
                priorityEnum = AlarmPriority.LOW_PRIORITY;
            } else
            {
                return BadRequest();
            }
            List <Alarm> alarms = _alarmService.GetAlarmsByPriority(priorityEnum, isAscending);
            List<AlarmReportDTO> reportDTOs = new List<AlarmReportDTO>();

            foreach (Alarm alarm in alarms)
            {
                AlarmReportDTO reportDTO = new AlarmReportDTO(alarm);
                reportDTOs.Add(reportDTO);
            }
            return Ok(reportDTOs);
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

            List<AnalogInputReportDTO> reportDTOs = new List<AnalogInputReportDTO>();

            foreach (AnalogInput analogInput in analogInputs)
            {
                TagValue lastTagValue = GetLastTagValue(analogInput.Id);

                AnalogInputReportDTO reportDTO = new AnalogInputReportDTO(analogInput, lastTagValue);
                reportDTOs.Add(reportDTO);
            }

            if (isAscending)
            {
                reportDTOs.Sort((x, y) => x.Date.CompareTo(y.Date));
            }
            else
            {
                reportDTOs.Sort((x, y) => y.Date.CompareTo(x.Date));
            }

            return Ok(reportDTOs);
        }

        [HttpGet("digitalinputs/last")]
        public IActionResult GetLastDigitalInputs(bool isAscending = true)
        {
            List<DigitalInput> digitalInputs = _digitalInputService.GetAllDigitalInputs();
            digitalInputs = digitalInputs.Where(di => GetLastTagValue(di.Id) != null).ToList();

            List<DigitalInputReportDTO> reportDTOs = new List<DigitalInputReportDTO>();

            foreach (DigitalInput digitalInput in digitalInputs)
            {
                TagValue lastTagValue = GetLastTagValue(digitalInput.Id);

                DigitalInputReportDTO reportDTO = new DigitalInputReportDTO(digitalInput, lastTagValue);
                reportDTOs.Add(reportDTO);
            }

            if (isAscending)
            {
                reportDTOs.Sort((x, y) => x.Date.CompareTo(y.Date));
            }
            else
            {
                reportDTOs.Sort((x, y) => y.Date.CompareTo(x.Date));
            }

            return Ok(reportDTOs);
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
