using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyEvent : UnityEvent<Oppssum> { }
public class Oppssum : MonoBehaviour
{
    //public member
    public float speed = 4; //基础移动速度
    //chase
    public float chasespeed = 6;  //追击速度
    //idle
    public float idleTime = 2f;  //站立时间
    //Bleed
    public int MaxHP ;  //最大血量
    public int HP ;  //当前血量
    public int Maxbalance ;  //最大平衡值
    public int balance ;  //当前平衡值
    public float balanceRecoverSpeed; //平衡值恢复速度
    public float recoverBalance=0;      //累积恢复平衡值
    //cut
    public int cutforce = 1;  //攻击力
    public int cutCount = 2;  //攻击次数
    public float cutTime = 0.5f;  //攻击时间
    [Range(0,1)]
    public float cutEffect = 0.5f; //攻击生效时间点
    public float attackR = 0.5f;  //攻击半径
    //stiff
    public float stiffTime = 0.5f; //硬直时间
    public short stiffmulyiple = 1; //硬直时间倍数
    //defense
    public bool checkAttack = false;
    //be executed
    public bool norExecute = false;
    public bool backExecute = false;
    //key
    public bool haveKey = false;

    public bool listening = false;
    //Component
    public Transform execute;
    public Transform[] patrolPoint;
    public Transform[] chasePoint;
    public Transform target;
    public LayerMask targetLayer;//目标的图层
    public Transform attackPoint;//攻击范围圆心
    public StateController m_stateController;
    //events
    public EnemyEvent onAttackEffect;//敌人攻击生效事件
    // Start is called before the first frame update
    void Start()
    {
        m_stateController = GetComponent<StateController>();
        onAttackEffect = new EnemyEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //如果自身处于巡逻状态 && 主角位于身后 && 距离小于3
            if ((m_stateController.CurrentState() == "WarIdle" || m_stateController.CurrentState() == "WarRun")&&
                transform.localScale.x == -target.localScale.x &&
                0 < (target.position.x - transform.position.x) * transform.localScale.x &&
                (target.position.x - transform.position.x) * transform.localScale.x < 5)
            {
                //可以背部处决
                backExecute = true;
            }
            else
            {
                backExecute = false;
            }
        }
        //如果平衡值满了
        if (balance == Maxbalance)
        {
            //可以正面处决
            norExecute = true;
        }
        else
        {
            norExecute = false;
        }
        if(norExecute||backExecute)
        {
            execute.gameObject.SetActive(true);
        }
        else
        {
            execute.gameObject.SetActive(false);
        }
        if(balance>0&&(m_stateController.CurrentState()=="WarIdle"|| m_stateController.CurrentState() == "WarRun"))
        {
            recoverBalance += Time.deltaTime * balanceRecoverSpeed;
            if(recoverBalance>=1f)
            {
                balance -= 1;
                recoverBalance = 0;
            }
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackR);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fox"))
        {
            target = collision.transform;
            if (!listening)
            {
                Fox fox = collision.GetComponent<Fox>();
                fox.onCutStart.AddListener(OnPlayerCutStart);
                fox.onCutEnd.AddListener(OnPlayerCutEnd);
                fox.onAttackEffect.AddListener(OnGetAttack);
                listening = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fox"))
        {
            target = null;
            if (listening)
            {
                Fox fox = collision.GetComponent<Fox>();
                fox.onCutStart.RemoveListener(OnPlayerCutStart);
                fox.onCutEnd.RemoveListener(OnPlayerCutEnd);
                fox.onAttackEffect.RemoveListener(OnGetAttack);
                listening = false;
            }
        }
    }
    private void OnDestroy()
    {
        if(haveKey)
        {
            GameObject key = (GameObject)Instantiate(Resources.Load("Prefabs/key"));
            key.transform.position = transform.position;
            key.transform.Translate(0, 1, 0);
        }
    }
    /// <summary>
    /// 当视野范围内的主角发动攻击调用此函数
    /// </summary>
    void OnPlayerCutStart(Fox fox)
    {
        //如果在主角的攻击范围内
        if (Mathf.Abs(fox.attackPoint.position.x - transform.position.x) <= fox.attackR *3)
        {
            checkAttack = true;//进行防御
        }
    }
    /// <summary>
    /// 当视野范围内的主角攻击结束调用此函数
    /// </summary>
    void OnPlayerCutEnd()
    {
        checkAttack = false;
    }
    /// <summary>
    /// 监听函数，内部调用委托
    /// </summary>
    /// <param name="fox"></param>
    void OnGetAttack(Fox fox)
    {
        if(m_stateController.CurrentState()=="WarDefense")
        {
            DefenseAttack(fox);
        }
        else
        {
            NorGetAttack(fox);
        }
    }
    void NorGetAttack(Fox fox)
    {
        Debug.Log("NorGetAttack");
        //击退
        transform.Translate(2.0f * fox.transform.localScale.x, 0, 0);
        //进入受伤状态
        m_stateController.ChangeState("WarHurt");
        //血量减去主角攻击力
        HP -= fox.cutforce;
    }
    void DefenseAttack(Fox fox)
    {
        Debug.Log("格挡攻击");
        //平衡值增加
        balance++;
        //平衡条超过最大平衡值
        if (balance > Maxbalance)
        {
            //进入大硬直
            stiffmulyiple = 3;
            m_stateController.ChangeState("WarHurt");
            //平衡条清0
            balance = 0;
        }
    }
}
