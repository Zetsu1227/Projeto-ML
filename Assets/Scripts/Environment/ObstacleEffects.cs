using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public GameObject particlePrefab;

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
