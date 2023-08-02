using System.Collections;
using UnityEngine;

// Rigidbody ������Ʈ�� �ʿ����� ��Ÿ���� ��Ʈ����Ʈ
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // ����Ƽ �������� �ν����� �гο��� ���� ������ �� �ֵ��� �ϴ� ��Ʈ����Ʈ
    // �����Ӱ� ȸ���� ���õ� ���� ����
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float rotSpeed = 500.0f;

    // �Ѿ� �߻� ���� ����
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingRate = 0.2f;

    private Rigidbody rb;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    // ������Ʈ�� Ȱ��ȭ�Ǹ� ����Ǵ� �޼���
    private void Awake()
    {
        // �ش� ���� ������Ʈ���� Rigidbody ������Ʈ�� ������
        rb = GetComponent<Rigidbody>();

        // Rigidbody�� �Ӽ� ����
        rb.drag = 50;
        rb.angularDrag = 50;

        // �Է� �̺�Ʈ�� �޼��� ����
        Managers.Input.OnSpaceDown += Jump;
        Managers.Input.OnLeftShiftDown += Descend;
        Managers.Input.OnLeftMouseDown += StartShooting;
        Managers.Input.OnLeftMouseUp += StopShooting;
    }

    // ������Ʈ�� �ı��� �� ����Ǵ� �޼���
    private void OnDestroy()
    {
        // �޸� ������ �����ϱ� ���� �̺�Ʈ ���� ���
        Managers.Input.OnSpaceDown -= Jump;
        Managers.Input.OnLeftShiftDown -= Descend;
        Managers.Input.OnLeftMouseDown -= StartShooting;
        Managers.Input.OnLeftMouseUp -= StopShooting;
    }

    // �����Ӹ��� ����Ǵ� �޼���
    private void Update()
    {
        HandleMovementAndRotation();
    }

    // ���� �޼���
    private void Jump()
    {
        // Rigidbody�� ����Ͽ� ������ ȿ���� ����
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // �������� �޼���
    private void Descend()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 3);
    }

    // �����Ӱ� ȸ�� ó�� �޼���
    private void HandleMovementAndRotation()
    {
        // GetAxisRaw �޼��带 ����Ͽ� �Է��� �� �����ϰ� ����
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        
        // �÷��̾� �̵�
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        // �÷��̾ �̵� ������ �ٶ󺸰� ȸ��
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        }
    }

    // �Ѿ� �߻� ���� �޼���
    private void StartShooting()
    {
        ShootBullet();
        if (shootingCoroutine == null) shootingCoroutine = StartCoroutine(ShootBulletsRepeatedly());
    }

    // �Ѿ� �߻� ���� �޼���
    private void StopShooting()
    {
        isShooting = false;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    // �ڷ�ƾ�� ����Ͽ� ���� �������� �Ѿ� �߻�
    private IEnumerator ShootBulletsRepeatedly()
    {
        yield return new WaitForSeconds(shootingRate);
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootingRate);
        }
    }

    // �Ѿ� �߻� �޼���
    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 1000);
        bulletRb.useGravity = false;
        Destroy(bullet, 5);
    }
}