using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerialzieField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerialzieField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerialzieField]
    GameObject _player = null;

    void Start()
    {
        
    }

    void LateUpdate() // ��� Update�� ���� �Ŀ� ȣ��ȴ�.
    {
        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform); // ī�޶� �÷��̾ �ٶ󺸰� �Ѵ�.
    }

    public void SetQuterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
