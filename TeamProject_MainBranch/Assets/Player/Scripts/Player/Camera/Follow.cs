using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;


    public float xmove = 0;  // X�� ���� �̵���
    public float ymove = 0;  // Y�� ���� �̵���
    public float distance = 3;
    private float xRotateMove, yRotateMove;

    public float rotateSpeed = 500.0f;
    private void Awake()
    {
        transform.position += offset;
    }
    void Update()
    {
        xmove += Input.GetAxis("Mouse X"); // ���콺�� �¿� �̵����� xmove �� �����մϴ�.
        ymove -= Input.GetAxis("Mouse Y"); // ���콺�� ���� �̵����� ymove �� �����մϴ�.
        transform.rotation = Quaternion.Euler(0, xmove, 0); // �̵����� ���� ī�޶��� �ٶ󺸴� ������ �����մϴ�.
        Vector3 reverseDistance = offset; // ī�޶� �ٶ󺸴� �չ����� Z ���Դϴ�. �̵����� ���� Z ������� ���͸� ���մϴ�.
        transform.position = target.transform.position - transform.rotation * reverseDistance; // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� �����մϴ�.
    }

    public Vector2 GetCursorPosition()
    {
        return Input.mousePosition;
    }
}
