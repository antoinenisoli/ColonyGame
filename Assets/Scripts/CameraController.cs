using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 minPosition, maxPosition;
    [SerializeField] float moveSpeed = 10f, scrollSpeed = 20f, followSpeed = 10f;
    Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position;
    }

    public void SetTarget(Settler settler)
    {
        Vector3 targetPos = settler.transform.position;
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        float zInput = Input.mouseScrollDelta.y;

        Vector3 zoom = new Vector3(0, 0, zInput) * Time.deltaTime * scrollSpeed;
        Vector3 movement = new Vector3(xInput, yInput).normalized * Time.deltaTime * moveSpeed;
        desiredPosition += movement + zoom;
        desiredPosition = GameDevHelper.ClampedVector(desiredPosition, minPosition, maxPosition);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
