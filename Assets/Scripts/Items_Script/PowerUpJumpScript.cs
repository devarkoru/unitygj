using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJumpScript : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement characterMovement;

    private PlayerHealth playerHealth;
    public float jumpPowerUp = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        characterMovement = GetComponent<CharacterMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            print("test");
            print("test powerup");
            playerHealth.PowerUp();
            print("segundos");
            
            Destroy(gameObject);
            

        }
    }


}
