namespace Un.Domain.Games;

public readonly record struct GameId(string Id)
{
  public static GameId Generate() => new(Guid.NewGuid().ToString());

  public override string ToString() => Id;
}