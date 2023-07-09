using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi.Repositories
{
    public interface IAnalogOutputRepository
    {
        List<AnalogOutput> GetAll();
        AnalogOutput GetById(int id);
        void Add(AnalogOutput analogOutput);
        void Delete(AnalogOutput analogOutput);
    }

    public class AnalogOutputRepository : IAnalogOutputRepository
    {
        private readonly ScadaDBContext _context;

        public AnalogOutputRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<AnalogOutput> GetAll()
        {
            return _context.AnalogOutputs.ToList();
        }

        public AnalogOutput GetById(int id)
        {
            return _context.AnalogOutputs.Find(id);
        }

        public void Add(AnalogOutput analogOutput)
        {
            _context.AnalogOutputs.Add(analogOutput);
            _context.SaveChanges();
        }

        public void Delete(AnalogOutput analogOutput)
        {
            _context.AnalogOutputs.Remove(analogOutput);
            _context.SaveChanges();
        }
    }
}
