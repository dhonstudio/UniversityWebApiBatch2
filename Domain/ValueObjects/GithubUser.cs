namespace Domain.ValueObjects
{
    public class GithubUser
    {
        public required string Login { get; set; }
        public string? Name { get; set; }
    }
}
