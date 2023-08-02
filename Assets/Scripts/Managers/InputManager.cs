using System;
using UnityEngine;

public class InputManager
{
    // Define delegates and events
    public Action OnSpaceDown;
    public Action OnLeftShiftDown;
    public Action OnLeftShift;
    public Action OnLeftMouseDown;
    public Action OnLeftMouseUp;

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnSpaceDown?.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftShift)) OnLeftShiftDown?.Invoke();
        if (Input.GetKey(KeyCode.LeftShift)) OnLeftShift?.Invoke();
        if (Input.GetMouseButtonDown(0)) OnLeftMouseDown?.Invoke();
        if (Input.GetMouseButtonUp(0)) OnLeftMouseUp?.Invoke();
    }
}
