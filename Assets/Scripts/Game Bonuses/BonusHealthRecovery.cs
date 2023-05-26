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
        // float rotationAmount = rotationSpeed * Time.deltaTime;
        // transform.Rotate(0, -rotationAmount, 0);
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
