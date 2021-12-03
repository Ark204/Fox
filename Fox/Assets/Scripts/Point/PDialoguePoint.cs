using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDialoguePoint : MonoBehaviour
{
    public Transform Fox;
    void Start()
    {
        //Fox=transform.GetComponent<Transform>();
        Fox = transform.Find("/PlayerDialogue");
        void Update()
        {
            Vector2 Pos = transform.position;
            //Pos = Fox.transform.position.x;
        }
    }
}