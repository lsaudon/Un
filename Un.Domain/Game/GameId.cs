namespace Un.Domain.Game;

public record GameId(string Id)
{
    public static GameId Generate() => new(Guid.NewGuid().ToString());

    public override string ToString() => Id;
}