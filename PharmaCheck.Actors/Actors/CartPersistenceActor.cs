using Akka.Persistence;
using Akka.Persistence.Snapshot;
using PharmaCheck.Actors.Messages;

namespace PharmaCheck.Actors.Actors;

public sealed class CartPersistenceActor : UntypedPersistentActor
{
    private const int DEFAULT_COUNTER_STATE = 0;
    private int _count = DEFAULT_COUNTER_STATE;

    public override string PersistenceId => Context.Self.Path.Name;

    protected override void OnCommand(object message)
    {
        if (message is LoadSnapshotResult result)
        {
            if (result.Snapshot != null)
            {
                _count = (int)result.Snapshot.Snapshot;
            }
        }

        if (message is IncrementMessage increment)
        {
            _count++;
            return;
        }

        if (message is DecrementMessage decrement)
        {
            _count--;
            return;
        }

        if (message is SaveStateMessage saveState)
        {
            SaveSnapshot(_count);
            return;
        }

        if (message is GetStateMessage getState)
        {
            Sender.Tell(_count, Context.Self);
            return;
        }

        if (message is ClearStateMessage clearState)
        {
            DeleteSnapshots(new SnapshotSelectionCriteria(LastSequenceNr));
            return;
        }

        if (message is DeleteSnapshotsSuccess deleteSnapshotsSuccess)
        {
            _count = DEFAULT_COUNTER_STATE;
            return;
        }
    }

    protected override void OnRecover(object message)
    {
        LoadSnapshot(PersistenceId, new SnapshotSelectionCriteria(LastSequenceNr), LastSequenceNr);
    }
}