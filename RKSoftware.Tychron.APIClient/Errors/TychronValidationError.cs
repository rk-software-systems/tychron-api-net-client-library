namespace RKSoftware.Tychron.APIClient.Errors;

/// <summary>
/// Represents Validation Error of oncoming Tychron request.
/// </summary>
/// <param name="FieldName">Invalid field name.</param>
/// <param name="ErrorCode">Validation Error Code (can be used to translate validation error messages).</param>
/// <param name="Message">Validation Error Message (short description)</param>
public record TychronValidationError(string FieldName, string ErrorCode, string Message);
