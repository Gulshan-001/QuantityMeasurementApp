using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool AreEqual<U>(Quantity<U> q1, Quantity<U> q2)
            where U : struct, Enum
        {
            return q1.Equals(q2);
        }

        public Quantity<U> Convert<U>(Quantity<U> quantity, U targetUnit)
            where U : struct, Enum
        {
            return quantity.ConvertTo(targetUnit);
        }

        public Quantity<U> Add<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit)
            where U : struct, Enum
        {
            return q1.Add(q2, targetUnit);
        }
    }
}