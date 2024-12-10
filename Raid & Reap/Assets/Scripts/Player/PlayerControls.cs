using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    //Joystick
    public GameObject joystick;
    public GameObject joystickBase;
    public Vector2 joystickVector;
    private Vector2 joystickTouchPosition;
    private Vector2 joystickOriginalPosition;
    private float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        //joystickOriginalPosition = joystickBase.transform.position;
        joystickRadius =  joystickBase.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void PointerDown() {
        //Debug.Log("PointerDown triggered"); for checking - JC
        joystick.transform.position = Input.mousePosition;
        joystickBase.transform.position = Input.mousePosition;
        joystickTouchPosition = Input.mousePosition;
    }*/

    public void Drag(BaseEventData baseEventData) {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVector = (dragPos - joystickTouchPosition).normalized;

        //float joystickDistance = Vector2.Distance(dragPos, joystickTouchPosition);
        float joystickDistance = Vector2.Distance(dragPos, joystickBase.transform.position);

        // Calculate the distance from the joystick background center
        Vector2 direction = dragPos - (Vector2)joystickBase.transform.position;
        joystickVector = direction.normalized;

        if(joystickDistance < joystickRadius) {
            joystick.transform.position = dragPos;
            //joystick.transform.position = joystickTouchPosition + joystickVector * joystickDistance;
        }
        else {
            joystick.transform.position = (Vector2)joystickBase.transform.position + joystickVector * joystickRadius;
            //joystick.transform.position = joystickTouchPosition + joystickVector * joystickRadius;
        }
    }

    public void PointerUp() {
        joystickVector = Vector2.zero;
        joystick.transform.position = joystickBase.transform.position;
        //joystick.transform.position = joystickOriginalPosition;
        //joystickBase.transform.position = joystickOriginalPosition;
    }

    public Vector2 GetJoystickInput()
    {
        return joystickVector; // Access joystick vector
    }
}
