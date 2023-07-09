using webapi.Model;
using webapi.Repositories;

namespace webapi.Services
{
    public interface ITagValueService
    {
        List<TagValue> GetTagValues(DateTime startTime, DateTime endTime);
        List<TagValue> GetTagValuesByTagId(int tagId);
        TagValue GetLastTagValue(int tagId);
        void AddTagValue(TagValue tagValue);
        void UpdateTagValue(TagValue tagValue);
        void DeleteTagValue(TagValue tagValue);
    }

    public class TagValueService : ITagValueService
    {
        private readonly ITagValueRepository _tagValueRepository;

        public TagValueService(ITagValueRepository tagValueRepository)
        {
            _tagValueRepository = tagValueRepository;
        }

        public List<TagValue> GetTagValues(DateTime startTime, DateTime endTime)
        {
            return _tagValueRepository.GetTagValues(startTime, endTime);
        }

        public List<TagValue> GetTagValuesByTagId(int tagId)
        {
            return _tagValueRepository.GetTagValuesByTagId(tagId);
        }

        public TagValue GetLastTagValue(int tagId)
        {
            return _tagValueRepository.GetLastTagValue(tagId);
        }

        public void AddTagValue(TagValue tagValue)
        {
            _tagValueRepository.AddTagValue(tagValue);
        }

        public void UpdateTagValue(TagValue tagValue)
        {
            _tagValueRepository.UpdateTagValue(tagValue);
        }

        public void DeleteTagValue(TagValue tagValue)
        {
            _tagValueRepository.DeleteTagValue(tagValue);
        }
    }
}
