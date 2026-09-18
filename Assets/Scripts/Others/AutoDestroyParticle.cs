using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem particles;
        
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!particles.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
