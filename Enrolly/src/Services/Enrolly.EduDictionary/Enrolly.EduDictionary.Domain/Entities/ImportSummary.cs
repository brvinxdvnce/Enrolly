namespace Enrolly.EduDictionary.Domain.Entities;

public class ImportSummary
{
    public ImportSummary ( ) {}

    private ImportSummary(string collectionTypeName)
    {
        Id = Guid.NewGuid();
        StartedAt = DateTime.UtcNow;
        CollectionName = collectionTypeName;
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CollectionName { get; private set; }
    public DateTime StartedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; private set; }
    public int Received { get; set; } = 0;
    public int Added { get; set; } = 0;
    public int Removed { get; set; } = 0;
    public int Updated { get; set; } = 0;

    public static ImportSummary StartImport<T>() where T : class
    {
        return new ImportSummary(typeof(T).Name);
    }

    public void StopImport()
    {
        CompletedAt = DateTime.UtcNow;
    }
    
    public void AddStats((int received, int added, int updated, int deleted) res)
    {
        Received += res.received;
        Added += res.added;
        Updated += res.updated;
        Removed += res.deleted;
    }
}