using UnityEngine;

public class BackExecute : OnGround
{
    public BackExecute(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    public override void enter()
    {
        //���ű��̶���
        Debug.Log("����");
        m_animator.Play("skill_1");
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 0.95f)
        {
            //����Idle״̬
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        //��������
        m_fox.enemy.GetComponent<Oppssum>().HP = 0;
        m_fox.enemy.GetComponent<StateController>().ChangeState("WarDeath");
    }
}
