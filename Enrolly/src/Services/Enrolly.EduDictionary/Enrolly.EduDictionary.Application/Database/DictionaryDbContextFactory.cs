using Enrolly.Shared.Logging.Utils.Database;

namespace Enrolly.EduDictionary.Application.Database;

public class DictionaryDbContextFactory : NpgsqlDbContextFactory<DictionaryDbContext> { }