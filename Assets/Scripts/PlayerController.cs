using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float rotSpeed = 500.0f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingRate = 0.2f;

    private Rigidbody rb;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set drag and angular drag for the Rigidbody
        rb.drag = 50;
        rb.angularDrag = 50;

        // Subscribe to the InputManager's events
        Managers.Input.OnSpaceDown += Jump;
        Managers.Input.OnLeftShiftDown += Descend;
        Managers.Input.OnLeftMouseDown += StartShooting;
        Managers.Input.OnLeftMouseUp += StopShooting;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        Managers.Input.OnSpaceDown -= Jump;
        Managers.Input.OnLeftShiftDown -= Descend;
        Managers.Input.OnLeftMouseDown -= StartShooting;
        Managers.Input.OnLeftMouseUp -= StopShooting;
    }

    private void Update()
    {
        HandleMovementAndRotation();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    private void Descend()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 3);
    }

    private void HandleMovementAndRotation()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        // Move
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        // Rotate towards moving direction
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        }
    }

    private void StartShooting()
    {
        ShootBullet();
        if (shootingCoroutine == null) shootingCoroutine = StartCoroutine(ShootBulletsRepeatedly());
    }

    private void StopShooting()
    {
        isShooting = false;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator ShootBulletsRepeatedly()
    {
        yield return new WaitForSeconds(shootingRate);
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootingRate);
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 1000);
        bulletRb.useGravity = false;
        Destroy(bullet, 5);
    }
}
