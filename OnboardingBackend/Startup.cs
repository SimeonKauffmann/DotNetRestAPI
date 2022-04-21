using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using OnboardingBackend.Services;
using OnboardingBackend.Models;



namespace OnboardingBackend
{
  public class Startup
  {
    private IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {

      Configuration = configuration;
    }



    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
                  {

                builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();

              });
      });
      services.Configure<DbModel>(Configuration.GetSection("ConnectionStrings"));
      services.AddSingleton<IMongoClient>(x => new MongoClient(Configuration.GetConnectionString("mongoDB")));
      services.AddSingleton<UserService>();
      services.AddControllers();


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors();

      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
