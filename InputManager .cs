using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BY PRISTAX
public class InputManager : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool handbrake;

    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = (Input.GetAxis("Jump") != 0)? true : false;
    }
}
