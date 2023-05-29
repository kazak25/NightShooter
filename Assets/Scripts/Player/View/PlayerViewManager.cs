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
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource playerAudio;

    private bool damaged;
    private Color _flashColour = new Color(1f, 0f, 0f, 0.1f);
    private float _flashSpeed = 5f;


    private CompositeDisposable _subscription;

    private void Start()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetPlayerDeadEvent>(Death),
            EventStream.Game.Subscribe<GetEnemyTakeDamageEvent>(TakeDamageAudio),
            EventStream.Game.Subscribe<GetCurrentHealthEvent>(TakeHealthBonus),
            EventStream.Game.Subscribe<GetDamageEvent>(GetDamage)
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
    private void GetDamage(GetDamageEvent data)
    {
        damaged = true;
    }

    private void TakeHealthBonus(GetCurrentHealthEvent data)
    {
        healthSlider.value = data.CurrentHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = _flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, _flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}