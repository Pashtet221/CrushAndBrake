using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystickBG;
    public Vector3 rotation;

    private Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private Camera _cam;
    [SerializeField] private Vector3 mousePos;
    [Header("MousePosCordinate")]
    [SerializeField] private float x, y, z;

    public Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public float moveSpeed = 0.1f;


    private void Start()
    {
         _cam = Camera.main;
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        HideJoystick();
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(joystickBG.transform.position, mousePosition, moveSpeed);
    }

    public void PointerDown()
    {
        mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = z;
        mousePos.x = x;

        joystickBG.transform.eulerAngles = rotation;

        joystickBG.transform.position = mousePos;
        joystickTouchPos = mousePos;
        DisplayJoystick();
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
        DisplayJoystick();

        rb.MovePosition(position);
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystickBG.transform.position = joystickOriginalPos;
        HideJoystick();
    }


    private void DisplayJoystick()
    {
        joystickBG.SetActive(true);
    }

    private void HideJoystick()
    {
        joystickBG.SetActive(false);
    }
}
