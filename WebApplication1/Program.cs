using Savorboard.CAP.InMemoryMessageQueue;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCap(x =>
            {
                x.UseSqlServer("");
                x.UseInMemoryMessageQueue();
                x.UseDashboard();
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}