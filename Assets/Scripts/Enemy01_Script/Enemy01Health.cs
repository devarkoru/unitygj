using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2.0f;
    [SerializeField] private int currentHealth;

    private float timer = 0.5f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    private CapsuleCollider enemyCollider;

    private new AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deadAudio;

    public bool IsAlive
    {
        get {return isAlive; }

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        weaponCollider = GetComponentInChildren<BoxCollider>();
        enemyCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0f;
            }
        }
    }

    void TakeHit()
    {
        if (currentHealth > 0)
        {            
            anim.Play("Enemy01_HitFront");
            currentHealth -= 10;
            audio.PlayOneShot(hurtAudio);
        }
        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
            
            
        }
    }

    void KillEnemy()
    {
        anim.SetTrigger("IsEnemy01Dead");
        capsuleCollider.enabled = false;
        nav.enabled = false;        
        rigidbody.isKinematic = true;
        weaponCollider.enabled = false;
        enemyCollider.enabled = false;
        audio.PlayOneShot(deadAudio);
        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
