using webapi.model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IDigitalInputService
    {
        List<DigitalInput> GetAllDigitalInputs();
        DigitalInput GetDigitalInputById(int id);
        void CreateDigitalInput(DigitalInput digitalInput);
        void UpdateDigitalInput(DigitalInput digitalInput);
        void DeleteDigitalInput(int id);
    }

    public class DigitalInputService : IDigitalInputService
    {
        private readonly IDigitalInputRepository _digitalInputRepository;

        public DigitalInputService(IDigitalInputRepository digitalInputRepository)
        {
            _digitalInputRepository = digitalInputRepository;
        }

        public List<DigitalInput> GetAllDigitalInputs()
        {
            return _digitalInputRepository.GetAll();
        }

        public DigitalInput GetDigitalInputById(int id)
        {
            return _digitalInputRepository.GetById(id);
        }

        public void CreateDigitalInput(DigitalInput digitalInput)
        {
            _digitalInputRepository.Add(digitalInput);
        }

        public void UpdateDigitalInput(DigitalInput digitalInput)
        {
            _digitalInputRepository.Update(digitalInput);
        }

        public void DeleteDigitalInput(int id)
        {
            var digitalInput = _digitalInputRepository.GetById(id);
            if (digitalInput != null)
            {
                _digitalInputRepository.Delete(digitalInput);
            }
        }
    }
}
