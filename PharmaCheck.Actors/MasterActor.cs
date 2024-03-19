using Akka.Actor;
using PharmaCheck.Actors.Actors;
using PharmaCheck.Actors.Messages;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Actors;

public sealed class MasterActor : ReceiveActor
{
    IActorRef _medicineActor;
    IActorRef _countActor;

    public MasterActor()
    {
        _medicineActor = Context.ActorOf(Props.Create(() => new MedicineActor()), "medicine");
        _countActor = Context.ActorOf(Props.Create(() => new CartPersistenceActor()), "cart");

        Receive<MedicineDeliveryMessage>(message =>
        {
            Sender.Tell(_medicineActor.Ask<ResponseService<Guid>>(message));
        });

        Receive<IncrementMessage>(message => _countActor.Tell(message));
        Receive<DecrementMessage>(message => _countActor.Tell(message));
        Receive<GetStateMessage>(message => _countActor.Forward(message));
        Receive<SaveStateMessage>(message => _countActor.Tell(message));
    }
}