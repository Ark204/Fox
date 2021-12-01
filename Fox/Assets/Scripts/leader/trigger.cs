using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private Fox Fox;
    private void Awake()
    {
        Fox = GameObject.FindGameObjectWithTag("Fox").GetComponent<Fox>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Medicine" && Fox.Red < Fox.MaxRed)
        {
            Fox.Red += 1;
            Destroy(other.gameObject);
        }
    }
}
