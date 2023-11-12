namespace Un.Domain.Games;

public readonly record struct PlayerId(string Id)
{
    public static PlayerId Generate() => new(Guid.NewGuid().ToString());

    public override string ToString() => Id;
}