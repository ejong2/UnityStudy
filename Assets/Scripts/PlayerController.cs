using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float rotSpeed = 500.0f;

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraDistance = 10.0f;
    [SerializeField] private float cameraHeight = 5.0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 50;
        rb.angularDrag = 50;
    }

    private void Update()
    {
        HandleMovementAndRotation();

        Vector3 cameraPosition = transform.position - Vector3.forward * cameraDistance;
        cameraPosition.y += cameraHeight;
        cameraTransform.position = cameraPosition;
    }

    private void HandleMovementAndRotation()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        }
    }
}
