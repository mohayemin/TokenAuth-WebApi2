using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Db
{
	public class RefreshToken
	{
		[Key]
		public string UserId { get; set; }
		public string Token { get; set; }
		public DateTime Expires { get; set; }

		[ForeignKey(nameof(UserId))]
		public virtual IdentityUser User { get; set; }

		public RefreshToken() { }

		public RefreshToken(string userId, string token, DateTime expires)
		{
			UserId = userId;
			Token = token;
			Expires = expires;
		}
	}
}
