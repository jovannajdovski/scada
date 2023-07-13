using webapi.model;
using webapi.Model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IDigitalOutputService
    {
        List<DigitalOutput> GetAllDigitalOutputs();
        DigitalOutput GetDigitalOutputById(int id);
        void CreateDigitalOutput(DigitalOutput digitalOutput);
        void DeleteDigitalOutput(int id);
        void AddNewValue(int id, string value);
    }

    public class DigitalOutputService : IDigitalOutputService
    {
        private readonly IDigitalOutputRepository _digitalOutputRepository;
        private readonly ITagValueRepository _tagValueRepository;

        public DigitalOutputService(IDigitalOutputRepository digitalOutputRepository, ITagValueRepository tagValueRepository)
        {
            _digitalOutputRepository = digitalOutputRepository;
            _tagValueRepository = tagValueRepository;
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
        public void AddNewValue(int id, string value)
        {
            DigitalOutput digitalOutput = _digitalOutputRepository.GetById(id);
            TagValue tv = new TagValue();
            tv.Value = value;
            tv.Type = "boolean";
            tv.Date = DateTime.Now;
            tv.TagBaseId = id;
            digitalOutput.Values.Add(tv);
            _tagValueRepository.AddTagValue(tv);
            _digitalOutputRepository.Update(digitalOutput);
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
