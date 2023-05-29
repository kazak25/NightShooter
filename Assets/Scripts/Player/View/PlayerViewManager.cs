using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewManager : MonoBehaviour
{
   [SerializeField] private Slider healthSlider;
   [SerializeField] private Image damageImage;
   [SerializeField] private AudioClip deathClip;
   [SerializeField] private  Animator anim;
   [SerializeField] private AudioSource playerAudio;
   
    
    
    private CompositeDisposable _subscription;
    private void Start()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetPlayerDeadEvent>(Death),
            EventStream.Game.Subscribe<GetEnemyTakeDamageEvent>(TakeDamageAudio)
        };
    }
    public void Death(GetPlayerDeadEvent data)
    {
        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();
    }

    public void TakeDamageAudio(GetEnemyTakeDamageEvent data)
    {
        playerAudio.Play();
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
    private void TakeHealthBonus(int amount)
    {
        _currentHealth += amount;
        healthSlider.value = _currentHealth;
    }
}