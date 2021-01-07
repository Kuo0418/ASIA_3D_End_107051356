using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0,50)]
    public float speed = 3;
    [Header("停止距離"), Range(0,50)]
    public float stopDistance = 2.5f;
    [Header("冷卻時間"), Range(0, 50)]
    public float CD = 2f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"),Range(0f,5f)]
    public float atklength;

    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;

    private float timer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        player = GameObject.Find("機器人").transform;

        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atklength);
    }

    private RaycastHit hit;

    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;
            pos.y = transform.position.y;

            if (timer >= CD)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

             Physics.Raycast(atkPoint.position, atkPoint.forward, atklength, 1 << 8);
                
            }
        }
    }

    private void Track()
    {
        nav.SetDestination(player.position);
        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);
    }
}
