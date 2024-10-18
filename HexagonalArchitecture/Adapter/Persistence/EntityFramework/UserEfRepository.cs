using HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

internal sealed class UserEfRepository(UserContext context) : IUserRepository
{
    private bool _disposedValue;
    private readonly IFunction<User, UserModel> _mapUserToModel = UserMappingFactory.UserToModelMapper();

    private readonly IFunction<UserModel, User>
        _mapUserModelToDomain = UserMappingFactory.UserModelToDomainMapper();

    public async Task<User> ChangeUserDetails(User domain)
    {
        ArgumentNullException.ThrowIfNull(domain, "User cannot be null");

        var model = await context.Users.FirstOrDefaultAsync(model => model.UserId == domain.UserId.Value);

        if (model is null)
        {
            throw new KeyNotFoundException($"User with ID {domain.UserId.Value} not found.");
        }

        var modifiedModel = UserModeller.ApplyChangesFrom(domain).To(model);
        context.Entry(modifiedModel).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return domain;
    }

    public async Task DeleteBy(UserId id)
    {
        ArgumentNullException.ThrowIfNull(id, "UserId cannot be null");

        var model = await context.Users.FirstOrDefaultAsync(model => model.UserId == id.Value);

        if (model != null)
        {
            context.Users.Remove(model);
        }

        await context.SaveChangesAsync();
    }

    public async Task<User> FindBy(UserId id)
    {
        ArgumentNullException.ThrowIfNull(id, "UserId cannot be null");

        var model = await context.Users.FirstOrDefaultAsync(model => model.UserId == id.Value);

        return _mapUserModelToDomain.Apply(model);
    }

    public async Task<User> FindBy(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress, "Email address cannot be null!");

        var model = await context.Users.FirstOrDefaultAsync(model => model.EmailAddress == emailAddress.value);

        return _mapUserModelToDomain.Apply(model);
    }

    public async Task<List<User>> ListAll()
    {
        var model = await context.Users.ToListAsync();
        var domain = new List<User>(0);

        model.ForEach(user => domain.Add(_mapUserModelToDomain.Apply(user)));

        return domain;
    }

    public async Task Insert(User domain)
    {
        ArgumentNullException.ThrowIfNull(domain, "User cannot be null");

        var model = _mapUserToModel.Apply(domain);
        await context.Users.AddAsync(model);

        await context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            context.Dispose();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}