namespace Eventy.Service.Domain.Hash.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}
