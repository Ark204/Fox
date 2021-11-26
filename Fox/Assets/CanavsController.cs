using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavsController : MonoBehaviour
{
    public GameObject PauseMenu;
    public void PauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale=0f;
    }
    public void ResumeGame(){
        PauseMenu.SetActive(false);
        Time.timeScale=1f;
    }
}
