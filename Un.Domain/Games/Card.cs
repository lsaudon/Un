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

public readonly record struct Card(CardColor Color, CardValue Value);