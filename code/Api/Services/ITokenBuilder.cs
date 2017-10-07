
namespace Api.Services
{
	public interface ITokenBuilder
    {
		Token Build(string username);
    }
}
