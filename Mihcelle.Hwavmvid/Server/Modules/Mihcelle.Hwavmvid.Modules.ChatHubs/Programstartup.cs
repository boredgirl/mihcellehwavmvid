using Microsoft.AspNetCore.Mvc;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public void Configure(IServiceCollection services)
        {

            /*
            services.AddScoped<Mihcelle.Hwavmvid.Modules.ChatHubs.Applicationdbcontext, Mihcelle.Hwavmvid.Modules.ChatHubs.Applicationdbcontext>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.DefaultBufferSize = 4096;
                options.JsonSerializerOptions.MaxDepth = 41;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddMemoryCache();
            //services.TryAddHttpClientWithAuthenticationCookie();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            */
        }

        public void Configureapp(WebApplication application)
        {

        }

    }
}
