using Akka.Persistence;
using PharmaCheck.Actors.Messages;

namespace PharmaCheck.Actors.Actors;

public sealed class CartPersistenceActor : PersistentActor
{
    private int _count = 0;

    public override string PersistenceId => "cart";


    protected override bool ReceiveCommand(object message)
    {
        if (message is IncrementMessage increment)
        {
            _count++;
            return true;
        }

        if (message is DecrementMessage decrement)
        {
            _count--;
            return true;
        }

        return false;
    }

    protected override bool ReceiveRecover(object message)
    {
        if (message is SaveStateMessage saveState)
        {
            Persist<int>(_count, (state) =>
            {
                int a = 10;
            });
        }

        return false;
    }
}