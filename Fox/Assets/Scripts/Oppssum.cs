using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Oppssum : MonoBehaviour
{
    //public member
    public float speed = 4; //�����ƶ��ٶ�
    //chase
    public float chasespeed = 6;  //׷���ٶ�
    //idle
    public float idleTime = 2f;  //վ��ʱ��
    //Bleed
    public int MaxHP ;  //���Ѫ��
    public int HP ;  //��ǰѪ��
    public int Maxbalance ;  //���ƽ��ֵ
    public int balance ;  //��ǰƽ��ֵ
    public float balanceRecoverSpeed; //ƽ��ֵ�ָ��ٶ�
    public float recoverBalance=0;      //�ۻ��ָ�ƽ��ֵ
    //cut
    public int cutforce = 1;  //������
    public int cutCount = 2;  //��������
    public float cutTime = 0.5f;  //����ʱ��
    public float attackR = 0.5f;  //�����뾶
    //stiff
    public float stiffTime = 0.5f; //Ӳֱʱ��
    public short stiffmulyiple = 1; //Ӳֱʱ�䱶��
    //defense
    public bool checkAttack = false;
    //be executed
    public bool norExecute = false;
    public bool backExecute = false;

    public bool listening = false;
    //Component
    public Transform execute;
    public Transform[] patrolPoint;
    public Transform[] chasePoint;
    public Transform target;
    public LayerMask targetLayer;//Ŀ���ͼ��
    public Transform attackPoint;//������ΧԲ��
    public StateController m_stateController;
    // Start is called before the first frame update
    void Start()
    {
        m_stateController = GetComponent<StateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //���������Ѳ��״̬ && ����λ����� && ����С��3
            if ((m_stateController.CurrentState() == "WarIdle" || m_stateController.CurrentState() == "WarRun")&&
                transform.localScale.x == -target.localScale.x &&
                0 < (target.position.x - transform.position.x) * transform.localScale.x &&
                (target.position.x - transform.position.x) * transform.localScale.x < 5)
            {
                //���Ա�������
                backExecute = true;
            }
            else
            {
                backExecute = false;
            }
        }
        //���ƽ��ֵ����
        if (balance == Maxbalance)
        {
            //�������洦��
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
    /// <summary>
    /// ����Ұ��Χ�ڵ����Ƿ����������ô˺���
    /// </summary>
    void OnPlayerCutStart(Fox fox)
    {
        //��������ǵĹ�����Χ��
        if (Mathf.Abs(fox.attackPoint.position.x - transform.position.x) <= fox.attackR *3)
        {
            checkAttack = true;//���з���
        }
    }
    /// <summary>
    /// ����Ұ��Χ�ڵ����ǹ����������ô˺���
    /// </summary>
    void OnPlayerCutEnd()
    {
        checkAttack = false;
    }
    /// <summary>
    /// �����������ڲ�����ί��
    /// </summary>
    /// <param name="fox"></param>
    void OnGetAttack(Fox fox)
    {
        WarriorState state = (WarriorState)m_stateController.M_state;
        if (state == null)
            Debug.Log("null state");
        state.m_GetAttackFun(fox);
    }
}
