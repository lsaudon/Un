namespace Un.Domain.Game;

public record GameId(Guid Id)
{
    public static GameId Generate()
    {
        return new GameId(Guid.NewGuid());
    }
}