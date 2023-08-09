using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Define.CameraMode _mode = Define.CameraMode.QuarterView;

        [SerializeField]
        Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

        [SerializeField]
        GameObject _player = null;

        void Start()
        {
            // �÷��̾� GameObject �ڵ� �Ҵ�
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void LateUpdate() // ��� Update�� ���� �Ŀ� ȣ��ȴ�.
        {
            if (_player == null) return; // _player�� null�̸� ���� �ڵ带 �������� ����

            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform); // ī�޶� �÷��̾ �ٶ󺸰� �Ѵ�.
        }

        public void SetQuterView(Vector3 delta)
        {
            _mode = Define.CameraMode.QuarterView;
            _delta = delta;
        }
    }
}