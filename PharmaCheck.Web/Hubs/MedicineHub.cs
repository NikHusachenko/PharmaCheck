﻿using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using PharmaCheck.Actors;
using PharmaCheck.Actors.Messages;
using PharmaCheck.Services.Response;
using PharmaCheck.Web.Responses;

namespace PharmaCheck.Web.Hubs
{
    public class MedicineHub : Hub
    {
        private readonly IActorRef _masterActor;

        public MedicineHub(ActorService actorService)
        {
            _masterActor = actorService.MasterActor;
        }

        public async Task MoveMedicine(MedicineDeliveryMessage model)
        {
            ResponseService<Guid> response = await _masterActor.Ask<ResponseService<Guid>>(model);
            
            await Clients.Caller.SendAsync("MedicineDelivery", new MedicineDeliveryHttpGetModel()
            {
                Id = response.Value
            });
        }

        public async Task Increment() => _masterActor.Tell(new IncrementMessage());

        public async Task Decrement() => _masterActor.Tell(new DecrementMessage());

        public async Task GetStatus()
        {
            await Clients.Caller.SendAsync("MedicineDelivery", new { value = await _masterActor.Ask<int>(new GetStateMessage()) });
        }

        public async Task SaveStatus() => _masterActor.Tell(new SaveStateMessage());

        public async Task ClearStatus() => _masterActor.Tell(new ClearStateMessage());
    }
}