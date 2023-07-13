using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAnalogInputService
    {
        List<AnalogInput> GetAllScanningAnalogInputs();

        List<AnalogInput> GetAllAnalogInputs();

        AnalogInput GetAnalogInputById(int id);

        void CreateAnalogInput(AnalogInput analogInput);

        void UpdateAnalogInput(AnalogInput analogInput);

        void DeleteAnalogInput(int id);
    }

    public class AnalogInputService : IAnalogInputService
    {
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IAlarmService _alarmService;

        public AnalogInputService(IAnalogInputRepository analogInputRepository, IAlarmService alarmService)
        {
            _analogInputRepository = analogInputRepository;
            _alarmService = alarmService;
        }

        public List<AnalogInput> GetAllScanningAnalogInputs()
        {
            return _analogInputRepository.GetAllScanningAnalogInputs();
        }

        public List<AnalogInput> GetAllAnalogInputs()
        {
            return _analogInputRepository.GetAll();
        }

        public AnalogInput GetAnalogInputById(int id)
        {
            return _analogInputRepository.GetById(id);
        }

        public void CreateAnalogInput(AnalogInput analogInput)
        {
            _analogInputRepository.Add(analogInput);
        }

        public void UpdateAnalogInput(AnalogInput analogInput)
        {
            _analogInputRepository.Update(analogInput);
            //_alarmService.TriggerAlarms(analogInput);
        }

        public void DeleteAnalogInput(int id)
        {
            var analogInput = _analogInputRepository.GetById(id);
            if (analogInput != null)
            {
                _analogInputRepository.Delete(analogInput);
            }
        }
    }
}