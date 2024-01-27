namespace RK.Tychron.APIClient.Error
{
    /// <summary>
    /// Represents Validation Error of incoming Tychron request.
    /// </summary>
    public class TychronValidationException : Exception
    {
        private readonly ICollection<TychronValidationError> _validationErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="TychronValidationException"/> class.
        /// </summary>
        /// <param name="errors">List or errors in request.</param>
        public TychronValidationException(IEnumerable<TychronValidationError> errors)
        {
            _validationErrors = errors.ToList();
        }

        /// <summary>
        /// Gets the list of errors in request.
        /// </summary>
        public ICollection<TychronValidationError> ValidationErrors => _validationErrors;
    }
}
