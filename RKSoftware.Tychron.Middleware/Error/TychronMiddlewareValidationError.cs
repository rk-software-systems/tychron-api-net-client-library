namespace RKSoftware.Tychron.Middleware.Error
{
    public record TychronMiddlewareValidationError
    {
        public required string FieldName { get; set; }

        public required string ErrorCode { get; set; }

        public required string Message { get; set; }

        public override string ToString()
        {
            return "FieldName: " + FieldName + ", ErrorCode: " + ErrorCode + ", Message: " + Message;
        }
    }
}
