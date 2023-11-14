namespace Un.Domain;

public abstract class DecisionProjectionBase
{
  private readonly Dictionary<Type, Action<IDomainEvent>> _handlersByType = [];

  public void Apply(IDomainEvent evt)
  {
    if (_handlersByType.TryGetValue(evt.GetType(), out Action<IDomainEvent>? apply))
    {
      apply(evt);
    }
  }

  protected void AddHandler<T>(Action<T> apply)
    where T : IDomainEvent
  {
    _handlersByType.Add(typeof(T), o => apply((T)o));
  }
}