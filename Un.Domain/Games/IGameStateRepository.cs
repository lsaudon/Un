namespace Un.Domain.Games;

public interface IGameStateRepository
{
  void Save(GameStateProjection projection);
  GameStateProjection GetStateOfGame(GameId id);
}