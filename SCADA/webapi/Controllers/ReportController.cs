using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IAnalogOutputService _analogOutputService;
        private readonly IDigitalOutputService _digitalOutputService;
        private readonly IDigitalInputService _digitalInputService;
        private readonly ITagValueService _tagValueService;
        private readonly IAlarmService _alarmService;

        public ReportController(IAnalogInputService analogInputService,
            IDigitalInputService digitalInputService,
            IAnalogOutputService analogOutputService,
            IDigitalOutputService digitalOutputService,
            ITagValueService tagValueService,
            IAlarmService alarmService)
        {
            _analogInputService = analogInputService;
            _digitalInputService = digitalInputService;
            _tagValueService = tagValueService;
            _alarmService = alarmService;
            _analogOutputService = analogOutputService;
            _digitalOutputService = digitalOutputService;
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

            List<TagValueReportDTO> reportDTOs = new List<TagValueReportDTO>();

            foreach (TagValue tagValue in tagValues)
            {
                TagBase tag;
                tag = _digitalInputService.GetDigitalInputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Digital Input", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _digitalOutputService.GetDigitalOutputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Digital Output", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _analogOutputService.GetAnalogOutputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Analog Output", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _analogInputService.GetAnalogInputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Analog Input", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
            }

            return Ok(reportDTOs);
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
                tagValues.Sort((x, y) => {
                    if (x.Type.Equals("double"))
                    {
                        double xDouble, yDouble;
                        if (Double.TryParse(x.Value, out xDouble) && Double.TryParse(y.Value, out yDouble))
                        {
                            return xDouble.CompareTo(yDouble);
                        }
                        else
                        {
                            return x.Value.CompareTo(y.Value);
                        }
                    }
                    return x.Value.CompareTo(y.Value);
                    });
            }
            else
            {
                tagValues.Sort((x, y) => {
                    if (x.Type.Equals("double"))
                    {
                        double xDouble, yDouble;
                        if (Double.TryParse(x.Value, out xDouble) && Double.TryParse(y.Value, out yDouble))
                        {
                            return yDouble.CompareTo(xDouble);
                        }
                        else
                        {
                            return y.Value.CompareTo(x.Value);
                        }
                    }
                    return y.Value.CompareTo(x.Value);
                });
            }

            List<TagValueReportDTO> reportDTOs = new List<TagValueReportDTO>();

            foreach (TagValue tagValue in tagValues)
            {
                TagBase tag;
                tag = _digitalInputService.GetDigitalInputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Digital Input", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _digitalOutputService.GetDigitalOutputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Digital Output", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _analogOutputService.GetAnalogOutputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Analog Output", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
                tag = _analogInputService.GetAnalogInputById(tagValue.TagBaseId);
                if (tag != null)
                {
                    TagValueReportDTO reportDTO = new TagValueReportDTO(tagValue, "Analog Input", tag.Description);
                    reportDTOs.Add(reportDTO);
                    continue;
                }
            }

            return Ok(reportDTOs);
        }

        private TagValue GetLastTagValue(int tagId)
        {
            return _tagValueService.GetLastTagValue(tagId);
        }
    }
}
