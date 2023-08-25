using Core.Domain;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance;

public class UserRepository : IUserRepository
{
    IAppDbContext _db;

    public UserRepository(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<User> GetByIdAsync(long id)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

}