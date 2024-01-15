namespace RK.Tychron.APIClient.Error
{
    public class TychronValidationError
    {
        public required string FieldName { get; set; }

        public required string ErrorCode { get; set; }

        public required string Message { get; set; }
    }
}
