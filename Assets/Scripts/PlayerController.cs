using System.Collections;
using UnityEngine;

// Rigidbody 컴포넌트가 필요함을 나타내는 어트리뷰트
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // 유니티 에디터의 인스펙터 패널에서 값을 수정할 수 있도록 하는 어트리뷰트
    // 움직임과 회전에 관련된 변수 설정
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float rotSpeed = 500.0f;

    // 총알 발사 관련 설정
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingRate = 0.2f;

    private Rigidbody rb;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    // 오브젝트가 활성화되면 실행되는 메서드
    private void Awake()
    {
        // 해당 게임 오브젝트에서 Rigidbody 컴포넌트를 가져옴
        rb = GetComponent<Rigidbody>();

        // Rigidbody의 속성 조정
        rb.drag = 50;
        rb.angularDrag = 50;

        // 입력 이벤트에 메서드 구독
        Managers.Input.OnSpaceDown += Jump;
        Managers.Input.OnLeftShiftDown += Descend;
        Managers.Input.OnLeftMouseDown += StartShooting;
        Managers.Input.OnLeftMouseUp += StopShooting;
    }

    // 오브젝트가 파괴될 때 실행되는 메서드
    private void OnDestroy()
    {
        // 메모리 누수를 방지하기 위해 이벤트 구독 취소
        Managers.Input.OnSpaceDown -= Jump;
        Managers.Input.OnLeftShiftDown -= Descend;
        Managers.Input.OnLeftMouseDown -= StartShooting;
        Managers.Input.OnLeftMouseUp -= StopShooting;
    }

    // 프레임마다 실행되는 메서드
    private void Update()
    {
        HandleMovementAndRotation();
    }

    // 점프 메서드
    private void Jump()
    {
        // Rigidbody를 사용하여 물리적 효과를 적용
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // 내려가기 메서드
    private void Descend()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 3);
    }

    // 움직임과 회전 처리 메서드
    private void HandleMovementAndRotation()
    {
        // GetAxisRaw 메서드를 사용하여 입력을 더 정밀하게 제어
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        
        // 플레이어 이동
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        // 플레이어가 이동 방향을 바라보게 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        }
    }

    // 총알 발사 시작 메서드
    private void StartShooting()
    {
        ShootBullet();
        if (shootingCoroutine == null) shootingCoroutine = StartCoroutine(ShootBulletsRepeatedly());
    }

    // 총알 발사 중지 메서드
    private void StopShooting()
    {
        isShooting = false;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    // 코루틴을 사용하여 일정 간격으로 총알 발사
    private IEnumerator ShootBulletsRepeatedly()
    {
        yield return new WaitForSeconds(shootingRate);
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootingRate);
        }
    }

    // 총알 발사 메서드
    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 1000);
        bulletRb.useGravity = false;
        Destroy(bullet, 5);
    }
}