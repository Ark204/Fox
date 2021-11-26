using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public interface IState 
{
    void update();

    void enter();

    void exit();

    void onCollisionEnter2D(Collision2D collision);

    void onTriggerEnter2D(Collider2D collision);

    void onTriggerStay2D(Collider2D collision);

    void onTriggerExit2D(Collider2D collision);

    void OnEvent(); 
}
