using RKSoftware.Tychron.Middleware.Error;

namespace RKSoftware.Tychron.Middleware.Models
{
    /// <summary>
    /// All Tychron models that can be validated should implement this interface.
    /// </summary>
    public interface IValidationSubject
    {
        /// <summary>
        /// Validates the model and returns a list of errors.
        /// </summary>
        /// <returns>
        /// List with Tychron errors in case there are some errors present.
        /// Empty list in case there are no errors.
        /// </returns>
        List<TychronMiddlewareValidationError> Validate();
    }
}
