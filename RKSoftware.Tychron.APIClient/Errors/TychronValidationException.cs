namespace RKSoftware.Tychron.APIClient.Errors;

/// <summary>
/// Represents Validation Error of incoming Tychron request.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TychronValidationException"/> class.
/// </remarks>
/// <param name="errors">List or errors in request.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
public class TychronValidationException(IEnumerable<TychronValidationError> errors) : Exception
{
    private readonly ICollection<TychronValidationError> _validationErrors = errors.ToList();

    /// <summary>
    /// Gets the list of errors in request.
    /// </summary>
    public ICollection<TychronValidationError> ValidationErrors => _validationErrors;
}
