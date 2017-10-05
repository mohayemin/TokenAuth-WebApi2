using Api.Services;
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
			var token = _builder.Build("whatever");
			Assert.Equal(2, token.Count(c => c == '.'));
		}
	}
}
