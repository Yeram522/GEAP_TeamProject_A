using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbodyInfo;

    public float stoppingDistance = 0.3f;

    public bool arrived = false;

    private bool forceRotate = false;

    private Vector3 forceRotateDirection;

    public Vector3 destination;

    public float walkSpeed = 3.0f;

    public float rotationSpeed = 360.0f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        rigidbodyInfo = GetComponent<Rigidbody>();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destinationXZ = destination;
        destinationXZ.y = transform.position.y;

        Vector3 direction = (destinationXZ - transform.position).normalized;
        if (Terrain.activeTerrain.SampleHeight(direction) < 40)
        {
            Vector3 reverseDirection = direction * 6;
            reverseDirection.x = -reverseDirection.x;
            reverseDirection.z = -reverseDirection.z;
            direction = reverseDirection;
        }
        
        float distance = Vector3.Distance(transform.position,destinationXZ);

        Vector3 currentVelocity = velocity;

        if (arrived || distance < stoppingDistance)
            arrived = true;
        
        if (arrived) velocity = Vector3.zero;
        else velocity = direction * walkSpeed;

        velocity = Vector3.Lerp(currentVelocity, velocity,Mathf.Min (Time.deltaTime * 5.0f ,0.5f));
        velocity.y = 0;
        
        if (!forceRotate) {
            if (velocity.magnitude > 0.1f && !arrived) { 
                Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
            }
        } else {
            Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
        }

        rigidbodyInfo.velocity = velocity;
        
        if (forceRotate && Vector3.Dot(transform.forward,forceRotateDirection) > 0.98f)
            forceRotate = false;
    }
    
    
    private bool isGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
    
    public bool Arrived()
    {
        return arrived;
    }
    
    public void StopMove()
    {
        destination = transform.position; 
    }
    
    public void SetDestination(Vector3 destination)
    {
        arrived = false;
        this.destination = destination;
    }
	
    // 지정한 방향으로 향한다.
    public void SetDirection(Vector3 direction)
    {
        forceRotateDirection = direction;
        forceRotateDirection.y = 0;
        forceRotateDirection.Normalize();
        forceRotate = true;
    }
}
