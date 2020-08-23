using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_EF_Start.DataAccess;
using System.Data.SQLite;


namespace MVC_EF_Start
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      Configuration = configuration;
      _currentEnvironment = env;
     }

    public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _currentEnvironment;

   // This method gets called by the runtime. Use this method to add services to the container.
   // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
    {
            if (_currentEnvironment.IsDevelopment())
            {

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration["Data:IEXTrading:ConnectionString"]));
            }
            else if (_currentEnvironment.IsProduction())
            {

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:IEXTrading:ConnectionString"]));
            }

            // Setup EF connection
            string connString = "Data Source=./SQLITEDB.sqlite;";
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connString));

      // added from MVC template
      services.AddMvc();
    }

    // this is the version from the MVC template
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      //This ensures that the database and tables are created as per the Models.
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
      }

      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}"
            );
          routes.MapRoute(
            name: "Detail Page",
            template: "{controller=DatabaseExample}/{action=PointQueryAction}/{id}"
            );
          routes.MapRoute(
            name: "db",
            template: "{controller=DatabaseExample}/{action=DatabaseOperations}/"
            );
        });
    }
  }
}