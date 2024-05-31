using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]

    [SerializeField] private float startingHealth;
    [SerializeField] public float currentHealth { get; private set; }

    private bool dead;
    private MainMenu mainMenu;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private void Awake()
    {
        currentHealth = startingHealth;
        mainMenu = FindObjectOfType<MainMenu>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);        
        if (currentHealth >  0)
        {
            //Player hurt
            StartCoroutine(Invunerability());
            AudioManager.instance.PlayOneShot(FMODEvents.instance.loseLife, this.transform.position);
        }
        else
        {
            //Player dead
            if (!dead)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.unlifed, this.transform.position);
                //player dead
                GetComponent<Player>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                // show game over screen
                SceneManager.LoadScene("MainMenuScene");

                dead = true;

            }
            else
            {

            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10,11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

}
