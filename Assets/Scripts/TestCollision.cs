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
        Vector3 look = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position + Vector3.up , Vector3.forward * 10, Color.red);

        RaycastHit[] ColliderHits;
        ColliderHits = Physics.RaycastAll(transform.position + Vector3.up , Vector3.forward, 10);

        foreach(RaycastHit hit in ColliderHits)
        {
            Debug.Log($"Raycast hit! {hit.collider.gameObject.name}!");
        }
    }
}
