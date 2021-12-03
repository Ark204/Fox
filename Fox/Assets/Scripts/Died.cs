using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Died : MonoBehaviour
{
    public GameObject DiedMenu;
    void DiedAnim(){
        Time.timeScale=0f;
        DiedMenu.SetActive(true);
    }
}
