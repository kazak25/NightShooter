using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealthController playerHealthController;
    
    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealthController.CurrentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}