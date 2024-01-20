using RK.Tychron.Middleware.Error;

namespace RK.Tychron.Middleware.Models
{
    public interface IValidationSubject
    {
        List<TychronMiddlewareValidationError> Validate();
    }
}
