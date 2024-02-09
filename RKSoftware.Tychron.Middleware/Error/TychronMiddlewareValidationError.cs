namespace RKSoftware.Tychron.Middleware.Error;

/// <summary>
/// This object represents a validation error.
/// </summary>
/// <param name="FieldName">The name of the field that caused the error.</param>
/// <param name="ErrorCode">Validation error code.</param>
/// <param name="Message">Validation error message.</param>
public record TychronMiddlewareValidationError(string FieldName, string ErrorCode, string Message);
