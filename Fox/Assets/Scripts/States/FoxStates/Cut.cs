using UnityEngine;

public class Cut : FoxState
{
    private AnimatorStateInfo info;
    public Cut(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        m_rigidbody2D.velocity = new Vector2(0, 0);
        //���Ĵ�����һ
        m_fox.cutCount--;
        if(m_fox.cutCount==1)
        {
            m_animator.Play("attack_1");
        }
        if(m_fox.cutCount==0)
        {
            m_animator.Play("attack_2");
        }
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        //�������߶��ο�
        Cut();
        //����һ�빥����Ч
        if(0.5f < info.normalizedTime &&
            info.normalizedTime < 0.53&&
            Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //���õ����ܵ���Ч��������
            if(m_fox.enemy!=null)
            {
                m_fox.enemy.GetComponent<StateController>().StateEvent();
            }
        }
        //������
        if (info.normalizedTime >= 0.95f)
        {
            //����ǵڶ���ն����
            if (m_fox.cutCount == 0)
            {
                //����Ӳֱ
                m_stateController.ChangeState("Stiff");
            }
            //����idle״̬
            else
                m_stateController.ChangeState("Idle");
        }
    }
    public override void exit(){ }
}
