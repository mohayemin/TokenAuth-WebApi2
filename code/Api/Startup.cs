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

namespace Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AuthDbContext>(options =>
			{
				options.UseInMemoryDatabase("authdb");
			});

			ConfigureIdentity(services);
			ConfigureAuth(services);

			services.AddMvc();

			services.AddSingleton<ITokenConfig, SampleTokenConfig>()
				.AddScoped<ITokenBuilder, JwtTokenBuilder>()
				.AddScoped<ITokenIssuer, IdentityTokenIssuer>()
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
		}

		private void ConfigureIdentity(IServiceCollection services)
		{
			var builder = services.AddIdentityCore<IdentityUser>(opt =>
			{
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 1;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
			}).AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<AuthDbContext>()
			.AddSignInManager<SignInManager<IdentityUser>>()
			.AddDefaultTokenProviders();
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
	}
}