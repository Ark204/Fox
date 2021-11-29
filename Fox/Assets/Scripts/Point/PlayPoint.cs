using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPoint : MonoBehaviour
{
    public Transform playPoint;
    void Update()
    {
        Vector2 pos=playPoint.transform.position;
        pos.x=playPoint.transform.position.x;
        pos.y=playPoint.transform.position.y+2;
        transform.position=playPoint.transform.position;
    }
}
