using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("",true)]
public class Bullet : MonoBehaviour
{
    private Collider2D m_collider2D;
    public float m_existTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        m_collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_collider2
    }
    void FixedUpdate()
    {
        m_existTime = m_existTime - Time.fixedDeltaTime;
        if(m_existTime<=0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemies")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("dying", true);
        }
        Destroy(gameObject);
    }
    //void OnTrigerEnter2D(Collision2D collision)
    //{
    //    Destroy(gameObject);
    //}

    
}
