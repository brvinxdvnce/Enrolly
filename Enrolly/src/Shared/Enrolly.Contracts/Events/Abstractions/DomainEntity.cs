namespace Enrolly.Contracts.Events.Abstractions;

public abstract class DomainEntity
{
    private readonly List<IEvent> _events = new();
    public IReadOnlyList<IEvent> Events => this._events;

    public void AddEvent(IEvent e) { _events.Add(e); }

    public void ClearEvents() { _events.Clear(); }
}