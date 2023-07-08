using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IDigitalOutputService
    {
        List<DigitalOutput> GetAllDigitalOutputs();
        DigitalOutput GetDigitalOutputById(int id);
        void CreateDigitalOutput(DigitalOutput digitalOutput);
        void DeleteDigitalOutput(int id);
    }

    public class DigitalOutputService : IDigitalOutputService
    {
        private readonly IDigitalOutputRepository _digitalOutputRepository;

        public DigitalOutputService(IDigitalOutputRepository digitalOutputRepository)
        {
            _digitalOutputRepository = digitalOutputRepository;
        }

        public List<DigitalOutput> GetAllDigitalOutputs()
        {
            return _digitalOutputRepository.GetAll();
        }

        public DigitalOutput GetDigitalOutputById(int id)
        {
            return _digitalOutputRepository.GetById(id);
        }

        public void CreateDigitalOutput(DigitalOutput digitalOutput)
        {
            _digitalOutputRepository.Add(digitalOutput);
        }

        public void DeleteDigitalOutput(int id)
        {
            var digitalOutput = _digitalOutputRepository.GetById(id);
            if (digitalOutput != null)
            {
                _digitalOutputRepository.Delete(digitalOutput);
            }
        }
    }
}
