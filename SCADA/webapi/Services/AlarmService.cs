using Microsoft.EntityFrameworkCore;
using webapi.DTO;
using webapi.Enum;
using webapi.model;
using webapi.Model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAlarmService
    {
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority);

        public Alarm Mute(int id);

        Alarm Create(AlarmDTO alarmDTO);

        void TriggerAlarms(AnalogInput analogInput);
    }

    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IAlarmTriggerRepository _alarmTriggerRepository;

        public AlarmService(IAlarmRepository alarmRepository, IAnalogInputRepository analogInputRepository, IAlarmTriggerRepository alarmTriggerRepository)
        {
            _alarmRepository = alarmRepository;
            _analogInputRepository = analogInputRepository;
            _alarmTriggerRepository = alarmTriggerRepository;
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
            AnalogInput analogInput = _analogInputRepository.GetById(alarmDTO.AnalogInputId);
            if (analogInput != null)
            {
                alarm = new Alarm();
                alarm.AnalogInput = analogInput;
                alarm.isMuted = false;
                alarm.Priority = alarmDTO.Priority;
                alarm.Limit = alarmDTO.Limit;
                alarm.Type = alarmDTO.Type;
            }
            _alarmRepository.Add(alarm);
            return alarm;
        }

        public void TriggerAlarms(AnalogInput analogInput)
        {
            var lowAlarms = GetLowAlarmByAnalogInput(analogInput.Alarms);
            var value = analogInput.Values.Last().Value;
            foreach (var alarm in lowAlarms)
            {
                if (alarm.Limit >= Double.Parse(value))
                {
                    TriggerAlarm(alarm); break;
                }
            }

            var highAlarms = GetHighAlarmByAnalogInput(analogInput.Alarms);
            value = analogInput.Values.Last().Value;
            foreach (var alarm in lowAlarms)
            {
                if (alarm.Limit <= Double.Parse(value))
                {
                    TriggerAlarm(alarm); break;
                }
            }
        }

        public List<Alarm> GetLowAlarmByAnalogInput(List<Alarm> alarms)
        {
            return alarms
                .Where(alarm => alarm.Type == AlarmType.LOW)
                .OrderByDescending(alarm => alarm.Priority)
                .ThenBy(alarm => alarm.Limit).ToList();
        }

        public List<Alarm> GetHighAlarmByAnalogInput(List<Alarm> alarms)
        {
            return alarms
                .Where(alarm => alarm.Type == AlarmType.HIGH)
                .OrderByDescending(alarm => alarm.Priority)
                .ThenByDescending(alarm => alarm.Limit).ToList();
        }

        public void TriggerAlarm(Alarm alarm)
        {
            var trigger = new AlarmTrigger();
            trigger.Alarm = alarm;
            trigger.DateTime = DateTime.Now;
            _alarmTriggerRepository.AddTrigger(trigger);
        }
    }
}