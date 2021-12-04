using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateController : MonoBehaviour
{
    //˽��״̬����
    protected IState m_state;
    //״̬��
    public IStateGroup stateGroup;
    // Start is called before the first frame update
    private void Awake()
    {
        //����״̬���ʼע��״̬
        stateGroup.InitDictionary(this);
    }
    void Start()
    {
        //���ó�ʼ״̬
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
        // �������
        m_state.update();
    }
    //�ı�״̬
    public void ChangeState(string type)
    {
        //����ǰһ��״̬���˳�����
        m_state.exit();
        //״̬�ı�
        m_state = stateGroup.states[type];
        //������״̬���뺯��
        m_state.enter();
    }

    public void Return()
    {
        //����վ��״̬
        ChangeState("Idle");
    }
    public void ReturnDown()
    {
        //��������״̬
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
}
