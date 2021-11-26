using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("", true)]
public class TempMove : MonoBehaviour
{
    private Transform mytransform;
    private Rigidbody2D myrigidbody2D;
    private Animator myanimator;

    public Collider2D mycollider2D;
    public LayerMask ground;
    public float speed=10;
    public float jumpforce;

    // Start is called before the first frame update
    void Start()
    {
        mytransform = this.GetComponent<Transform>();
        myrigidbody2D = this.GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        float hormove = Input.GetAxis("Horizontal");
        float facedir = Input.GetAxisRaw("Horizontal");
        //½ÇÉ«ÒÆ¶¯
        if(hormove != 0)
        {
            myrigidbody2D.velocity = new Vector2(hormove * speed*Time.deltaTime, myrigidbody2D.velocity.y);
            myanimator.SetFloat("running", Mathf.Abs(facedir));
        }
        if(facedir !=0)
        {
            mytransform.localScale = new Vector3(facedir, 1, 1);
        }
        //½ÇÉ«ÌøÔ¾
        if(Input.GetButtonDown("Jump"))
        {
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, Time.deltaTime*jumpforce);
            //myanimator.SetBool("jumping", true);
        }
    }
    void SwitchAnim()
    {
        //myanimator.SetBool("idle", false);
        if (myanimator.GetBool("jumping"))
        {
            if(myrigidbody2D.velocity.y<0)
            {
                //myanimator.SetBool("jumping", true);
                //.SetBool("falling", true);
            }
            else if(mycollider2D.IsTouchingLayers(ground))
            {
                //myanimator.SetBool("jumping", false);
                //myanimator.SetBool("falling", false);
            }
        }
    }
}
