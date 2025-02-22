using GymSync.Components;
using GymSync.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymSync {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

			var manifestStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GymSync.connection.txt")
				?? throw new InvalidOperationException("Could not find the connection string within the assembly's embedded resources.");
			string connectionString;
			using (var reader = new StreamReader(manifestStream))
				connectionString = reader.ReadToEnd();

			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.Run();
		}
	}
}
