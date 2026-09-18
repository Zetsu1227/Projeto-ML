using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    public float rotateSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed * 10);
    }
}
