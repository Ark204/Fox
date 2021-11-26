using UnityEngine;

public class Cut : FoxState
{
    private float cutTime;
    public Cut(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //��ʼ��״̬����ʱ��
        cutTime = m_fox.cutTime;
        //���Ĵ�����һ
        m_fox.cutCount--;
        if(m_fox.cutCount==1)
        {
            Debug.Log("���ǣ����ŵ�һ������");
        }
        if(m_fox.cutCount==0)
        {
            Debug.Log("���ǣ����ŵڶ�������");
        }
        //����������Χ
        m_stateController.GetComponentInChildren<CircleCollider2D>().enabled = true;
    }
    public override void update()
    {
        cutTime -= Time.fixedDeltaTime;
        //���ο�
        if(m_fox.cutPressed&&m_fox.cutCount>0)
        {
            m_fox.cutPressed = false;
            //�������״̬
            m_stateController.ChangeState("Cut");
        }
        //����һ�빥����Ч
        if(m_fox.cutTime/2-Time.fixedDeltaTime < cutTime && cutTime < m_fox.cutTime/2
            && Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //���õ����ܵ���Ч��������
            if(m_fox.enemy!=null)
            {
                m_fox.enemy.GetComponent<StateController>().StateEvent();
            }
        }
        //������
        if(cutTime<0)
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
        m_stateController.GetComponentInChildren<CircleCollider2D>().enabled = false;
    }
    //�������Ƿ��ڷ�Χ��
    public override void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            m_fox.enemy = collision.transform;
        }
    }
}
