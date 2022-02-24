namespace Rova.Model.AuthProvider
{
    public class AuthRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}