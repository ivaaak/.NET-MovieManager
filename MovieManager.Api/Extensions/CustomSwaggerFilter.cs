using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace SwaggerFilter.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var playlistRoutes = swaggerDoc.Paths
                .Where(x => !x.Key.ToLower().Contains("getplaylists"))
                .FirstOrDefault();

            swaggerDoc.Paths.Clear();
            swaggerDoc.Paths.Append(playlistRoutes);
        }
    }
}
