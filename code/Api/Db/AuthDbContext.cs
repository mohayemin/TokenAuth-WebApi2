using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Db
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions options)
			: base(options) { }

		public DbSet<RefreshToken> RefreshTokens { get; set; }
	}
}
