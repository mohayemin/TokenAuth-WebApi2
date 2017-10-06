
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Services.Samples
{
    public class SampleTokenConfig : ITokenConfig
    {
		public string Issuer => "BenzeneSoft";

		public string Audiance => "BenzeneSoft";

		public double LifeTimeMinutes => 1;

		public SigningCredentials SigningCredentials => new SigningCredentials(
												new SymmetricSecurityKey(Encoding.ASCII.GetBytes("C6H6 is benzene and the key needs to be really long")),
												SecurityAlgorithms.HmacSha256);
	}
}
