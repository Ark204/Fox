using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHurt : WarriorState
{
    public WarHurt(StateController stateController) : base(stateController) { }
    private float timer=0;
    public override void enter()
    {
        base.enter();
        //����hurt����
        Debug.Log("���ˣ�����");
        //����
        m_transform.Translate(0.5f* m_oppssum.target.localScale.x, 0, 0);
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if(timer>m_oppssum.stiffTime*m_oppssum.stiffmulyiple)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit()
    {
        //Ӳֱʱ�䱶����ʼ��
        m_oppssum.stiffmulyiple = 1;
        //������־ȥ��
        m_oppssum.execute.gameObject.SetActive(false);
        //��ʱ������
        timer =0;
        Debug.Log("���ˣ��ָ�");
    }
}
