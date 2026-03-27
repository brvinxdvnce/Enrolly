namespace Enrolly.EduDictionary.Domain.Entities;

public class ImportSummary
{
    private ImportSummary ( ) {}

    private ImportSummary(string collectionTypeName)
    {
        Id = Guid.NewGuid();
        StartedAt = DateTime.Now;
        CollectionName = collectionTypeName;
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CollectionName { get; private set; }
    public DateTime StartedAt { get; private set; } = DateTime.Now;
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
        CompletedAt = DateTime.Now;
    }
    
    public void AddStats((int received, int added, int updated, int deleted) res)
    {
        Received += res.received;
        Added += res.added;
        Updated += res.updated;
        Removed += res.deleted;
    }
}