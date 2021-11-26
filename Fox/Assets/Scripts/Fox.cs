using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    //public member
    public float speed = 10; //�����ƶ��ٶ�
    //jump
    public float jumpforce =10;  //��Ծ��
    public int jumpCount = 2;  //��Ծ����
    //sprint  
    public float sprintspeed = 30;  //����ٶ�
    public float sprintTime = 0.1f;  //���ʱ��
    //Bleed
    public int MaxRed = 5;  //���Ѫ��
    public int Red = 5;  //��ǰѪ��
    public int Maxbalance = 3;  //���ƽ��ֵ
    public int balance = 0;  //��ǰƽ��ֵ
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
    public bool executeable = false; 
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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //�����ȡ
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        //if (Input.GetButtonDown("Shut"))
        //{
        //    shutPressed = true;
        //}
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
        if(Input.GetButtonDown("Fire3"))
        {
            sprintPressed = true;
        }
        if (enemy)
        {
            executeable= enemy.GetComponent<Oppssum>().execute.gameObject.activeSelf ;
        }
        else
        {
            executeable = false;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            cutPressed = true;
        }
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
            enemy = collision.transform;
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
            enemy = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackR);
    }
}
