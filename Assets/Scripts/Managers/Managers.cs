using UnityEngine;

public class Managers : MonoBehaviour
{
    // 싱글턴 패턴을 위한 인스턴스 변수
    static Managers s_instance;
    // 싱글턴 패턴의 인스턴스 접근자
    static Managers Instance { get { Init(); return s_instance; } }

    // 입력 관리자 인스턴스 생성
    InputManager _input = new InputManager();
    // 입력 관리자 접근자
    public static InputManager Input { get { return Instance._input; } }

    // 게임 시작 전 최초 한 번만 실행되는 메서드
    void Start()
    {
        Init();
    }

    // 프레임마다 실행되는 메서드
    void Update()
    {
        _input.OnUpdate();
    }

    // 싱글턴 인스턴스 초기화 메서드
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

            // 씬 전환 시 파괴되지 않게 설정
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}