using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateController : MonoBehaviour
{
    //私有状态代理
    protected IState m_state;
    //状态组
    public IStateGroup stateGroup;
    // Start is called before the first frame update
    private void Awake()
    {
        //调用状态组初始注册状态
        stateGroup.InitDictionary(this);
    }
    void Start()
    {
        //设置初始状态
        if(stateGroup.firststate!=null)
        {
            m_state = stateGroup.firststate;
        }
        m_state.enter();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void FixedUpdate()
    {
        // 代理更新
        m_state.update();
    }
    //改变状态
    public void ChangeState(string type)
    {
        //调用前一个状态的退出函数
        m_state.exit();
        //状态改变
        m_state = stateGroup.states[type];
        //调用新状态进入函数
        m_state.enter();
    }

    public void Return()
    {
        //返回站立状态
        ChangeState("Idle");
    }
    public void ReturnDown()
    {
        //返回下落状态
        ChangeState("Down");
    }
    public void ReturnUp()
    {
        ChangeState("Up");
    }

    public GameObject LoadPrefabs(string path)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load(path));
        return prefab;
    }
    public string CurrentState()
    {
        return m_state.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_state.onCollisionEnter2D(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_state.onTriggerEnter2D(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        m_state.onTriggerStay2D(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_state.onTriggerExit2D(collision);
    }
    public void StateEvent()
    {
        m_state.OnEvent();
    }
}
