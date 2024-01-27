namespace RKSoftware.Tychron.APIClient.Error
{
    /// <summary>
    /// Represents Validation Error of oncoming Tychron request.
    /// </summary>
    public record TychronValidationError
    {
        /// <summary>
        /// Invalid field name.
        /// </summary>
        public required string FieldName { get; set; }

        /// <summary>
        /// Validation Error Code (can be used to translate validation error messages).
        /// </summary>
        public required string ErrorCode { get; set; }

        /// <summary>
        /// Validation Error Message (short description)
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// Custom error string representation.
        /// </summary>
        /// <returns>Validation Error string representation.</returns>
        public override string ToString()
        {
            return "FieldName: " + FieldName + ", ErrorCode: " + ErrorCode + ", Message: " + Message;
        }
    }
}
