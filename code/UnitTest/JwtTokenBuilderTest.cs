using Api.Services;
using Api.Services.Samples;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Xunit;

namespace UnitTest
{
	public class JwtTokenBuilderTest
	{
		private JwtTokenBuilder _builder;

		public JwtTokenBuilderTest()
		{
			_builder = new JwtTokenBuilder(new SampleTokenConfig());
		}

		[Fact]
		public void TokenHasTwoDots()
		{
			var user = new IdentityUser("adminuser")
			{
				Id = "123",
				Email="admin@nomail.com",
				NormalizedEmail = "admin@nomail.com",
				NormalizedUserName = "adminuser",
			};
			var token = _builder.Build(user, new[] { "admin" });
			Assert.Equal(2, token.AccessToken.Count(c => c == '.'));
		}
	}
}
