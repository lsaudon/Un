namespace Un.Domain.Games;

public record PlayerId(string Id)
{
    public static PlayerId Generate() => new(Guid.NewGuid().ToString());

    public override string ToString() => Id;
}