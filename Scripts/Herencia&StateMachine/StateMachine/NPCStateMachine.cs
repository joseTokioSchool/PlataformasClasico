using UnityEngine;

public class NPCStateMachine : MonoBehaviour // MÁQUINA DE ESTADOS
{
    public MonoBehaviour NPCAttackState;
    public MonoBehaviour NPCFollowState;
    public MonoBehaviour NPCDeathState;

    private void Awake()
    {
        NPCAttackState.enabled = true;
        NPCFollowState.enabled = false;
        NPCDeathState.enabled = false;
    }
    public void AttackState()
    {
        NPCAttackState.enabled = true;
        NPCFollowState.enabled = false;
        NPCDeathState.enabled = false;
    }
    public void FollowState()
    {
        NPCAttackState.enabled = false;
        NPCFollowState.enabled = true;
        NPCDeathState.enabled = false;
    }
    public void DeathState()
    {
        NPCAttackState.enabled = false;
        NPCFollowState.enabled = false;
        NPCDeathState.enabled = true;
    }
}
