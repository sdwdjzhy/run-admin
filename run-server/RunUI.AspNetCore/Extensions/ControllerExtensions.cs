using Microsoft.Extensions.DependencyInjection;

namespace RunUI
{
    public static class ControllerExtensions
    {
        public static T GetService<T>(this BaseController controller)
        {
            return controller.HttpContext.RequestServices.GetService<T>();
        }

        public static IEnumerable<T> GetServices<T>(this BaseController controller)
        {
            return controller.HttpContext.RequestServices.GetServices<T>();
        }
    }
}
