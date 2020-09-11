using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    
    [SerializeField] float timeSinceLastHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] Slider healthSlider;
    private Animator anim;
    private CharacterMovement characterMovement;

    private new AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deadAudio;
    public AudioClip pickHealthItem;

    private new ParticleSystem particleSystem;
    public LevelManager levelManager;

    public bool moduleEnabled;

 


    public float Timer
    {
        get { return timer; }
        set
        {
            timer = 0.0f;
        }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement>();
        audio = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.enabled = moduleEnabled;
        levelManager = FindObjectOfType<LevelManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        PlayerKill();
    }

    void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit()
    {
        if(currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Player_Hit");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(hurtAudio);
        }
        if (currentHealth <= 0) 
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.SetTrigger("IsDead"); 
            characterMovement.enabled = false;
            audio.PlayOneShot(deadAudio);
        }
        
    }

    public void PowerUpHealth()
    {
        if (currentHealth <= 80)
        {
            currentHealth += 20;
        } else if (currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }

        healthSlider.value = currentHealth;
        audio.PlayOneShot(pickHealthItem);
    }

    public void PowerUp()
    {
        characterMovement.PowerUpJump();
        StartCoroutine(PowerUpRoutine());
    }

    public IEnumerator PowerUpRoutine()
    {

        yield return new WaitForSeconds(5);
        print("nuevos segundos");
        print("reset");
        characterMovement.ResetPowerUp();
    }

    public void KillBox()
    {
        CurrentHealth = 0;
        healthSlider.value = currentHealth;
    }

    public void PlayerKill()
    {
        if (currentHealth == 0)
        {
            levelManager.RespawnPlayer();
        }
    }

}
