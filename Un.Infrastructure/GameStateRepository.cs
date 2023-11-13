using Un.Domain.Games;

namespace Un.Infrastructure;

public class GameStateRepository : IGameStateRepository
{
  private readonly IDictionary<GameId, GameStateProjection> _projectionsById =
    new Dictionary<GameId, GameStateProjection>();

  public void Save(GameStateProjection projection)
  {
    if (_projectionsById.ContainsKey(projection.GameId))
    {
      _projectionsById[projection.GameId] = projection;
      return;
    }

    _projectionsById.Add(projection.GameId, projection);
  }

  public GameStateProjection GetStateOfGame(GameId id)
  {
    if (_projectionsById.TryGetValue(id, out GameStateProjection game))
    {
      return game;
    }

    throw new NotSupportedException();
  }
}