using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAnalogOutputService
    {
        List<AnalogOutput> GetAllAnalogOutputs();
        AnalogOutput GetAnalogOutputById(int id);
        void CreateAnalogOutput(AnalogOutput analogOutput);
        void DeleteAnalogOutput(int id);
    }

    public class AnalogOutputService : IAnalogOutputService
    {
        private readonly IAnalogOutputRepository _analogOutputRepository;

        public AnalogOutputService(IAnalogOutputRepository analogOutputRepository)
        {
            _analogOutputRepository = analogOutputRepository;
        }

        public List<AnalogOutput> GetAllAnalogOutputs()
        {
            return _analogOutputRepository.GetAll();
        }

        public AnalogOutput GetAnalogOutputById(int id)
        {
            return _analogOutputRepository.GetById(id);
        }

        public void CreateAnalogOutput(AnalogOutput analogOutput)
        {
            _analogOutputRepository.Add(analogOutput);
        }

        public void DeleteAnalogOutput(int id)
        {
            var analogOutput = _analogOutputRepository.GetById(id);
            if (analogOutput != null)
            {
                _analogOutputRepository.Delete(analogOutput);
            }
        }
    }
}
