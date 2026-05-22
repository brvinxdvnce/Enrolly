using Enrolly.Admissions.Domain.Entities;
using Enrolly.Shared.Logging.Utils.Database;

namespace Enrolly.Admissions.Infrastructure.Database;

public class AdmissionsDbContextFactory : NpgsqlDbContextFactory<AdmissionsDbContext> { }