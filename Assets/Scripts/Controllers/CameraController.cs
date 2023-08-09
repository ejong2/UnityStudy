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
            // 플레이어 GameObject 자동 할당
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void LateUpdate() // 모든 Update가 끝난 후에 호출된다.
        {
            if (_player == null) return; // _player가 null이면 이후 코드를 실행하지 않음

            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform); // 카메라가 플레이어를 바라보게 한다.
        }

        public void SetQuterView(Vector3 delta)
        {
            _mode = Define.CameraMode.QuarterView;
            _delta = delta;
        }
    }
}