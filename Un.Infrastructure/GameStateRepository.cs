using Un.Domain.Games;

namespace Un.Infrastructure;

public class GameStateRepository : IGameStateRepository
{
  private readonly Dictionary<GameId, GameStateProjection> _projectionsById =
    [];

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
        return _projectionsById.TryGetValue(id, out GameStateProjection game) ? game : throw new NotSupportedException();
    }
}