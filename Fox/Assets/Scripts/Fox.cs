using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerEvent : UnityEvent<Fox> { }
public class Fox : MonoBehaviour
{
    //public member
    public float speed = 10; //�����ƶ��ٶ�
    //jump
    public float jumpforce =20;  //��Ծ��
    public int jumpCount = 2;  //��Ծ����
    //sprint  
    public float sprintspeed = 60;  //����ٶ�
    public float sprintTime = 0.1f;  //���ʱ��
    public float sprintCd;     //�����ȴʱ��
    public float sprintCurrCd =0;  //��ǰʣ����ȴʱ��
    //Bleed
    public int MaxRed = 5;  //���Ѫ��
    public int Red = 5;  //��ǰѪ��
    public int Maxbalance = 3;  //���ƽ��ֵ
    public int balance = 0;  //��ǰƽ��ֵ
    public float balanceRecoverSpeed; //ƽ��ֵ�ָ��ٶ�
    //medicine
    public int numMedicine = 0;//ҩƷ����
    //cut
    public int cutforce = 1;  //������
    public int cutCount = 2;  //��������
    public float cutTime = 0.5f;  //����ʱ��
    public float attackR=0.5f;  //�����뾶
    //execute
    public float executeSpeed = 200f;//�����ƶ��ٶ�
    //stiff
    public float stiffTime = 0.5f; //Ӳֱʱ��
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
    
    public Transform attackPoint;
    public Transform silkStart;
    public Transform CellingCheck;
    public Transform Ladder;
    public List<Transform> nearEnemies;
    public Transform enemy; //����
    public LayerMask enemies; //����ͼ��
    public LayerMask layerMask;
    public Vector3 shutPoint;
    //events
    public PlayerEvent onCutStart;
    public UnityEvent onCutEnd;
    public PlayerEvent onAttackEffect;
    // Start is called before the first frame update
    void Start()
    {
        onCutStart = new PlayerEvent();
        onCutEnd = new UnityEvent();
        onAttackEffect = new PlayerEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //�����ȡ
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
        //ˢ����ȴʱ��
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
            }
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
