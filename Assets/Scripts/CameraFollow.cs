using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    public Manager manager;
    public Transform objectToFollow;
    public Vector3 offset;
    public float rotationSpeed = 1f;
    private Vector3 dragStartPosition;
    public float dragSpeed = 1f;

    public TMP_Text messageUI;

    void Start()
    {
        manager = Manager.GetInstance();
        DisplayMessage(GameProgress.HowToPlay, 10f);
    }

    void LateUpdate()
    {
        if (objectToFollow)
        {
            transform.position = objectToFollow.position + offset;
        }

        // If left mouse button is pressed down, start dragging
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = Input.mousePosition;
        }

        // If left mouse button is held down and moving, rotate the camera around the target
        if (Input.GetMouseButton(0))
        {
            float rotationX = (dragStartPosition.x - Input.mousePosition.x) * rotationSpeed;
            float rotationY = (dragStartPosition.y - Input.mousePosition.y) * rotationSpeed;
            transform.localEulerAngles += new Vector3(rotationY, rotationX, 0f);
            dragStartPosition = Input.mousePosition;
        }
        // If right mouse button is held down and moving, adjust the offset
        if (Input.GetMouseButton(1))
        {
            float offsetX = (dragStartPosition.x - Input.mousePosition.x) * dragSpeed * Time.deltaTime;
            float offsetY = (dragStartPosition.y - Input.mousePosition.y) * dragSpeed * Time.deltaTime;
            offset += new Vector3(offsetX, offsetY, 0f);
            dragStartPosition = Input.mousePosition;
        }
    }

    public void DisplayMessage(GameProgress key, float duration)
    {
        string[] _text = Manager.GetInstance().GetProgressMessage(key);
        if (_text.Length == 0)
        {
            return;
        }
        StartCoroutine(DisplayMessageCoroutine(_text, key, duration));
    }

    IEnumerator DisplayMessageCoroutine(string[] text, GameProgress key, float duration)
    {
           for (int i = 0; i < text.Length; i++)
           {
                messageUI.text = text[i];
                yield return new WaitForSeconds(duration);
           }

        messageUI.text = "";
        Manager.GetInstance().SetProgressByKey(key, true);
    }
}
