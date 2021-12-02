using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private Fox Fox;
    public GameObject Door;
    public GameObject NoKey;
    private bool isKey = false;
    private void Awake()
    {
        Fox = GameObject.FindGameObjectWithTag("Fox").GetComponent<Fox>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Medicine" && Fox.Red < Fox.MaxRed)
        {
            Fox.Red += 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Door")
        {

            if (isKey == true)
            {
                Destroy(other.gameObject);
                Door.SetActive(true);
            }
            else
            {
                NoKey.SetActive(true);
            }
        }
        if (other.tag == "Key")
        {
            isKey = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Door")
        {
            NoKey.SetActive(false);
        }
    }
}