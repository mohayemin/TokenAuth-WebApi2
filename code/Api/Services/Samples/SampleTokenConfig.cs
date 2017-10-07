
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Services.Samples
{
    public class SampleTokenConfig : ITokenConfig
    {
		public string Issuer => "BenzeneSoft";
		public string Audiance => "BenzeneSoft";
		public double AccessTokenLifeTimeMinutes => 60;

		public int RefreshTokenLength => 20;
		public double RefreshTokenLifetimeHours => 24;

		public SigningCredentials SigningCredentials => new SigningCredentials(
												new SymmetricSecurityKey(Encoding.ASCII.GetBytes("C6H6 is benzene and the key needs to be really long")),
												SecurityAlgorithms.HmacSha256);
	}
}
