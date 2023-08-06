using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name} !");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name} !");
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Local <-> World <-> Viewport <-> Screen

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.green);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 1000.0f))
        {
            Debug.Log($"Raycast @ {raycastHit.collider.gameObject.name}");
        }
    }
}
