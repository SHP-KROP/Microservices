namespace AuthServer.DTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; init; } = Guid.NewGuid().ToString();

        public string Name { get; init; }

        public string Surname { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }
    }
}
