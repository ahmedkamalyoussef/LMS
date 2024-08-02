using Microsoft.AspNetCore.Builder;

namespace LMS.Application.Extentions
{
    public static class BuilderExtension
    {

        public static void UseCustomMiddlewares(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();

            app.MapControllers();

            app.UseCors("CorsPolicy");
        }
    }
}
