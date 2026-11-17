using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 1.2f;

    

    public int recompensa = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    
}
