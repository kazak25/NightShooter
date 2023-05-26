using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Events;
using JetBrains.Annotations;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    
    public UnityEvent HeartDestroyed;
    
    
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    // private void Start()
    // {
    //     var eventDataRequest = new GetPlayerHealthEvent(this);
    //     EventStream.Game.Publish(eventDataRequest);
    // }

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    private void TakeHealthBonus(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth;
    }

    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
    [UsedImplicitly]
    private void OnCollisionEnter(Collision heart)
    {
        if (heart.collider.CompareTag("Heart"))
        {
            
            Debug.Log("Good");
            TakeHealthBonus(20);
            HeartDestroyed.Invoke();
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }
        }
    }

    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
