using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Events;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;
using UnityEngine.Events;

public class PlayerHealthController : MonoBehaviour
{
    public UnityEvent HeartDestroyed;
    public int CurrentHealth => _currentHealth;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private int _startingHealth = 100;
    [SerializeField] private int _currentHealth;

    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    bool isDead;
    bool damaged;


    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        _currentHealth -= amount;

        healthSlider.value = _currentHealth;

        var eventDataRequest = new GetEnemyTakeDamageEvent();
        EventStream.Game.Publish(eventDataRequest);

        if (_currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void TakeHealthBonus(int amount)
    {
        _currentHealth += amount;
        healthSlider.value = _currentHealth;
    }           

    void Death()
    {
        isDead = true;

        _playerShooting.DisableEffects();

        var eventDataRequest = new GetPlayerDeadEvent();
        EventStream.Game.Publish(eventDataRequest);

        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }

    [UsedImplicitly]
    private void OnCollisionEnter(Collision heart)
    {
        if (heart.collider.CompareTag("Heart"))
        {
            TakeHealthBonus(20);
            HeartDestroyed.Invoke();
            if (_currentHealth > 100)
            {
                _currentHealth = 100;
            }
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}