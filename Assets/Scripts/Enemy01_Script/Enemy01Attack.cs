using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [SerializeField] private float range = 3.0f;
    [SerializeField] private float timeBetweenAttack = 1.0f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;
    private Enemy01Health enemy01Health;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        enemy01Health = GetComponent<Enemy01Health>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance (transform.position, player.transform.position) < range && enemy01Health.IsAlive)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    IEnumerator attack()
    {
        if(playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Enemy01_Attack");
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        yield return null;
        StartCoroutine(attack());
    }


}
