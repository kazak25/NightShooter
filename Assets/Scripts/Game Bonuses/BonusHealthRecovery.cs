using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;
public class BonusHealthRecovery : MonoBehaviour
{
    private int rotationSpeed = 100;
    private void Update()
    {
        transform.Rotate(0,0 , rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision player)
    {
        
        if (player.collider.CompareTag("Player"))
        {
            Debug.Log("Heart");
            Destroy(gameObject);
        }
    }
}
