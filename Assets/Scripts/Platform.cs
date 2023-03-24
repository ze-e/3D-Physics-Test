using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float amount = 10f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * amount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * -amount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * -amount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right * amount * Time.deltaTime);
        }
    }
  
}
