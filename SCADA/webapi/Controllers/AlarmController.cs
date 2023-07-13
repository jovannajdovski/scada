using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
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

        [HttpPut("mute/{id}")]
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
        public ActionResult<Alarm> NewAlarm(AlarmDTO alarmDTO)
        {
            var alarm = _alarmService.Create(alarmDTO);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("triggers")]
        public ActionResult<List<AlarmTrigger>> GetTriggers()
        {
            List<AlarmTrigger> triggers = _alarmTrggerRepository.GetUnmutedTriggers(DateTime.MinValue, DateTime.MaxValue);

            List<AllAlarmsDTO> all = new List<AllAlarmsDTO>();

            foreach (var trigger in triggers)
            {
                all.Add(new AllAlarmsDTO(trigger));
            }

            return Ok(all);
        }

        [HttpGet("all-triggers")]
        public ActionResult<List<AlarmTrigger>> GetAllTriggers(DateTime from, DateTime to)
        {
            return Ok(_alarmTrggerRepository.GetAlarmsTriggers(from, to));
        }
    }
}