using System;
using UnityEngine;

public class InputManager
{
    // �Է¿� ���� �̺�Ʈ ����
    public Action OnSpaceDown;
    public Action OnLeftShiftDown;
    public Action OnLeftShift;
    public Action OnLeftMouseDown;
    public Action OnLeftMouseUp;

    // �����Ӹ��� �Է� ���¸� Ȯ���ϰ� �̺�Ʈ�� �����ϴ� �޼���
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnSpaceDown?.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftShift)) OnLeftShiftDown?.Invoke();
        if (Input.GetKey(KeyCode.LeftShift)) OnLeftShift?.Invoke();
        if (Input.GetMouseButtonDown(0)) OnLeftMouseDown?.Invoke();
        if (Input.GetMouseButtonUp(0)) OnLeftMouseUp?.Invoke();
    }

    /**
    Input.GetMouseButtonDown(0)���� 0�� ���콺�� ���� ��ư�� ��Ÿ���ϴ�. Unity���� ���콺 ��ư�� ������ ���� �ε��̵˴ϴ�:

    0: ���� ��ư
    1: ������ ��ư
    2: �߰� ��ư (�� Ŭ��)

    ���� Input.GetMouseButtonDown(0)�� ���� ���콺 ��ư�� ������ �� ù �����ӿ��� true�� ��ȯ�մϴ�.
    ����ϰ�, Input.GetMouseButtonUp(0)�� ���� ���콺 ��ư�� ������ �� ù �����ӿ��� true�� ��ȯ�մϴ�.

    �� �ڵ��� �ǵ��� ���� ���콺 ��ư�� �پ��� ���� (������ ��, ������ �� ��)�� ���� ���õ� �̺�Ʈ�� ȣ���ϴ� ���Դϴ�.
    */
}