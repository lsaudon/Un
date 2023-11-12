namespace Un.Domain.Game;

public record GameId(String Id)
{
    public static GameId Generate() => new(Guid.NewGuid().ToString());

    public override string ToString() => Id;
}