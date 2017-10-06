using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Services;
using Api.Services.Samples;

namespace Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options => ConfigureJwt(options, services));

			services.AddMvc();

			services.AddSingleton<ITokenConfig, SampleTokenConfig>()
				.AddScoped<ITokenBuilder, JwtTokenBuilder>()
				.AddScoped<ITokenIssuer, SampleTokenIssuer>();
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

		private void ConfigureJwt(JwtBearerOptions options, IServiceCollection services)
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
		}
	}
}