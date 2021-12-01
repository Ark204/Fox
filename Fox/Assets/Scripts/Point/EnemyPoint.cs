using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    //public Transform Enemy;
    public Transform Point;
    
    void Start()
    {

        GameObject fu = this.transform.parent.gameObject;
    }
    private void Awake() {
        //var Father = gameObject.transform.parent;
        //GameObject fu = this.transform.parent.gameObject;
    }
    void Update()
    {
        GameObject fu = this.transform.parent.gameObject;
        Vector2 Por=Point.transform.position;
        Por.x=fu.transform.position.x;
        Por.y=fu.transform.position.y+1;
        Point.transform.position=Por;
    }
}
