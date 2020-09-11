using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItemScript : MonoBehaviour
{
    private GameObject player;
    private new AudioSource audio;
    private new ParticleSystem particleSystem;
    private PlayerHealth playerHealth;

    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticle;

    public bool moduleEnabled;

    public GameObject pickupEffect;

    private ItemExplode itemExplode;
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;

        particleSystem = player.GetComponent<ParticleSystem>();

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        brainParticle = GetComponent<ParticleSystem>();

        itemExplode = GetComponent<ItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine( InvincibleRoutine());
            meshRenderer.enabled = false;
            
        }
    }

    public IEnumerator InvincibleRoutine()
    {
        itemExplode.PickUp();
        sphereCollider.enabled = false;
        var emission = particleSystem.emission;
        emission.enabled = !moduleEnabled;
        playerHealth.enabled = false;
        var brainEmission = brainParticle.emission;
        brainEmission.enabled = moduleEnabled;
        playerHealth.Timer = 0;

        yield return new WaitForSeconds(10.0f);
        emission.enabled = moduleEnabled;
        playerHealth.enabled = true;
        Destroy(gameObject);
    }

    void PickUp()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
}
