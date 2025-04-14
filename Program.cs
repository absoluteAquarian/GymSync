using GymSync.Components;
using GymSync.Components.Account;
using GymSync.Data;
using GymSync.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace GymSync {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();
			
			builder.Services.AddSingleton<UserSessionService>();

			builder.Services.AddHttpsRedirection(options => {
				options.HttpsPort = 7172;
			});


			builder.Services.AddCascadingAuthenticationState();
			builder.Services.AddScoped<IdentityUserAccessor>();
			builder.Services.AddScoped<IdentityRedirectManager>();
			builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = IdentityConstants.ApplicationScheme;
				options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
			})
			.AddIdentityCookies();

			var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

			var manifestStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GymSync.connection.txt")
				?? throw new InvalidOperationException("Could not find the connection string within the assembly's embedded resources.");
			string connectionString;
			using (var reader = new StreamReader(manifestStream))
				connectionString = reader.ReadToEnd();

			var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));
			//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, serverVersion));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();

			builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (!app.Environment.IsDevelopment())
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();


			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.MapAdditionalIdentityEndpoints();

			app.Run();
		}
	}
}
