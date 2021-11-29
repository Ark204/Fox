using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tip : MonoBehaviour
{
    public GameObject tip1;
    public GameObject tip2;
    public GameObject tip3;
    public GameObject tip4;
    public GameObject TipBox1;
    public GameObject TipBox2;
    public GameObject TipBox3;
    public GameObject TipBox4;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == TipBox1)
        {
            tip1.SetActive(true);
        }
        if (collision.gameObject == TipBox2)
        {
            tip2.SetActive(true);
        }
        if (collision.gameObject == TipBox3)
        {
            tip3.SetActive(true);
        }
        if (collision.gameObject == TipBox4)
        {
            tip4.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == TipBox1)
        {
            tip1.SetActive(false);
        }
        if (collision.gameObject == TipBox2)
        {
            tip2.SetActive(false);
        }
        if (collision.gameObject == TipBox3)
        {
            tip3.SetActive(false);
        }
        if (collision.gameObject == TipBox4)
        {
            tip4.SetActive(false);
        }
    }
}
