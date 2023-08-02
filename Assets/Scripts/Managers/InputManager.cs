using System;
using UnityEngine;

public class InputManager
{
    // 입력에 따른 이벤트 정의
    public Action OnSpaceDown;
    public Action OnLeftShiftDown;
    public Action OnLeftShift;
    public Action OnLeftMouseDown;
    public Action OnLeftMouseUp;

    // 프레임마다 입력 상태를 확인하고 이벤트를 실행하는 메서드
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnSpaceDown?.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftShift)) OnLeftShiftDown?.Invoke();
        if (Input.GetKey(KeyCode.LeftShift)) OnLeftShift?.Invoke();
        if (Input.GetMouseButtonDown(0)) OnLeftMouseDown?.Invoke();
        if (Input.GetMouseButtonUp(0)) OnLeftMouseUp?.Invoke();
    }

    /**
    Input.GetMouseButtonDown(0)에서 0은 마우스의 왼쪽 버튼을 나타냅니다. Unity에서 마우스 버튼은 다음과 같이 인덱싱됩니다:

    0: 왼쪽 버튼
    1: 오른쪽 버튼
    2: 중간 버튼 (휠 클릭)

    따라서 Input.GetMouseButtonDown(0)은 왼쪽 마우스 버튼이 눌렸을 때 첫 프레임에서 true를 반환합니다.
    비슷하게, Input.GetMouseButtonUp(0)은 왼쪽 마우스 버튼이 놓였을 때 첫 프레임에서 true를 반환합니다.

    이 코드의 의도는 왼쪽 마우스 버튼의 다양한 상태 (눌렀을 때, 놓았을 때 등)에 따라 관련된 이벤트를 호출하는 것입니다.
    */
}