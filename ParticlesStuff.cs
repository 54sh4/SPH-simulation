using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    public GameObject particlePrefab;

    public int horizontalNumberOfParticles = 9;
    public int verticalNumberOfParticles = 9;
    public float spacing = 0.5f;
    public float gravity = 1;
    public float wallBounce = 0.5f;
    public float smoothingRadius = 2;
    public float restDensity = 5;
    public float mass = 1;
    public float gasConstant = 1;
    public float forceMultiplyer = 1;
    

    private List<Particle> particles = new List<Particle>();
    private List<GameObject> particleObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        InitialiseParticlesLocations();
        InitialiseParticleObjects();
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDensitys();
        CalculatePressureForce();
        UpdateParticlePositions();
        CheckForBoxCollision();
        UpdateParticleObjects();

    }
    void InitialiseParticlesLocations() 
    {
        for (int i = 0; i < horizontalNumberOfParticles; i++)
        {
            for (int j = 0; j < verticalNumberOfParticles; j++)
            {
                Vector2 position = new Vector2(spacing * i, spacing * j);
                Particle part = new Particle(position);
                particles.Add(part);
            }
        }
    }
    void InitialiseParticleObjects() 
    {
        foreach (Particle p in particles)
        {
            GameObject particleObject = Instantiate(particlePrefab, p.position, Quaternion.identity);
            particleObjects.Add(particleObject);
        }
    }
    void UpdateParticleObjects()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particleObjects[i].transform.position = particles[i].position;
        }
    }
    void CheckForBoxCollision() 
    { foreach (Particle p in particles) 
        {
            if (math.abs(p.position.x) > 4.6f) 
            {
                p.position.x = 4.6f*math.sign(p.position.x);
                p.velocity.x = p.velocity.x * -1 * wallBounce;
            }
            if (math.abs(p.position.y) > 4.6)
            {
                p.position.y = 4.6f * math.sign(p.position.y);
                p.velocity.y = p.velocity.y * -1 * wallBounce;
            }
        }
        
    }

    void BoxCollisionCheck(Particle p) 
    {
        if (math.abs(p.position.x) > 4.6f)
        {
            p.position.x = 4.6f * math.sign(p.position.x);
            p.velocity.x = p.velocity.x * -1 * wallBounce;
        }
        if (math.abs(p.position.y) > 4.6)
        {
            p.position.y = 4.6f * math.sign(p.position.y);
            p.velocity.y = p.velocity.y * -1 * wallBounce;
        }
    }
    float DensityKernel(float distance) 
    {
        return (8 * math.pow((1 - distance / smoothingRadius), 3)) / (math.PI * smoothingRadius * smoothingRadius);
    }
    void CalculateDensitys() 
    {
        foreach (Particle p in particles) 
        {
            p.density = 0;
            foreach (Particle a in particles)
            {
                float distance = Vector2.Distance(p.position, a.position);
                if (distance < smoothingRadius)
                {
                    p.density += DensityKernel(distance) * mass;
                    Vector2 normalisedDistance = (p.position - a.position).normalized;
                    p.movementDirection += normalisedDistance * DensityKernel(distance);
                }
            }
        }
        
    }
    void CalculatePressureForce() 
    {
        foreach (Particle p in particles) 
        {
            p.pressure = forceMultiplyer*(p.density);
            p.force = p.pressure * p.movementDirection;
        }
    }
    void UpdateParticlePositions() 
    {
        foreach (Particle p in particles) 
        {
            Vector2 gravityVecotor = new(0f, -1 * gravity);
            p.position += gravityVecotor;
            p.position += p.force * Time.deltaTime;
        }
    }
}
