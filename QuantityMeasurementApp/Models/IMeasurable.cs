namespace QuantityMeasurementApp.Models
{
    public interface IMeasurable
    {
        double GetConversionFactor();
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();

        // UC14 additions (SAFE DEFAULTS)

        public delegate bool SupportsArithmetic();

        SupportsArithmetic supportsArithmetic => () => true;

        public virtual bool SupportsArithmeticOperations()
        {
            return supportsArithmetic();
        }

        public virtual void ValidateOperationSupport(string operation)
        {
            // Default behavior: allow all operations
        }
    }
}