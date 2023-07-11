using webapi.DTO;
using webapi.Enum;
using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAlarmService
    {
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority);

        public Alarm Mute(int id);

        Alarm Create(AlarmDTO alarmDTO);
    }

    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputService _analogInputService;

        public AlarmService(IAlarmRepository alarmRepository, IAnalogInputService analogInputService)
        {
            _alarmRepository = alarmRepository;
            _analogInputService = analogInputService;
        }

        public List<Alarm> GetAlarmsByPriority(AlarmPriority priority)
        {
            return _alarmRepository.GetAlarmsByPriority(priority);
        }

        public Alarm Mute(int id)
        {
            var alarm = _alarmRepository.GetAlarmById(id);
            if (alarm != null)
            {
                alarm.isMuted = true;
                _alarmRepository.UpdateAlarm(alarm);
            }
            return alarm;
        }

        public Alarm Create(AlarmDTO alarmDTO)
        {
            Alarm alarm = null;
            AnalogInput analogInput = _analogInputService.GetAnalogInputById(alarmDTO.AnalogInputId);
            if (analogInput != null)
            {
                alarm = new Alarm();
                alarm.AnalogInput = analogInput;
                alarm.isMuted = false;
                alarm.Priority = alarmDTO.Priority;
                alarm.Limit = alarmDTO.Limit;
                alarm.Type = alarmDTO.Type;
            }
        }
    }
}