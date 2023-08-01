using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // ī�޶� �÷��̾ ����ٴϰ� ����
        Camera.main.transform.position = transform.position - transform.forward * 3 + Vector3.up * 3;
        Camera.main.transform.LookAt(transform.position);

        // w, s, a, d ����Ű ���� transform
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3);
        }

        // q, e ȸ��Ű ���� transform
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -100);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 100);
        }

        // space bar ����Ű ���� transform
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        // shift Ű ���� transform
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3);
        }


    }
}
