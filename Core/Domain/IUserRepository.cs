namespace Core.Domain
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long id);
    }
}