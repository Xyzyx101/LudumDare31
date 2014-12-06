using UnityEngine;
using System.Collections;

public abstract class SteeringBehaviour : MonoBehaviour 
{
	public Rigidbody objectToMove;
	public float maxSpeed;
	public float turnSpeed;
	public float wanderCircleDistance;
	public float wanderCircleRadius;
	public float wanderAngleAdjustment;
	
	private float wanderAngle;
  	private Vector3 steering = Vector3.zero;
	
	//apply seeking
	public void Seek(Vector3 position, float slowingRadius = 0)
	{
		steering += DoSeek(position,slowingRadius);	
	}
	//apply fleeing
	public void Flee(Vector3 position)
	{
		steering += DoFlee(position);	
	}
	//apply wander
	public void Wander()
	{
		steering += DoWander();	
	}
	//avoid collisions
	public void AvoidHullCollisions(float distance)
	{
		steering += DoAvoidHullCollisions(distance);
	}
	
	public void AdvancedCollisions(Sensor left, Sensor right)
	{
		steering += DoAdvancedCollisions(left,right);
	}
	
    // Should be called after all behaviors have been invoked
  	public void ApplySteering()
	{
		if(steering.magnitude > turnSpeed)
		{
			steering = steering.normalized * turnSpeed;
		}
		
		objectToMove.rigidbody.velocity = objectToMove.rigidbody.velocity + steering;
		
		if(objectToMove.rigidbody.velocity.magnitude > maxSpeed)
		{
			objectToMove.rigidbody.velocity  = (objectToMove.rigidbody.velocity).normalized * maxSpeed;	
		}
		
		objectToMove.transform.rotation = Quaternion.LookRotation(objectToMove.velocity);
	}
 
    // Reset the internal steering force.
    public void Reset()
	{
		steering = Vector3.zero;
	}

    //Seek and Arrive combined. Use slowing radius 0 to do regular seek.
    private Vector3 DoSeek(Vector3 position, float slowingRadius)
	{
		Vector3 desiredVelocity = position - transform.position;
    	float distance = desiredVelocity.magnitude;
    
		// Check the distance to detect whether the character  is inside the slowing area
    	if (distance < slowingRadius) 
		{
      		desiredVelocity = desiredVelocity.normalized * maxSpeed * (distance / slowingRadius);
    	} 
		else 
		{
     		desiredVelocity = desiredVelocity.normalized * maxSpeed;
    	}

		return desiredVelocity - objectToMove.velocity;
	}
	
	//flee from the target
    private Vector3 DoFlee(Vector3 position)
	{
		Vector3 desiredVelocity = transform.position - position;
		return desiredVelocity - objectToMove.velocity;
	}
	
    private Vector3 DoWander() 
	{
		// Calculate the circle center 
    	Vector3 circleCenter = objectToMove.velocity.normalized * wanderCircleDistance;
    
    	//Calculate the displacement force 
    	Vector3 displacement = new Vector3(0, 0, wanderCircleRadius);
    
    	//Randomly change the vector direction by making it change its current angle
    	displacement = SetAngle(displacement, wanderAngle);
    
    	//Change wanderAngle just a bit, so it won't have the same value next frame.
    	wanderAngle += Random.Range(-1f,1f) * wanderAngleAdjustment;
    
    	//Finally calculate and return the wander force 
    	return circleCenter+displacement; 
	}
	
	private Vector3 SetAngle(Vector3 vector, float rotation)
	{
    	float length = vector.magnitude;
    	vector.x = Mathf.Cos(rotation) * length;
    	vector.z = Mathf.Sin(rotation) * length;
		return vector;
  	}
	
	private Vector3 DoAvoidHullCollisions(float distance)
	{		
		RaycastHit hit1;
		Ray ray1 = new Ray(objectToMove.transform.position, objectToMove.velocity);
        if (Physics.Raycast(ray1, out hit1, distance, (1 << 9)))
		{
            Debug.DrawRay(ray1.origin, ray1.direction * distance, Color.magenta, 0.5f);
            Debug.DrawRay(ray1.origin, objectToMove.transform.position - hit1.transform.position, Color.green, 0.5f);
			return (objectToMove.transform.position - hit1.transform.position);
		}
        else if (Physics.Raycast(ray1, out hit1, distance, (1 << 11)))
        {
            Debug.DrawRay(ray1.origin, ray1.direction * distance, Color.magenta, 0.5f);
            Debug.DrawRay(ray1.origin, objectToMove.transform.position - objectToMove.transform.position * 2, Color.green, 0.5f);
            return (hit1.transform.parent.position - objectToMove.transform.position);
        }
        
		return Vector3.zero;
	}
	
	private Vector3 DoAdvancedCollisions(Sensor left, Sensor right)
	{
		if(right.isColliding)
		{
            //Debug.DrawRay(objectToMove.transform.position, right.collider.transform.position, Color.magenta, 0.5f);
			return (-objectToMove.transform.right) * turnSpeed;
		}
		else if(left.isColliding)
		{
            //Debug.DrawRay(objectToMove.transform.position, left.collider.transform.position, Color.magenta, 0.5f);
            return (objectToMove.transform.right) * turnSpeed;
		}
		
		return Vector3.zero;
	}
}
