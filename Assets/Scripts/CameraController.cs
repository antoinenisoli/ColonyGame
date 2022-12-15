using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 minPosition, maxPosition;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] float moveSpeed = 10f, scrollSpeed = 20f;

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        float zInput = Input.mouseScrollDelta.y;

        Vector3 zoom = new Vector3(0, 0, zInput) * Time.deltaTime * scrollSpeed;
        Vector3 movement = new Vector3(xInput, yInput).normalized * Time.deltaTime * moveSpeed;
        Vector3 newPosition = transform.position + movement + zoom;
        newPosition = GameDevHelper.ClampedVector(newPosition, minPosition, maxPosition);

        transform.position = newPosition;
    }
}
