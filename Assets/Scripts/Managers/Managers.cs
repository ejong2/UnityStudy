using UnityEngine;

public class Managers : MonoBehaviour
{
    // �̱��� ������ ���� �ν��Ͻ� ����
    static Managers s_instance;
    // �̱��� ������ �ν��Ͻ� ������
    static Managers Instance { get { Init(); return s_instance; } }

    // �Է� ������ �ν��Ͻ� ����
    InputManager _input = new InputManager();
    // �Է� ������ ������
    public static InputManager Input { get { return Instance._input; } }

    // ���� ���� �� ���� �� ���� ����Ǵ� �޼���
    void Start()
    {
        Init();
    }

    // �����Ӹ��� ����Ǵ� �޼���
    void Update()
    {
        _input.OnUpdate();
    }

    // �̱��� �ν��Ͻ� �ʱ�ȭ �޼���
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            // �� ��ȯ �� �ı����� �ʰ� ����
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}