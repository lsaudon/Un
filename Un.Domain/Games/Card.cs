namespace Un.Domain.Games;

public enum CardColor
{
    Red,
    Blue
}

public enum CardValue
{
    Zero,
    One
}

public record Card(CardColor Color, CardValue Value);