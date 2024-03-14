using Akka.Actor;
using PharmaCheck.Actors.Messages;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Actors;

public sealed class MasterActor : ReceiveActor
{
    IActorRef _medicineActor;

    public MasterActor()
    {
        _medicineActor = Context.ActorOf(Props.Create(() => new MedicineActor()), "medicine");

        Receive<MedicineDeliveryMessage>(async (message) =>
        {
            Sender.Tell(await _medicineActor.Ask<ResponseService<Guid>>(message));
        });
    }
}