using Microsoft.EntityFrameworkCore;
using ArithmeticService.Models;

namespace ArithmeticService.Data
{
    public class ArithmeticDbContext : DbContext
    {
        public ArithmeticDbContext(DbContextOptions<ArithmeticDbContext> options) : base(options) { }

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
    }

    public interface IMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        int GetOperationCount(string operation);
    }

    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ArithmeticDbContext _context;

        public MeasurementRepository(ArithmeticDbContext context)
        {
            _context = context;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.QuantityMeasurements
                .Where(x => x.OperationType.ToLower() == "add"
                         || x.OperationType.ToLower() == "subtract"
                         || x.OperationType.ToLower() == "divide")
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            return _context.QuantityMeasurements
                .Where(x => x.OperationType.ToLower() == operation.ToLower())
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public int GetOperationCount(string operation)
        {
            return _context.QuantityMeasurements
                .Count(x => x.OperationType.ToLower() == operation.ToLower());
        }
    }
}
