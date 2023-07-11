using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.model;

namespace webapi.Repositories
{
    public interface IAlarmRepository
    {
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority);

        void Add(Alarm alarm);

        Alarm GetAlarmById(int alarmId);

        void UpdateAlarm(Alarm alarm);
    }

    public class AlarmRepository : IAlarmRepository
    {
        private readonly ScadaDBContext _context;

        public AlarmRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public Alarm GetAlarmById(int alarmId)
        {
            return _context.Alarms.Include(alarm => alarm.AnalogInput)
                .Where(alarm => alarm.Id == alarmId).First();
        }

        public List<Alarm> GetAlarmsByPriority(AlarmPriority priority)
        {
            return _context.Alarms
                .Include(alarm => alarm.AnalogInput)
                .Where(alarm => alarm.Priority == priority)
                .ToList();
        }

        public void UpdateAlarm(Alarm alarm)
        {
            _context.Alarms.Update(alarm);
            _context.SaveChanges();
        }

        public void Add(Alarm alarm)
        {
            _context.Alarms.Add(alarm);
            _context.SaveChanges();
        }
    }
}