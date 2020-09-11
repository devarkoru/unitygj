using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Move : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private Enemy01Health enemy01Health;


    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemy01Health = GetComponent<Enemy01Health>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.position, this.transform.position) < 12)
        {
            if (!GameManager.instance.GameOver && enemy01Health.IsAlive)
            {
                nav.SetDestination(player.position);
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsIdle", false);
            }
            else if (GameManager.instance.GameOver || !enemy01Health.IsAlive)
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsIdle", true);
                nav.enabled = false;
            }
        }
        
    }
}
