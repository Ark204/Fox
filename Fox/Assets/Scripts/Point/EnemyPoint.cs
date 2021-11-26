using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public Transform Enemy;
    public Transform Point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Por=Point.transform.position;
        Por.x=Enemy.transform.position.x;
        Por.y=Enemy.transform.position.y;
        Point.transform.position=Por;
    }
}
