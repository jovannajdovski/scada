using webapi.model;

namespace webapi.Repositories
{

    public interface IIOAdressRepository
    {
        List<IOAdress> GetAll();
        IOAdress GetById(int id);
        void Add(IOAdress ioAdress);
        void Delete(IOAdress ioAdress);
    }

    public class IOAdressRepository : IIOAdressRepository
    {
        private readonly ScadaDBContext _context;

        public IOAdressRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<IOAdress> GetAll()
        {
            return _context.Adresses.ToList();
        }

        public IOAdress GetById(int id)
        {
            return _context.Adresses.Find(id);
        }

        public void Add(IOAdress ioAdress)
        {
            _context.Adresses.Add(ioAdress);
            _context.SaveChanges();
        }

        public void Delete(IOAdress ioAdress)
        {
            _context.Adresses.Remove(ioAdress);
            _context.SaveChanges();
        }
    }
}
