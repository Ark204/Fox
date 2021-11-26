using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppssum : MonoBehaviour
{
    //public member
    public float speed = 10; //�����ƶ��ٶ�
    //chase
    public float chasespeed = 12;  //׷���ٶ�
    //idle
    public float idleTime = 2f;  //վ��ʱ��
    //Bleed
    public int MaxHP = 5;  //���Ѫ��
    public int HP = 5;  //��ǰѪ��
    public int Maxbalance = 1;  //���ƽ��ֵ
    public int balance = 0;  //��ǰƽ��ֵ
    //cut
    public int cutforce = 1;  //������
    public int cutCount = 2;  //��������
    public float cutTime = 0.5f;  //����ʱ��
    public float attackR = 0.5f;  //�����뾶
    //stiff
    public float stiffTime = 0.5f; //Ӳֱʱ��
    public short stiffmulyiple = 1; //Ӳֱʱ�䱶��
    public float attackforce = 15;
    //defense
    public bool checkAttack = false;
    //Component
    public Transform execute;
    public Transform[] patrolPoint;
    public Transform[] chasePoint;
    public Transform target;
    public LayerMask targetLayer;//Ŀ���ͼ��
    public Transform attackPoint;//������ΧԲ��
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(target.GetComponent<StateController>().CurrentState()=="Cut")
            {
                checkAttack = true;
            }
            else
            {
                checkAttack = false;
            }
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackR);
    }
}
