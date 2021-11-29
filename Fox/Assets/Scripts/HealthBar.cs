using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //生命值
    public Image healthPointImage;//主血条
    public Image healthPointEffect;//缓降血条
    public Image Enemyhealthpoint;//敌人血条
    private Fox Fox;//获得主角
    private Oppssum Oppssum;
    //耐力条
    public Image endurancePointImage;//耐力条
    public Image EnemyEndurance;
    private void Awake()
    {
         Fox=GameObject.FindGameObjectWithTag("Fox").GetComponent<Fox>();
         Oppssum=GameObject.FindGameObjectWithTag("Enemies").GetComponent<Oppssum>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthPointImage.fillAmount = (float)Fox.Red / (float)Fox.MaxRed;
        if (healthPointEffect.fillAmount > healthPointImage.fillAmount)
        {
            healthPointEffect.fillAmount -= 0.003f;
        }
        else { healthPointEffect.fillAmount = healthPointImage.fillAmount; }
       endurancePointImage.fillAmount= (float)Fox.balance/ (float)Fox.Maxbalance;
       Enemyhealthpoint.fillAmount= Oppssum.HP/Oppssum.MaxHP;
       EnemyEndurance.fillAmount=1;
    }
    
}
