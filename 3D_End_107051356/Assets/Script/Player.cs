using UnityEngine;
using Invector.vCharacterController;

public class Player : MonoBehaviour
{
    private float HP = 100;
    private Animator ani;
    private int atkCount;
    private float timer;

    [Header("連續間格時間"), Range(0, 3)]
    public float interval = 1;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atklength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 50;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atklength);
    }

    private RaycastHit hit;

    private void Attack()
    {
        if (atkCount < 3)
        {
            if (timer < interval)
            {
                timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    atkCount++;
                    timer = 0;
                    if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atklength, 1 << 9))
                    {
                        hit.collider.GetComponent<Enemy>().Damage(atk);
                    }
                }
            }
            else
            {
                timer = 0;
                atkCount = 0;
            }
        }
        if (atkCount == 3)
        {
            atkCount = 0;
        }
        ani.SetInteger("連擊", atkCount);
    }

    public void Damage(float damage)
    {
        HP -= damage;
        ani.SetTrigger("受傷觸發");
        if (HP <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        ani.SetTrigger("死亡觸發");
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
}
