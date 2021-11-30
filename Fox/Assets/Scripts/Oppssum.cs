using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppssum : MonoBehaviour
{
    //public member
    public float speed = 4; //基础移动速度
    //chase
    public float chasespeed = 6;  //追击速度
    //idle
    public float idleTime = 2f;  //站立时间
    //Bleed
    public int MaxHP = 5;  //最大血量
    public int HP = 5;  //当前血量
    public int Maxbalance = 1;  //最大平衡值
    public int balance = 0;  //当前平衡值
    //cut
    public int cutforce = 1;  //攻击力
    public int cutCount = 2;  //攻击次数
    public float cutTime = 0.5f;  //攻击时间
    public float attackR = 0.5f;  //攻击半径
    //stiff
    public float stiffTime = 0.5f; //硬直时间
    public short stiffmulyiple = 1; //硬直时间倍数
    public float attackforce = 15;
    //defense
    public bool checkAttack = false;
    //Component
    public Transform execute;
    public Transform[] patrolPoint;
    public Transform[] chasePoint;
    public Transform target;
    public LayerMask targetLayer;//目标的图层
    public Transform attackPoint;//攻击范围圆心
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
        //如果平衡值满了
        if (balance == Maxbalance)
        {
            //显示可以处决
            execute.gameObject.SetActive(true);
        }
        else
        {
            execute.gameObject.SetActive(false); 
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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fox"))
        {
            target = null;
        }
    }
}
