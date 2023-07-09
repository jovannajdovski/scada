using webapi.Model;

namespace webapi.Repositories
{
    public interface ITagValueRepository
    {
        List<TagValue> GetTagValues(DateTime startTime, DateTime endTime);
        List<TagValue> GetTagValuesByTagId(int tagId);
        TagValue GetLastTagValue(int tagId);
        void AddTagValue(TagValue tagValue);
        void UpdateTagValue(TagValue tagValue);
        void DeleteTagValue(TagValue tagValue);
    }

    public class TagValueRepository : ITagValueRepository
    {
        private readonly ScadaDBContext _context;

        public TagValueRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<TagValue> GetTagValues(DateTime startTime, DateTime endTime)
        {
            return _context.TagValues
                .Where(tv => tv.Date >= startTime && tv.Date <= endTime)
                .ToList();
        }

        public List<TagValue> GetTagValuesByTagId(int tagId)
        {
            return _context.TagValues
                .Where(tv => tv.TagId == tagId)
                .ToList();
        }

        public TagValue GetLastTagValue(int tagId)
        {
            return _context.TagValues
                .Where(tv => tv.TagId == tagId)
                .OrderByDescending(tv => tv.Date)
                .FirstOrDefault();
        }

        public void AddTagValue(TagValue tagValue)
        {
            _context.TagValues.Add(tagValue);
            _context.SaveChanges();
        }

        public void UpdateTagValue(TagValue tagValue)
        {
            _context.TagValues.Update(tagValue);
            _context.SaveChanges();
        }

        public void DeleteTagValue(TagValue tagValue)
        {
            _context.TagValues.Remove(tagValue);
            _context.SaveChanges();
        }
    }
}
