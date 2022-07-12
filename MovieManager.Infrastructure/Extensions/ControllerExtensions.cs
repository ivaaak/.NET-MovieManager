using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetControllerName(this Type controllerType)
            => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}
