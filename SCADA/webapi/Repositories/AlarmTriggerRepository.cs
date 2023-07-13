using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.model;
using webapi.Model;

namespace webapi.Repositories
{
    public interface IAlarmTriggerRepository
    {
        List<AlarmTrigger> GetAlarmsTriggers(DateTime startTime, DateTime endTime);

        List<AlarmTrigger> GetAlarmsTriggersForAnalogInput(DateTime startTime, DateTime endTime, AnalogInput analogInput);

        List<AlarmTrigger> GetAlarmsTriggersByPriority(AlarmPriority priority);

        List<AlarmTrigger> GetUnmutedTriggers(DateTime startTime, DateTime endTime);

        void AddTrigger(AlarmTrigger trigger);
    }

    public class AlarmTriggerRepository : IAlarmTriggerRepository
    {
        private readonly ScadaDBContext _context;

        public AlarmTriggerRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<AlarmTrigger> GetAlarmsTriggersByPriority(AlarmPriority priority)
        {
            return _context.AlarmsTriggers
                .Include(trigger => trigger.Alarm)
                .Include(trigger => trigger.Alarm.AnalogInput)
                .Where(trigger => trigger.Alarm.Priority == priority)
                .ToList();
        }

        public List<AlarmTrigger> GetAlarmsTriggers(DateTime startTime, DateTime endTime)
        {
            return _context.AlarmsTriggers
                .Include(trigger => trigger.Alarm)
                .Include(trigger => trigger.Alarm.AnalogInput)
                .Where(alarm => alarm.DateTime >= startTime && alarm.DateTime <= endTime)
                .ToList();
        }

        public AlarmTrigger GetLatestAlarmTriggerForAnalogInput(AnalogInput analogInput)
        {
            return _context.AlarmsTriggers
                .Include(trigger => trigger.Alarm)
                .Where(trigger => trigger.Alarm.AnalogInput == analogInput)
                .OrderByDescending(trigger => trigger.DateTime)
                .ToList().First();
        }

        public void AddTrigger(AlarmTrigger trigger)
        {
            _context.AlarmsTriggers.Add(trigger);
            _context.SaveChanges();
        }

        public List<AlarmTrigger> GetUnmutedTriggers(DateTime startTime, DateTime endTime)
        {
            return this.GetAlarmsTriggers(startTime, endTime)
                .Where(trigger => trigger.Alarm.isMuted == false).ToList();
        }

        public List<AlarmTrigger> GetAlarmsTriggersForAnalogInput(DateTime startTime, DateTime endTime, AnalogInput analogInput)
        {
            return this.GetAlarmsTriggers(startTime, endTime)
               .Where(trigger => trigger.Alarm.AnalogInput == analogInput)
               .OrderByDescending(trigger => trigger.DateTime)
               .ThenByDescending(trigger => trigger.Alarm.Priority).ToList();
        }
    }
}