using Enrolly.Shared.Logging.Utils.Database;

namespace Enrolly.Documents.Infrastructure.Database;

public class DocumentsDbContextFactory : NpgsqlDbContextFactory<DocumentsDbContext> {}