using Domain.ValueObjects;

namespace Application.Interfaces.Repositories
{
    public interface IGithubRepository
    {
        public Task<GithubUser> GetUser(string username);
    }
}
