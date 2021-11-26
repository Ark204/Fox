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
        m_transform.Translate(-0.5f* m_transform.localScale.x, 0, 0);
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if(timer>m_oppssum.stiffTime*m_oppssum.stiffmulyiple)
        {
            if (m_oppssum.HP <= 0)
            {
                m_stateController.ChangeState("WarDeath");
            }
            else
            {
                m_stateController.ChangeState("WarChase");
            }
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
public class WarDeath : WarriorState
{
    public WarDeath(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        m_animator.Play("death");
    }
    public override void update()
    {
        
    }
    public override void exit()
    {
        
    }
}