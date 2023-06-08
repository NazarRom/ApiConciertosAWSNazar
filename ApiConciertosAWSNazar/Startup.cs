using ApiConciertosAWSNazar.Data;
using ApiConciertosAWSNazar.Helpers;
using ApiConciertosAWSNazar.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiConciertosAWSNazar;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        //string connectionString = this.Configuration.GetConnectionString("Conciertos");
        string miSecreto = HelperSecretManager.GetSecretAsync().Result;

        //PODEMOS DAR FORMATO A NUESTRO SECRETO
        KeysModel model = JsonConvert.DeserializeObject<KeysModel>(miSecreto);

        //SI ESTAMOS EN UNA APP MAS COMPLEJA DONDE NECESITAMOS RECUPERAR MAS 
        //DATOS Y UTILIZARLOS EN DISTINTAS CLASES, AL ESTILO DE IConfiguration
        services.AddSingleton<KeysModel>(x => model).BuildServiceProvider();
        services.AddTransient<RepositoryConcierto>();
        services.AddDbContext<ConciertosContext>(options => options.UseMySql(model.MySql, ServerVersion.AutoDetect(model.MySql)));
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}