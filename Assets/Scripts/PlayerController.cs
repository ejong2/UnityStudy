using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }
    }

    public MyVector normalized
    {
        get
        {
            return new MyVector(x / magnitude, y / magnitude, z / magnitude);
        }
    }

    public MyVector(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    // Unity의 Vector3로 변환
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    // Unity의 Vector3에서 MyVector로 변환
    public static MyVector FromVector3(Vector3 vec)
    {
        return new MyVector(vec.x, vec.y, vec.z);
    }
}

// 플레이어의 움직임 및 상호작용을 제어합니다.
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movSpeed = 15.0f;  // 플레이어 이동 속도.
    private float _rotSpeed = 5000.0f;  // 플레이어 회전 속도.

    public GameObject obj;  // 총알 오브젝트 참조.
    private bool isShooting = false;
    private float shootingRate = 0.2f;  // 1초에 5번 발사를 위한 빈도 (1/5 = 0.2초)

    void Start()
    {
        // 방향 벡터 초기화
        MyVector 시작점 = new MyVector(0.0f, 0.0f, 0.0f);
        MyVector 종료점 = new MyVector(10.0f, _rotSpeed, 0.0f);
        MyVector 방향 = 종료점 - 시작점;
    }

    void Update()
    {
        // 카메라 위치 및 회전 설정
        Camera.main.transform.position = transform.position - transform.forward * 3 + Vector3.up * 3;
        Camera.main.transform.rotation = Quaternion.Euler(25, 0, 0);

        // 움직임 입력 처리
        HandleMovementInput();
        // 회전 입력 처리
        HandleRotationInput();
        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        // shift 키를 누를 때 아래로 움직임
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3);
        }
        // 마우스 왼쪽 버튼으로 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
        // 마우스 왼쪽 버튼을 꾹 누르고 있을 경우
        if (Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootBulletsRepeatedly());
        }
        // 마우스 왼쪽 버튼을 뗄 경우
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            StopAllCoroutines();  // 여기에서 모든 코루틴을 멈추도록 수정
        }
    }

    // 플레이어 움직임 처리
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _movSpeed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _movSpeed);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _movSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _movSpeed);
    }

    // 플레이어 회전 처리
    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up * Time.deltaTime * -_rotSpeed);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * Time.deltaTime * _rotSpeed);
    }

    IEnumerator ShootBulletsRepeatedly()
    {
        yield return new WaitForSeconds(shootingRate); // 처음 발사 후에 대기
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootingRate);
        }
    }

    // 플레이어 총알 발사
    void ShootBullet()
    {
        GameObject bullet = Instantiate(obj, transform.position + transform.forward, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        Destroy(bullet, 5);
    }
}