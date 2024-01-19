using RK.Tychron.APIClient.Error;

namespace RK.Tychron.APIClient.Models
{
    public interface IValidationSubject
    {
        List<TychronValidationError> Validate();
    }
}
