using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi.Repositories
{
    public interface IDigitalOutputRepository
    {
        List<DigitalOutput> GetAll();
        DigitalOutput GetById(int id);
        void Add(DigitalOutput digitalOutput);
        void Delete(DigitalOutput digitalOutput);
    }

    public class DigitalOutputRepository : IDigitalOutputRepository
    {
        private readonly ScadaDBContext _context;

        public DigitalOutputRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<DigitalOutput> GetAll()
        {
            return _context.DigitalOutputs.ToList();
        }

        public DigitalOutput GetById(int id)
        {
            return _context.DigitalOutputs.Find(id);
        }

        public void Add(DigitalOutput digitalOutput)
        {
            _context.DigitalOutputs.Add(digitalOutput);
            _context.SaveChanges();
        }

        public void Delete(DigitalOutput digitalOutput)
        {
            _context.DigitalOutputs.Remove(digitalOutput);
            _context.SaveChanges();
        }
    }
}
