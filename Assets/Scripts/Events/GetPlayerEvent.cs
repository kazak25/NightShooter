using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GetPlayerEvent : EventBase
    {
        public Transform PlayerPoint { get; } 
        public PlayerHealth PlayerHealth { get; }

        public GetPlayerEvent(Transform playerPoint, PlayerHealth playerHealth)
        {
            PlayerPoint = playerPoint;
            PlayerHealth = playerHealth;
        }
    }
}