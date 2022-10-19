using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] LayerMask ballLayer;
    [SerializeField] LayerMask RayLayer;
    [SerializeField] Camera cam;
    [SerializeField] Transform cameraPivot;
    [SerializeField] Vector2 camSensitivity;
    [SerializeField] float shootForce;
    Vector3 lastMousePosition;
    float ballDistance;
    bool isShooting;
    Vector3 forceDir;
    float forceFactor;
    private void Start()
    {
        ballDistance = Vector3.Distance(cam.transform.position, ball.Position) + 1;
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        // Physics.Raycast()
        // }
        if (ball.IsMoving)
            return;
        if (this.transform.position != ball.Position)
            this.transform.position = ball.Position;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, ballDistance, ballLayer))
                isShooting = true;
        }

        if (Input.GetMouseButton(0) && isShooting == true)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            // Debug.DrawRay(ray.origin, ray.direction * 100);
            if (Physics.Raycast(ray, out Hit, ballDistance * 2, RayLayer))
            {
                Debug.DrawLine(ball.Position, Hit.point);
                Debug.Log(Hit.point);

                var forceVector = ball.Position - Hit.point;
                forceVector = new Vector3(forceVector.x, 0, forceVector.z);
                forceDir = forceVector.normalized;
                var forceMagnitude = forceVector.magnitude;
                Debug.Log(forceMagnitude);
                forceMagnitude = Mathf.Clamp(forceMagnitude, 0, 5);
                forceFactor = forceMagnitude / 5;
            }
        }

        if (Input.GetMouseButton(0) && isShooting == false)
        {
            var Current = cam.ScreenToViewportPoint(Input.mousePosition);
            var last = cam.ScreenToViewportPoint(lastMousePosition);
            var delta = Current - last;

            //horizontal
            cameraPivot.transform.RotateAround(ball.Position, Vector3.up, delta.x * camSensitivity.x);
            //vertical
            cameraPivot.transform.RotateAround(ball.Position, cam.transform.right, -delta.y * camSensitivity.y);

            var angle = Vector3.SignedAngle(Vector3.up, cam.transform.up, cam.transform.right);

            // kalau melewati batas putar balik
            if (angle < 3)
                cameraPivot.transform.RotateAround(ball.Position, cam.transform.right, 3 - angle);

            else if (angle > 65)
                cameraPivot.transform.RotateAround(ball.Position, cam.transform.right, 65 - angle);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ball.AddForce(forceDir * shootForce * forceFactor);
            forceFactor = 0;
            forceDir = Vector3.zero;
            isShooting = false;
        }

        lastMousePosition = Input.mousePosition;
    }
}
