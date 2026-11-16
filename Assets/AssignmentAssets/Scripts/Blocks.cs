using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blocks : MonoBehaviour
{
    public float moveSpd = 10;
    public GameObject ARCamera;
    public PlayerInput moveInput;
    public PlayerInput rotInput;
    public TrickyTowers tricky;
    private Rigidbody rb;
    private float time;
    private float rotBuffer;

    private void OnEnable()
    {
        rb = transform.GetComponent<Rigidbody>();
        ARCamera = GameObject.FindWithTag("MainCamera");
        time = 0f;
        rotBuffer = 0f;

        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 Mjoystick = moveInput.actions["MJoystick"].ReadValue<Vector2>();
        if (Mjoystick != Vector2.zero)
        {
            if (Mathf.Abs(Mjoystick.x) > Mathf.Abs(Mjoystick.y))
            {
                transform.position += new Vector3(Mjoystick.x, 0, 0) * Time.deltaTime * moveSpd;
            }
            else
            {
                transform.position += new Vector3(0, 0, Mjoystick.y) * Time.deltaTime * moveSpd;
            }
        }

        Vector2 Rjoystick = rotInput.actions["RJoystick"].ReadValue<Vector2>();
        Vector3 rotateAxis = ARCamera.transform.forward;
        rotateAxis.y = 0;
        if (Rjoystick != Vector2.zero)
        {
            if (rotBuffer < 0f || Rjoystick.magnitude < 0.2)
            {
                Vector3 dir;
                if (Mathf.Abs(rotateAxis.x) > Mathf.Abs(rotateAxis.z))
                {
                    if (rotateAxis.x > 0)
                        dir = Vector3.right;
                    else
                        dir = -Vector3.right;
                }
                else
                {
                    if (rotateAxis.z > 0)
                        dir = Vector3.forward;
                    else
                        dir = -Vector3.forward;
                }

                if (Mathf.Abs(Mjoystick.x) > Mathf.Abs(Mjoystick.y))
                {
                    Vector3 right = Quaternion.AngleAxis(90f, Vector3.up) * dir;
                    if (Mjoystick.y > 0)
                        transform.rotation *= Quaternion.AngleAxis(90, right);
                    else
                        transform.rotation *= Quaternion.AngleAxis(-90, right);

                }
                else
                {
                    if (Mjoystick.x > 0)
                        transform.rotation *= Quaternion.AngleAxis(90, dir);
                    else
                        transform.rotation *= Quaternion.AngleAxis(-90, dir);
                }
                rotBuffer = 1f;
            }
            else
            {
                rotBuffer -= Time.deltaTime;
            }
        }


        if (Mathf.Abs( rb.velocity.magnitude ) < 0.1f)
        {
            time += Time.deltaTime;
        }
        if (time > 1.2f)
        {
            tricky.SpawnNextBlock();
            this.enabled = false;  
        }

        //if (Input.gyro.enabled)
        //{
        //    Quaternion gyroRot = Input.gyro.attitude;
        //    transform.rotation = new Quaternion(gyroRot.x, gyroRot.y, -gyroRot.z, -gyroRot.w);
        //}
    }
}
