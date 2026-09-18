using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 1.2f;

    public GameObject particlePrefab;

    public int recompensa = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            SpawnEffect(transform.position);
        }
    }

    void SpawnEffect(Vector3 position)
    {
        // Instantiate the prefab at the target position
        Instantiate(particlePrefab, position, Quaternion.identity);
    }
}
