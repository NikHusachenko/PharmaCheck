using Akka.Actor;
using PharmaCheck.Actors.Messages;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Actors.Actors;

public sealed class MedicineActor : ReceiveActor
{
    public MedicineActor()
    {
        Receive<MedicineDeliveryMessage>(message =>
        {
            Sender.Tell(ResponseService<Guid>.Ok(Guid.NewGuid()));
        });
    }
}