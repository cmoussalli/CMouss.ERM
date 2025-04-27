using CMouss.ERM.Data;
using CMouss.ERM.Portal.Components;
using CMouss.ERM.Data.DBServices;

using CMouss.IdentityFramework;
using System.Reflection;


namespace CMouss.ERM.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // register DbContext and DB services
            builder.Services.AddDbContext<ERMDBContext>();
            builder.Services.AddScoped<EntityFieldDBService>();
            builder.Services.AddScoped<RecordDBService>();
            builder.Services.AddScoped<RecordFieldValueDBService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<Components.App>()
                .AddInteractiveServerRenderMode();



            bool isNewDatabase = false;
            string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\mydb.db";
            //if (System.IO.File.Exists(filepath))
            //{
            //    System.IO.File.Delete(filepath);
            //}
                isNewDatabase = false;
            Thread.Sleep(100);
            IDFManager.Configure(new IDFManagerConfig
            {
                DatabaseType = DatabaseType.SQLite,
                DBConnectionString = "Data Source=mydb.db;",
                DefaultListPageSize = 25,
                DBLifeCycle = DBLifeCycle.Both,
                IsActiveByDefault = true,
                IsLockedByDefault = false,
                DefaultTokenLifeTime = new LifeTime(30, 0, 0)
            });

            ERMDBContext db = new ERMDBContext();
            db.Database.EnsureCreated();

            if (isNewDatabase)
            {
                db.InsertTestData();
            }

            app.Run();
        }
    }
}
