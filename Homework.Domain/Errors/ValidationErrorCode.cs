namespace Homework.Domain.Errors
{
    public static class ValidationErrorCode
    {
        public static class VatRateInvalid
        {
            public static string ErrorCode => "VatRateInvalid"; 
            public static string ErrrorMessage => "The value of [{{PropertyName}}] must be {0}";
        }

        public static class GrossInvalid
        {
            public static string ErrorCode => "GrossInvalid";
            public static string ErrrorMessage => "The value of [{PropertyName}] field must be greater than 0";
        }
        public static class VatInvalid
        {
            public static string ErrorCode => "VatInvalid";
            public static string ErrrorMessage => "The value of [{PropertyName}] field must be greater than 0";
        }
        public static class NetInvalid
        {
            public static string ErrorCode => "NetInvalid";
            public static string ErrrorMessage => "The value of [{PropertyName}] field must be greater than 0";
        }
        public static class MultiplesFieldsInvalid
        {
            public static string ErrorCode => "MultiplesFieldsInvalid";
            public static string ErrrorMessage => "One and only one field from [Gross, Vat, Net] is required and allowed each time.";
            public static string PropertyName => "Gross, Vat, Net";
        }
    }
}
