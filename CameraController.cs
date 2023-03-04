using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Controller C;
    public GameObject Player;
    public GameObject cameraConstarint;
    public GameObject cameralookAt;
    public float speed;

    private void Awake()
    {
        C = Player.GetComponent<Controller>();
    }

    private void FixedUpdate()
    {
        follow();

        speed = (C.KPH >= 50) ? 20 : C.KPH / 4;
    }

    void follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraConstarint.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameralookAt.gameObject.transform.position);
    }
}
