using webapi.Enum;
using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAlarmService
    {
        List<Alarm> GetAlarms(DateTime startTime, DateTime endTime, bool isAscending = true);
        List<Alarm> GetAlarmsByPriority(AlarmPriority priority, bool isAscending = true);
    }

    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;

        public AlarmService(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;
        }

        public List<Alarm> GetAlarms(DateTime startTime, DateTime endTime, bool isAscending = true)
        {
            List<Alarm> alarms = _alarmRepository.GetAlarms(startTime, endTime);

            if (isAscending)
            {
                alarms.Sort((x, y) =>
                {
                    int priorityComparison = x.Priority.CompareTo(y.Priority);
                    if (priorityComparison != 0)
                    {
                        return priorityComparison;
                    }
                    return x.Date.CompareTo(y.Date);
                });
            }
            else
            {
                alarms.Sort((x, y) =>
                {
                    int priorityComparison = y.Priority.CompareTo(x.Priority);
                    if (priorityComparison != 0)
                    {
                        return priorityComparison;
                    }
                    return y.Date.CompareTo(x.Date);
                });
            }

            return alarms;
        }

        public List<Alarm> GetAlarmsByPriority(AlarmPriority priority, bool isAscending = true)
        {
            List<Alarm> alarms = _alarmRepository.GetAlarmsByPriority(priority);

            if (isAscending)
            {
                alarms.Sort((x, y) => x.Date.CompareTo(y.Date));
            }
            else
            {
                alarms.Sort((x, y) => y.Date.CompareTo(x.Date));
            }

            return alarms;
        }
    }
}
