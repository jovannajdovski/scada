using webapi.Enum;
using webapi.model;
using webapi.Model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAlarmTriggerService
    {
        string GetAnalogInputPriority(AnalogInput analogInput);
    }

    public class AlarmTriggerService : IAlarmTriggerService
    {
        private readonly IAlarmTriggerRepository _alarmTriggerRepository;

        public AlarmTriggerService(IAlarmTriggerRepository alarmTriggerRepository)
        {
            _alarmTriggerRepository = alarmTriggerRepository;
        }

        public string GetAnalogInputPriority(AnalogInput analogInput)
        {
            List<AlarmTrigger> alarmTriggers = _alarmTriggerRepository.GetAlarmsTriggersForAnalogInput(DateTime.Now.AddMinutes(-5), DateTime.Now, analogInput);
            
            if (alarmTriggers.Count > 0)
            {
                return alarmTriggers.First().Alarm.Priority.ToString();
            }
            else
                return "";
        }
    }
}