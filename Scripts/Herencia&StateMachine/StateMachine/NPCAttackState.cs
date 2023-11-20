using UnityEngine;

public class NPCAttackState : MonoBehaviour
{
    private NPCStateMachine stateMachine;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform player;

    [SerializeField] private float minDistance;
    [SerializeField] private float timeElapse;
    [SerializeField] private float duration;
    [SerializeField] private bool canShoot;

    private void Awake()
    {
        stateMachine = GetComponent<NPCStateMachine>();
    }
    private void Update()
    {
        Timer();

        Fight();
    }

    private void Fight() // Método para intercambiar entre estados.
    {
        if (Vector2.Distance(transform.position, player.position) < minDistance)
        {
            stateMachine.FollowState();
        }
        else if (timeElapse >= duration && canShoot)
        {
            Attack();
            canShoot = false;
            Debug.Log("Atacando");
        }
    }

    public void Attack() // Método para instanciar el proyectil
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
    private void Timer() // Método para el tiempo entre disparos
    {
        timeElapse += Time.deltaTime;
        if (timeElapse > 1.1f)
        {
            canShoot = true;
            timeElapse = 0;
        }

    }
}
