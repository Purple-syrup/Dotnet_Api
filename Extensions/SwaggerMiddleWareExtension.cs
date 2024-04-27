namespace Dotnet_Api.Extensions
{
    public static class SwaggerMiddleWareExtension
    {
        public static void ConfigureSwaggerMiddleWare(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}