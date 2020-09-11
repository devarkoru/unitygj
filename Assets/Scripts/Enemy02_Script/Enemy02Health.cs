using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy02Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2.0f;
    [SerializeField] private int currentHealth;

    private float timer = 0.0f;
    private Animator anim;
    private bool isAlive;
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private new AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;

    private DropItems dropItems;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        dropItems = GetComponent<DropItems>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                takeHit();
                timer = 0.0f;
            }
        }
    }

    void takeHit()
    {
        if (currentHealth > 0)
        {
            anim.Play("Enemy02_HitFront");
            currentHealth -= 10;
            audio.PlayOneShot(hurtAudio);
        }

        if(currentHealth <= 0)
        {
            isAlive = false;
            killEnemy();
        }
    }

    void killEnemy()
    {
        capsuleCollider.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        audio.PlayOneShot(killAudio);

        StartCoroutine(removeEnemy());
        dropItems.Drop();
    }


    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

}
