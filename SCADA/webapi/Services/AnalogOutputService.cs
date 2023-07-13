using webapi.model;
using webapi.Model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IAnalogOutputService
    {
        List<AnalogOutput> GetAllAnalogOutputs();
        AnalogOutput GetAnalogOutputById(int id);
        void CreateAnalogOutput(AnalogOutput analogOutput);
        void DeleteAnalogOutput(int id);
        void AddNewValue(int id, string value);
    }

    public class AnalogOutputService : IAnalogOutputService
    {
        private readonly IAnalogOutputRepository _analogOutputRepository;
        private readonly ITagValueRepository _tagValueRepository;

        public AnalogOutputService(IAnalogOutputRepository analogOutputRepository, ITagValueRepository tagValueRepository)
        {
            _analogOutputRepository = analogOutputRepository;
            _tagValueRepository = tagValueRepository;
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
        public void AddNewValue(int id,  string value) {
            AnalogOutput analogOutput = _analogOutputRepository.GetById(id);
            TagValue tv = new TagValue();
            tv.Value = value;
            tv.Type = "double";
            tv.Date = DateTime.Now;
            tv.TagBaseId = id;
            analogOutput.Values.Add(tv);
            _tagValueRepository.AddTagValue(tv);
            _analogOutputRepository.Update(analogOutput);
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
