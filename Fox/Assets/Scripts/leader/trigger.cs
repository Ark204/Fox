using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private Fox Fox;
    public GameObject Door;
    public GameObject NoKey;
<<<<<<< Updated upstream
=======
    public AudioSource LockDoor;
    public AudioSource OpedDoor;
    public AudioSource PlayMusic;
    public AudioSource BoosMusic;
>>>>>>> Stashed changes
    private void Awake()
    {
        Fox = GameObject.FindGameObjectWithTag("Fox").GetComponent<Fox>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Medicine" && Fox.Red < Fox.MaxRed)
        {
            Fox.Red += 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Door")
        {

            if (Fox.haveKey == true)
            {
                //开门
                other.isTrigger = true;
                other.transform.Rotate(0, -99, 0);
                //消耗钥匙
                Fox.haveKey = false;
<<<<<<< Updated upstream
=======
                OpedDoor.Play();
>>>>>>> Stashed changes
            }
            else
            {
                NoKey.SetActive(true);
            }
        }
        if (other.tag == "Key")
        {
            Destroy(other.gameObject);
            Fox.haveKey= true;
        }
        if(other.tag=="LockDoor")
        {
            //关门
            Door.GetComponent<Collider2D>().isTrigger = false;
            Door.transform.Rotate(0, 99, 0);
            other.enabled = false;
<<<<<<< Updated upstream
=======
            LockDoor.Play();
            PlayMusic.Stop();
            BoosMusic.Play();
>>>>>>> Stashed changes
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Door")
        {
            NoKey.SetActive(false);
        }
    }
}