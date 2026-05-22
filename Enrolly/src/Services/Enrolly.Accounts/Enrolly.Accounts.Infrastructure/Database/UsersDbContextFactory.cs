using Enrolly.Shared.Logging.Utils.Database;

namespace Enrolly.Accounts.Infrastructure.Database;

public class UsersDbContextFactory : NpgsqlDbContextFactory<UsersDbContext>{ }