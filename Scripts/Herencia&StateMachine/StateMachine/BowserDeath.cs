using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserDeath : MonoBehaviour
{
    private NPCStateMachine stateMachine;
    private void Awake()
    {
        stateMachine = GetComponent<NPCStateMachine>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.DotTest(transform, Vector2.down))
        {
            stateMachine.DeathState();
        }
    }
}
