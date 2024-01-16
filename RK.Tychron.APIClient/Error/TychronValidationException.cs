namespace RK.Tychron.APIClient.Error
{
    public class TychronValidationException : Exception
    {
        private readonly ICollection<TychronValidationError> _validationErrors;

        public TychronValidationException(IEnumerable<TychronValidationError> errors)
        {
            _validationErrors = errors.ToList();
        }
       
        public ICollection<TychronValidationError> ValidationErrors => _validationErrors;
    }
}
