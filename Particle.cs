using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 movementDirection;
    public Vector2 force;
    public float pressure;
    public float density;

    public Particle(Vector2 position)
    {
        this.position = position;
        this.velocity = Vector2.zero;
        this.density = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateChangeInVelocity(Vector2 vector) 
    {
        acceleration += vector; 
    }
    public void UpdateVelocity(Vector2 newVel) 
    {
        velocity += newVel;
    }
    public void ChangeVelocity(float num) 
    {
        velocity = velocity * num;
    } 
    public void UpdatePositions() 
    {
        position = position + velocity;
    }
    public void UpdateDensity(float num)
    {
        density += num;
    }
    public void SetDensity(float num) 
    {
        density = num;
    }
}
