using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silk : MonoBehaviour
{
    public float Maxlength = 5;
    public float growspeed = 0.36f;
    public float pullforce = 50f;
    public bool touch = false;
    public Transform m_transform;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        //m_transform为发射点
        m_transform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (m_transform.localScale.x < Maxlength&&!touch)
        {
            m_transform.localScale = new Vector3(m_transform.localScale.x + growspeed, m_transform.localScale.y , m_transform.localScale.z);
        }
        else
        {
            //还原发射点
            m_transform.rotation = new Quaternion();
            m_transform.localScale = new Vector3(1, 1, 1);
            //销毁丝线
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_transform!=null&&collision.gameObject.tag == "Ground")
        {
            //获取命中点
            Vector3 hitPoint = collision.bounds.ClosestPoint(transform.position);
            //获取角色点
            Vector3 playerPoint = m_transform.parent.position;
            //求出方向
            Vector2 direction = new Vector2(hitPoint.x - playerPoint.x, hitPoint.y - playerPoint.y);
            //方向单位化
            direction.Normalize();
            m_transform.parent.GetComponent<StateController>().ChangeState("SilkJump");
            m_transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * pullforce, direction.y * pullforce);
            touch = true;
        }
    }

}
