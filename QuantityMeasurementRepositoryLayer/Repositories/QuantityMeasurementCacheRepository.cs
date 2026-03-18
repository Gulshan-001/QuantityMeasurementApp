using System.Collections.Generic;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> _store = new();

        public void Save(QuantityMeasurementEntity entity)
        {
            entity.Id = _store.Count + 1;
            _store.Add(entity);
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return _store;
        }

        public QuantityMeasurementEntity GetById(int id)
        {
            return _store.Find(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                _store.Remove(item);
        }

        public void AddAuditLog(int logId, string actionType, string oldValue, string newValue, string modifiedBy)
        {
            // skip for cache
        }
    }
}