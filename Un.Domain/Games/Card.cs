namespace Un.Domain.Games;

public enum CardColor
{
  Red,
  Blue,
  Yellow,
  Green
}

public enum CardValue
{
  Zero,
  One,
  Two,
  Three,
  Four,
  Five,
  Six,
  Seven,
  Eight,
  Nine
}

public readonly record struct Card(CardColor Color, CardValue Value);