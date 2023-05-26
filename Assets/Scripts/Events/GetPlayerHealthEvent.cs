using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetPlayerHealthEvent : EventBase
    {
        public PlayerHealth PlayerHealth { get; }

        public GetPlayerHealthEvent(PlayerHealth playerHealth)
        {
            PlayerHealth = playerHealth;
        }
        
    }
}