using Un.Domain.Games.Events;

namespace Un.Domain.Games.Handlers;

public class UpdateState : IEventHandler<GameStarted>,
                           IEventHandler<CardPlayed>

{
  private readonly IGameStateRepository _repository;

  public UpdateState(IGameStateRepository repository)
  {
    _repository = repository;
  }

  public void Handle(GameStarted evt)
  {
    _repository.Save(new GameStateProjection(evt.Id, evt.Card));
  }

  public void Handle(CardPlayed evt)
  {
    GameStateProjection stateOfGame = _repository.GetStateOfGame(evt.Id);
    _repository.Save(stateOfGame with { Card = evt.Card });
  }
}