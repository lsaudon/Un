using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Un.GrpcService.Services;

public class UnService : Un.UnBase
{
  public override Task<StartGameReply> StartGame(Empty request, ServerCallContext context) =>
    Task.FromResult(new StartGameReply { GameId = Guid.NewGuid().ToString() });
}