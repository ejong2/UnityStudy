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

    // Unity�� Vector3�� ��ȯ
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    // Unity�� Vector3���� MyVector�� ��ȯ
    public static MyVector FromVector3(Vector3 vec)
    {
        return new MyVector(vec.x, vec.y, vec.z);
    }
}

// �÷��̾��� ������ �� ��ȣ�ۿ��� �����մϴ�.
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movSpeed = 15.0f;  // �÷��̾� �̵� �ӵ�.
    private float _rotSpeed = 5000.0f;  // �÷��̾� ȸ�� �ӵ�.

    public GameObject obj;  // �Ѿ� ������Ʈ ����.
    private bool isShooting = false;
    private float shootingRate = 0.2f;  // 1�ʿ� 5�� �߻縦 ���� �� (1/5 = 0.2��)

    void Start()
    {
        // ���� ���� �ʱ�ȭ
        MyVector ������ = new MyVector(0.0f, 0.0f, 0.0f);
        MyVector ������ = new MyVector(10.0f, _rotSpeed, 0.0f);
        MyVector ���� = ������ - ������;
    }

    void Update()
    {
        // ī�޶� ��ġ �� ȸ�� ����
        Camera.main.transform.position = transform.position - transform.forward * 3 + Vector3.up * 3;
        Camera.main.transform.rotation = Quaternion.Euler(25, 0, 0);

        // ������ �Է� ó��
        HandleMovementInput();
        // ȸ�� �Է� ó��
        HandleRotationInput();
        // ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        // shift Ű�� ���� �� �Ʒ��� ������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3);
        }
        // ���콺 ���� ��ư���� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
        // ���콺 ���� ��ư�� �� ������ ���� ���
        if (Input.GetMouseButton(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootBulletsRepeatedly());
        }
        // ���콺 ���� ��ư�� �� ���
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            StopAllCoroutines();  // ���⿡�� ��� �ڷ�ƾ�� ���ߵ��� ����
        }
    }

    // �÷��̾� ������ ó��
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

    // �÷��̾� ȸ�� ó��
    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up * Time.deltaTime * -_rotSpeed);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * Time.deltaTime * _rotSpeed);
    }

    IEnumerator ShootBulletsRepeatedly()
    {
        yield return new WaitForSeconds(shootingRate); // ó�� �߻� �Ŀ� ���
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootingRate);
        }
    }

    // �÷��̾� �Ѿ� �߻�
    void ShootBullet()
    {
        GameObject bullet = Instantiate(obj, transform.position + transform.forward, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        Destroy(bullet, 5);
    }
}