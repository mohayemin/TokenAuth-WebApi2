using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Services;
using Api.Services.Samples;
using Microsoft.AspNetCore.Identity;
using Api.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Api.Notifications;
using System;

namespace Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("authdb"));

			ConfigureIdentity(services);
			ConfigureAuth(services);

			services.AddMvc();

			services
				.AddSingleton<ITokenConfig, SampleTokenConfig>()
				.AddScoped<IdentityDbContext, AuthDbContext>()
				.AddScoped<ITokenBuilder, JwtTokenBuilder>()
				.AddScoped<ITokenIssuer, IdentityTokenIssuer>()
				.AddScoped<INotifier, SampleNotifier>()
				.AddScoped<UserService>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseMvc();

			Seed(app);
		}

		private void ConfigureIdentity(IServiceCollection services)
		{
			var builder = services.AddIdentity<IdentityUser, IdentityRole>(opt =>
			{
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 1;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
			});
			builder.AddEntityFrameworkStores<AuthDbContext>();
			builder.AddSignInManager<SignInManager<IdentityUser>>();
			builder.AddDefaultTokenProviders();
		}

		private void ConfigureAuth(IServiceCollection services)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var config = services.BuildServiceProvider().GetService<ITokenConfig>();

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = config.Issuer,
					ValidAudience = config.Audiance,
					IssuerSigningKey = config.SigningCredentials.Key
				};
			});
		}

		public void Seed(IApplicationBuilder app)
		{
			var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

			using (IServiceScope scope = scopeFactory.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				roleManager.CreateAsync(new IdentityRole("admin"));
				roleManager.CreateAsync(new IdentityRole("moderator"));
				roleManager.CreateAsync(new IdentityRole("regular"));

				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
				var adminUser = new IdentityUser("admin") { Email = "admin@nomail.com" };
				userManager.CreateAsync(adminUser, "123");

				userManager.AddToRoleAsync(adminUser, "admin");

				var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
				db.Set<RefreshToken>().AddAsync(new RefreshToken(adminUser.Id, "xyz", DateTime.MaxValue));
				db.SaveChangesAsync();
			}
		}
	}
}