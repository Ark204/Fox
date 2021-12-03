using UnityEngine;

public class Execute : OnGround
{
    public Execute(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        m_animator.Play("skill_1");
    }
    public override void update()
    {
        Vector3 enemyback = new Vector3(2*m_transform.localScale.x, 0, 0);  
        if (m_fox.enemy)
        {
            //移动
            enemyback += m_fox.enemy.position;
            m_transform.position = Vector2.MoveTowards(m_transform.position,
                enemyback,
                m_fox.executeSpeed * Time.fixedDeltaTime); 
            if (Vector2.Distance(m_transform.position, enemyback) <= 0.1)
            {
                m_stateController.ChangeState("Stiff");
            }
        }
    }
    public override void exit() 
    {
        //处决敌人
        m_fox.enemy.GetComponent<Oppssum>().HP = 0;
        m_fox.enemy.GetComponent<StateController>().ChangeState("WarDeath");
    }
}
