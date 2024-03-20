using Akka.Actor;
using Akka.Actor.Setup;
using Akka.Configuration;
using Akka.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PharmaCheck.Actors;

public sealed class ActorService : IHostedService
{
    private const string MASTER_ACTOR_IS_NULL_ERROR = "MasterActor is null";

    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _applicationLifetime;

    private ActorSystem _system;
    private IActorRef _masterActor;

    public IActorRef MasterActor => _masterActor ?? throw new InvalidOperationException(MASTER_ACTOR_IS_NULL_ERROR);

    public ActorService(IConfiguration configuration, 
        IServiceProvider serviceProvider, 
        IHostApplicationLifetime applicationLifetime)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _applicationLifetime = applicationLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        BootstrapSetup setup = BootstrapSetup.Create();

/*        Config config = ConfigurationFactory.ParseString(@"
            akka {
                actor {
                    provider = ""Akka.Actor.LocalActorRefProvider""
                }
            }
        ");*/

        DependencyResolverSetup diResolver = DependencyResolverSetup.Create(_serviceProvider);
        ActorSystemSetup systemSetup = setup.And(diResolver);

        _system = ActorSystem.Create("pharma-check", systemSetup);
        _masterActor = _system.ActorOf(Props.Create(() => new MasterActor()), "master-actor");

#pragma warning disable CS4014
        _system.WhenTerminated.ContinueWith(_ =>
        {
            _applicationLifetime.StopApplication();
        });
#pragma warning restore CS4014

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await CoordinatedShutdown.Get(_system).Run(CoordinatedShutdown.ClrExitReason.Instance);
    }
}