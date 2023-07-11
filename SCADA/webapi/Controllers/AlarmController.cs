using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
using webapi.model;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/alarms")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
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
        public ActionResult<Alarm> NewAlarm(AlarmDTO alarmDTO)
        {
            var alarm = _alarmService.Create(alarmDTO);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}