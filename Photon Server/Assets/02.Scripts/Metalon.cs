using UnityEngine;
using UnityEngine.AI;

public enum STATE
{
    MOVE,
    ATTACK,
    DIE,
}

public class Metalon : MonoBehaviour
{
    private int health;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] Transform target;
    STATE state;

    void Start()
    {
        health = 100;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Turret Tower").transform;
    }

    void Update()
    {
        switch (state)
        {
            case STATE.DIE:
                Die();
                break;

            case STATE.MOVE:
                Move();
                break;

            case STATE.ATTACK:
                Attack();
                break;
        }
    }

    public void Move()
    {
        agent.SetDestination(target.position);
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void Die()
    {
        animator.Play("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turret Tower"))
        {
            state = STATE.ATTACK;
        }
    }
}