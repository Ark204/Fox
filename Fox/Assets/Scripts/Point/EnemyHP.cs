using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    private Oppssum enemy;
    //public Image Enemyhealthpoint;
    private Image Enemyhealthpoint;
    private void Awake()
    {
        Enemyhealthpoint=GetComponentInChildren<Image>();
        //enemy = GameObject.FindGameObjectWithTag("Enemies").GetComponent<Oppssum>();
        enemy=GetComponentInParent<Oppssum>();
    }


    void Update()
    {
        Enemyhealthpoint.fillAmount = (float)enemy.HP / (float)enemy.MaxHP;
    }
}
