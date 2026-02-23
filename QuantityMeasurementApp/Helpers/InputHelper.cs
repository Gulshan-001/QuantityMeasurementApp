namespace QuantityMeasurementApp.Helpers
{
    public static class InputHelper
    {
        public static bool TryParseDouble(string? input, out double value)
        {
            return double.TryParse(input, out value);
        }
    }
}