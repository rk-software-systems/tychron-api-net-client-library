namespace RKSoftware.Tychron.Middleware.Error
{
    /// <summary>
    /// This object represents a validation error.
    /// </summary>
    public record TychronMiddlewareValidationError
    {
        /// <summary>
        /// The name of the field that caused the error.
        /// </summary>
        public required string FieldName { get; set; }

        /// <summary>
        /// Validation error code.
        /// </summary>
        public required string ErrorCode { get; set; }

        /// <summary>
        /// Validation error message.
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// String representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "FieldName: " + FieldName + ", ErrorCode: " + ErrorCode + ", Message: " + Message;
        }
    }
}
