using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour
{

    [SerializeField]private GameObject[] obstacles;

    private int obstacleIndex;

    void Start()
    {
        SpawnObstacle();
        StartCoroutine(SpawnAfterCooldown());
    }

    void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0, obstacles.Length);

        Instantiate(obstacles[obstacleIndex], transform.position, Quaternion.identity);
    }

    IEnumerator SpawnAfterCooldown()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            SpawnObstacle();
        }
    }
}
