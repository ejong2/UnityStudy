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
}