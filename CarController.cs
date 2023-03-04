using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BY PRISTAX
public class Controller : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField]private driveType drive;

    public InputManager IM;

    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public GameObject centerOfMass;
    private Rigidbody rig;

    public float KPH;
    public float brakePower;
    public float radius = 6;
    public float DownForceValue = 50;
    public int motorTroque = 200;
    public float steeringMax = 4f;

    private void Start()
    {
        getObjects();
    }

    void FixedUpdate()
    {

        addDownForce();
        animateWheels();
        moveWehicle();
        steerVehicle();

    }

    private void moveWehicle()
    {
        float totalPower;

        if(drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTroque / 4);
            }
        }else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTroque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length -2; i++)
            {
                wheels[i].motorTorque = IM.vertical * (motorTroque / 2);
            }
        }

        KPH = rig.velocity.magnitude * 3.6f;
        if (IM.handbrake)
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = brakePower;
        }
        else
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = 0;
        }
    }

    private void steerVehicle()
    {
        if(IM.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }else if (IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    void animateWheels()
    {
        Vector3 wheelsPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelsPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelsPosition;
            wheelMesh[i].transform.rotation = wheelRotation;

        }
    }

    private void getObjects()
    {
        IM = GetComponent<InputManager>();
        rig = GetComponent<Rigidbody>();
    }

    private void addDownForce()
    {
        rig.AddForce(-transform.up * DownForceValue * rig.velocity.magnitude);
    }
}
