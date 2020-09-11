using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timer = 0.0f;
    public float waitTime = 2.0f;

    public GameObject currentCheckpoint;
    private GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        print("Respawn activated");
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            print("Player respawn");
            player.transform.position = currentCheckpoint.transform.position;
            playerHealth.CurrentHealth = 100;
        }
        
    }
}
