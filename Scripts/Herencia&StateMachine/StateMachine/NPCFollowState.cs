using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowState : MonoBehaviour
{
    private NPCStateMachine stateMachine;

    [SerializeField] Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;

    private void Awake()
    {
        stateMachine = GetComponent<NPCStateMachine>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > minDistance)
        {
            stateMachine.AttackState();
        }
        else
        {
            Follow();
            Debug.Log("Siguiendo");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // Función para que Goomba pueda ser eliminado por los caparazones de Koopa.
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();
            player.Hit();
        }
    }
    public void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, 3.14f), speed * Time.deltaTime);
    }
}
