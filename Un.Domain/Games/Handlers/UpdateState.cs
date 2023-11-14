using Un.Domain.Games.Events;

namespace Un.Domain.Games.Handlers;

public class UpdateState(IGameStateRepository repository) : IEventHandler<GameStarted>, IEventHandler<CardPlayed>
{
  public void Handle(GameStarted evt)
  {
    repository.Save(new GameStateProjection(evt.Id, evt.Card));
  }

  public void Handle(CardPlayed evt)
  {
    GameStateProjection stateOfGame = repository.GetStateOfGame(evt.Id);
    repository.Save(stateOfGame with { Card = evt.Card });
  }
}