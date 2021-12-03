using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerEvent : UnityEvent<Fox> { }
public class Fox : MonoBehaviour
{
    //public member
    public float speed = 10; //基础移动速度
    //jump
    public float jumpforce =20;  //跳跃力
    public int jumpCount = 2;  //跳跃次数
    //sprint  
    public float sprintspeed = 60;  //冲刺速度
    public float sprintTime = 0.1f;  //冲刺时间
    public float sprintCd;     //冲刺冷却时间
    public float sprintCurrCd =0;  //当前剩余冷却时间
    //Bleed
    public int MaxRed = 5;  //最大血量
    public int Red = 5;  //当前血量
    public int Maxbalance = 3;  //最大平衡值
    public int balance = 0;  //当前平衡值
    public float balanceRecoverSpeed; //平衡值恢复速度
    //物品
    public int numMedicine = 0;//药品数量
    public bool haveKey = false;//是否有钥匙
    //cut
    public int cutforce = 1;  //攻击力
    public int cutCount = 2;  //攻击次数
    public float cutTime = 0.5f;  //攻击时间
    [Range(0, 1)]
    public float cutEffect = 0.5f; //攻击生效时间点
    public float attackR=0.5f;  //攻击半径
    //execute
    public float executeSpeed = 200f;//处决移动速度
    //stiff
    public float stiffTime = 0.5f; //硬直时间
    //Obsolete
    [System.Obsolete("", true)]
    public float shutStartdistance = 1;
    [System.Obsolete("", true)]
    public float crouchdistance = -0.5f;
    [System.Obsolete("", true)]
    public bool shutPressed = false;
    [System.Obsolete("",true)]
    public float sprintdistance = 10;
    //available
    public bool norExecuteable = false;
    public bool backExecuteable = false;
    //Inputs
    public bool cutPressed = false;
    public bool sprintPressed = false;
    public bool silkPressed = false;
    public bool jumpPressed = false;
    public bool defensePressed = false;
    public bool climbPressed = false;
    public bool inLadder = false;

    public bool listening = false;//是否监听
    
    public Transform attackPoint;
    public Transform silkStart;
    public Transform CellingCheck;
    public Transform Ladder;
    public List<Transform> nearEnemies;
    public Transform enemy; //敌人
    public LayerMask enemies; //敌人图层
    public LayerMask layerMask;
    public Vector3 shutPoint;
    public StateController m_stateController;
    //events
    public PlayerEvent onCutStart;
    public UnityEvent onCutEnd;
    public PlayerEvent onAttackEffect;
    // Start is called before the first frame update
    void Start()
    {
        m_stateController = GetComponent<StateController>();
        onCutStart = new PlayerEvent();
        onCutEnd = new UnityEvent();
        onAttackEffect = new PlayerEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //输入获取
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        if (Input.GetButtonDown("Grouch"))
        {
            defensePressed = true;
        }
        if (Input.GetButtonUp("Grouch"))
        {
            defensePressed = false;
        }
        if (Input.GetButtonDown("Vertical"))
        {
            climbPressed = true;
        }
        if (Input.GetButtonUp("Vertical"))
        {
            climbPressed = false;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            silkPressed = true;
            shutPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        //刷新冷却时间
        if(sprintCurrCd>0)
        {
            sprintCurrCd -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire3")&&sprintCurrCd<=0)
        {
            sprintPressed = true;
        }
        if (enemy)
        {
            norExecuteable = enemy.GetComponent<Oppssum>().norExecute;
            backExecuteable = enemy.GetComponent<Oppssum>().backExecute;
        }
        else
        {
            norExecuteable = false;
            backExecuteable = false;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            cutPressed = true;
        }
        //获得面前近的敌人
        FindFrontCloest();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ladder")
        {
            inLadder = true;
            Ladder = collision.GetComponentsInChildren<Transform>()[1];
        }
        if (collision.CompareTag("Enemies"))
        {
            Transform enemy = collision.transform;
            if(!nearEnemies.Contains(enemy))
            {
                nearEnemies.Add(enemy);
                enemy.GetComponent<Oppssum>().onAttackEffect.AddListener(OnGetAttack);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            inLadder = false;
        }
        if (collision.CompareTag("Enemies"))
        {
            if (nearEnemies.Contains(collision.transform))
            {
                nearEnemies.Remove(collision.transform);
                collision.transform.GetComponent<Oppssum>().onAttackEffect.RemoveListener(OnGetAttack);
            }
        }
    }
    void OnGetAttack(Oppssum oppssum)
    {
        if (m_stateController.CurrentState() == "Defense")
        {
            DefenseAttack(oppssum);
        }
        else
        {
            NorGetAttack(oppssum);
        }
    }
    void NorGetAttack(Oppssum oppssum)
    {
        Debug.Log("主角 受伤事件生效");
        //击退
        transform.Translate(-2.0f * oppssum.transform.localScale.x, 0, 0);
        //进入受伤状态
        m_stateController.ChangeState("Hurt");
        //血量减去主角攻击力
        Red -= oppssum.cutforce;
    }
    void DefenseAttack(Oppssum oppssum)
    {
        Debug.Log("主角 防御事件生效");
        //平衡值增加
        balance++;
        //平衡条超过最大平衡值
        if (balance > Maxbalance)
        {
            //进入大硬直
            //m_oppssum.stiffmulyiple = 2;
            m_stateController.ChangeState("Hurt");
            //平衡条清0
            balance = 0;
        }
    }

private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackR);
    }
    private void FindFrontCloest()
    {
        if (nearEnemies.Count > 0)
        {
            Transform temp = nearEnemies[0];
            for(int i =0; i<nearEnemies.Count;++i)
            {
                if ((nearEnemies[i].position.x - transform.position.x) * transform.localScale.x > 0 &&
                    Vector2.Distance(transform.position, nearEnemies[i].position) < Vector2.Distance(transform.position, temp.position))
                {
                    temp = nearEnemies[i];
                }
            }
            enemy = temp;

        }
        else
        {
            enemy = null;
        }
    }
}
