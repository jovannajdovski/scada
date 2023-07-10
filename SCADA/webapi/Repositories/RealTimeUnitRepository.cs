using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi.Repositories
{
    public interface IRealTimeUnitRepository
    {
        List<RealTimeUnit> GetAll();
        RealTimeUnit GetById(int id);
        void Add(RealTimeUnit realTimeUnit);
        void Delete(RealTimeUnit realTimeUnit);
    }

    public class RealTimeUnitRepository : IRealTimeUnitRepository
    {
        private readonly ScadaDBContext _context;

        public RealTimeUnitRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<RealTimeUnit> GetAll()
        {
            return _context.RealTimeUnits.ToList();
        }

        public RealTimeUnit GetById(int id)
        {
            return _context.RealTimeUnits.Find(id);
        }

        public void Add(RealTimeUnit realTimeUnit)
        {
            _context.RealTimeUnits.Add(realTimeUnit);
            _context.SaveChanges();
        }

        public void Delete(RealTimeUnit realTimeUnit)
        {
            _context.RealTimeUnits.Remove(realTimeUnit);
            _context.SaveChanges();
        }
    }
}
