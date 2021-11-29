using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class CanavsController : MonoBehaviour
{
    public GameObject tip1;
    public GameObject tip2;
    public GameObject tip3;
    public GameObject tip4;
    public GameObject PauseMenu;
    public void PauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale=0f;
    }
    public void ResumeGame(){
        PauseMenu.SetActive(false);
        Time.timeScale=1f;
    }  
    public void ReturnMenu(){
        EditorSceneManager.LoadScene("Menu");
        Time.timeScale=1f;
        tip1.SetActive(false);
        tip2.SetActive(false);
        tip3.SetActive(false);
        tip4.SetActive(false);
    }
}
