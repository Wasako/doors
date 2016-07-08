using UnityEngine;
using System.Collections;

public class Flocking : MonoBehaviour {
    //public Transform flyTarget;
    // Use this for initialization
    public GameObject leader;
    Vector2 point = new Vector2(0, 0);
    int neighbourCount = 0;
    GameObject[] agentArray;
    public float flockingRadius = 100;
    Vector2 velocityVector;
    public float speed = 1;
    public float flockingRadiusLeader = 5;
    public float alignmentWeight, cohesionWeight, separationWeight, alignmentWeightLeader, cohesionWeightLeader, separationWeightLeader;
    void Start () {
        leader = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 alignment = computeAlignment();
        Vector2 cohesion = computeCohesion();
        Vector2 separation = computeSeparation();
        Vector2 alignmentLeader = computeAlignmentLeader();
        Vector2 cohesionLeader = computeCohesionLeader();
        Vector2 separationLeader = computeSeparationLeader();

        velocityVector = 
                (alignment * alignmentWeight + 
                cohesion * cohesionWeight + 
                separation * separationWeight +
                alignmentLeader * alignmentWeightLeader +
                cohesionLeader * cohesionWeightLeader +
                separationLeader * separationWeightLeader) * speed;


        gameObject.GetComponent<Rigidbody2D>().velocity = velocityVector;
        //Debug.Log("final vector " + velocityVector.x + " " + velocityVector.y);
        //transform.LookAt(leader.transform);
        Quaternion rotation = Quaternion.LookRotation
             (leader.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

    }
    public Vector2 computeAlignment()
    {
        int neighbourCount = 0;
        Vector2 alignmentVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");
        foreach (GameObject agent in agentArray)
        {
           // if (agent == gameObject) return alignmentVector;
            //Debug.Log("distance = " + Vector2.Distance(transform.position, agent.transform.position) + " radius = " + flockingRadius);


            if  (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                alignmentVector += agent.GetComponent<Rigidbody2D>().velocity;
                neighbourCount++;
               // Debug.Log("compute alighment");
            }
           // Debug.Log("Length = " + agentArray.Length + ", neigbour = " + neighbourCount);
        }

        alignmentVector /= neighbourCount;
        alignmentVector.Normalize();
       // Debug.Log("alignment vector " + alignmentVector.x + " " + alignmentVector.y);
        return alignmentVector;
    }
    public Vector2 computeCohesion()
    {
        int neighbourCount = 0;
        Vector2 cohesionVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");
        foreach (GameObject agent in agentArray)
        {
            if (agent == gameObject) return cohesionVector;


            if  (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                cohesionVector.x += agent.transform.position.x;
                cohesionVector.y += agent.transform.position.y;
                neighbourCount++;
              // Debug.Log("compute cohesion");
            }

        }

        cohesionVector /= neighbourCount;
        cohesionVector = new Vector2(cohesionVector.x - gameObject.transform.position.x, cohesionVector.y - gameObject.transform.position.y);
        cohesionVector *= 1;
        cohesionVector.Normalize();
        //Debug.Log("cohesion vector " + cohesionVector.x + " " + cohesionVector.y);
        return cohesionVector;
    }
    public Vector2 computeSeparation()
    {
        int neighbourCount = 0;
        Vector2 separationVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");
        foreach (GameObject agent in agentArray)
        {
            if (agent == gameObject) return separationVector;


            if  (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                separationVector.x += agent.transform.position.x - transform.position.x;
                separationVector.y += agent.transform.position.y - transform.position.y;
                neighbourCount++;
              //  Debug.Log("compute separation");
            }

        }

        separationVector /= neighbourCount;
        separationVector.Normalize();
        separationVector *= -1;
        //Debug.Log("separation vector " + separationVector.x + " " + separationVector.y);
        return separationVector;
    }

    public Vector2 computeAlignmentLeader()
    {
       
        Vector2 alignmentVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");
        //FOR 2D
        // alignmentVector += leader.GetComponent<Rigidbody2D>().velocity; THIS IS FOR 2D


        alignmentVector.x += leader.GetComponent<Rigidbody>().velocity.x;
        alignmentVector.y += leader.GetComponent<Rigidbody>().velocity.y;
        neighbourCount++;
                // Debug.Log("compute alighment");
          

       
        alignmentVector.Normalize();
        // Debug.Log("alignment vector " + alignmentVector.x + " " + alignmentVector.y);
        return alignmentVector;
    }
    public Vector2 computeCohesionLeader()
    {
        
        Vector2 cohesionVector = new Vector2(0, 0);
        
                cohesionVector.x += leader.transform.position.x;
                cohesionVector.y += leader.transform.position.y;
                neighbourCount++;
           

      
        cohesionVector = new Vector2(cohesionVector.x - gameObject.transform.position.x, cohesionVector.y - gameObject.transform.position.y);
        cohesionVector *= -1;
        cohesionVector.Normalize();
       /// Debug.Log("cohesion vector " + cohesionVector.x + " " + cohesionVector.y);
        return cohesionVector;
    }
    public Vector2 computeSeparationLeader()
    {
        int neighbourCount = 0;
        Vector2 separationVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");


        if ((Vector2.Distance(transform.position, leader.transform.position) < flockingRadiusLeader))
        {
            separationVector.x += leader.transform.position.x - transform.position.x;
            separationVector.y += leader.transform.position.y - transform.position.y;
            neighbourCount++;

        }

        
        separationVector.Normalize();
        separationVector *= 1;
        //Debug.Log("separation vector " + separationVector.x + " " + separationVector.y);
        return separationVector;
    }
}

