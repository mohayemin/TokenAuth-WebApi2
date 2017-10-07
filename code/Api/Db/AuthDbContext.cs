using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Db
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions options)
			: base(options) {
		}
	}
}
