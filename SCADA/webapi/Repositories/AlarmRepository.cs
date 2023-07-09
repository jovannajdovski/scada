using webapi.Enum;
using webapi.model;

namespace webapi.Repositories
{
    public interface IAlarmRepository
    {
        List<Alarm> GetAlarms(DateTime startTime, DateTime endTime);
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority);
    }

    public class AlarmRepository : IAlarmRepository
    {
        private readonly ScadaDBContext _context;

        public AlarmRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<Alarm> GetAlarms(DateTime startTime, DateTime endTime)
        {
            return _context.Alarms
                .Where(alarm => alarm.Date >= startTime && alarm.Date <= endTime)
                .ToList();
        }

        public List<Alarm> GetAlarmsByPriority(AlarmPriority priority)
        {
            return _context.Alarms
                .Where(alarm => alarm.Priority == priority)
                .ToList();
        }
    }
}
