using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
using webapi.Enum;
using webapi.model;
using webapi.Model;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/alarms")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private IAlarmService _alarmService;
        private IAlarmTriggerRepository _alarmTrggerRepository;

        public AlarmController(IAlarmService alarmService, IAlarmTriggerRepository alarmTriggerRepository)
        {
            _alarmService = alarmService;
            _alarmTrggerRepository = alarmTriggerRepository;
        }

        [HttpPut("/mute/{id}")]
        public ActionResult<Alarm> MuteAlarm(int id)
        {
            var alarm = _alarmService.Mute(id);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult<Alarm> NewAlarm(AlarmCreateDTO alarmCreateDTO)
        {
            AlarmPriority priorityEnum;
            if (alarmCreateDTO.Priority == 1)
            {
                priorityEnum = AlarmPriority.NORMAL_PRIORITY;
            }
            else if (alarmCreateDTO.Priority == 2)
            {
                priorityEnum = AlarmPriority.HIGH_PRIORITY;
            }
            else if (alarmCreateDTO.Priority == 0)
            {
                priorityEnum = AlarmPriority.LOW_PRIORITY;
            }
            else
            {
                return BadRequest();
            }

            AlarmType typeEnum;
            if (alarmCreateDTO.Type == 1)
            {
                typeEnum = AlarmType.HIGH;
            }
            else if (alarmCreateDTO.Priority == 0)
            {
                typeEnum = AlarmType.LOW;
            }
            else
            {
                return BadRequest();
            }

            AlarmDTO alarmDTO = new AlarmDTO(typeEnum, priorityEnum, alarmCreateDTO.Limit, alarmCreateDTO.AnalogInputId);

            var alarm = _alarmService.Create(alarmDTO);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("/triggers")]
        public ActionResult<List<AlarmTrigger>> GetTriggers(DateTime from, DateTime to)
        {
            return Ok(_alarmTrggerRepository.GetUnmutedTriggers(from, to));
        }

        [HttpGet("/all-triggers")]
        public ActionResult<List<AlarmTrigger>> GetAllTriggers(DateTime from, DateTime to)
        {
            return Ok(_alarmTrggerRepository.GetAlarmsTriggers(from, to));
        }

        [HttpGet]
        public ActionResult<List<AlarmTableDTO>> GetAllAlarms()
        {
            List<Alarm> alarms = _alarmService.GetAllAlarms();
            List<AlarmTableDTO> alarmsDTOs = new List<AlarmTableDTO>();

            foreach(Alarm alarm in alarms)
            {
                alarmsDTOs.Add(new AlarmTableDTO(alarm));
            }
            return Ok(alarmsDTOs);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAlarm1(int id)
        {
            _alarmService.Remove(id);
            return NoContent();
        }
    }
}