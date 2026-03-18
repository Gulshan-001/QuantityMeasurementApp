using System.Collections.Generic;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        // Main log
        void Save(QuantityMeasurementEntity entity);

        // History
        List<QuantityMeasurementEntity> GetAll();

        // Optional (future use)
        QuantityMeasurementEntity GetById(int id);

        void Delete(int id);

        // Audit
        void AddAuditLog(int logId, string actionType, string oldValue, string newValue, string modifiedBy);
    }
}