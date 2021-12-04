using UnityEngine;

public class Cut : FoxState
{
    public Cut(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    private bool attackEffect;
    public override void enter()
    {
        base.enter();
        m_fox.onCutStart?.Invoke(m_fox);
        //ɲ��
        m_rigidbody2D.velocity = new Vector2(0, 0);
        //��ʼ�������Ƿ��Ѿ���Ч
        attackEffect = false;
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
        //����һ�빥����Ч
        if(!attackEffect&&
            m_fox.cutEffect < info.normalizedTime &&
            Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //�����Ѿ���Ч
            attackEffect = true;
            //���õ����ܵ���Ч��������
            m_fox.onAttackEffect?.Invoke(m_fox);
        }
        //������Ч����Է������ν���
        if(0.54f < info.normalizedTime)
        {
            //�������߶��ο�
            Cut();
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
    public override void exit()
    {
        m_fox.onCutEnd?.Invoke();
    }
    
}
