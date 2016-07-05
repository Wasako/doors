using UnityEngine;
using System.Collections;

public class Flocking : MonoBehaviour {
    //public Transform flyTarget;
    // Use this for initialization
    Vector2 point = new Vector2(0, 0);
    int neighbourCount = 0;
    GameObject[] agentArray;
    public float flockingRadius;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public Vector2 computeAlignment()
    {
        int neighbourCount = 0;
        Vector2 alignmentVector = new Vector2(0, 0);
        agentArray = GameObject.FindGameObjectsWithTag("bird");
        foreach (GameObject agent in agentArray)
        {
            if (agent == gameObject) return alignmentVector;


            if (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                alignmentVector += agent.GetComponent<Rigidbody2D>().velocity;
                neighbourCount++;
            }
            
        }

        alignmentVector /= neighbourCount;
        alignmentVector.Normalize();
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


            if (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                cohesionVector.x += agent.transform.position.x;
                cohesionVector.y += agent.transform.position.y;
                neighbourCount++;
            }

        }

        cohesionVector /= neighbourCount;
        cohesionVector = new Vector2(cohesionVector.x - gameObject.transform.position.x, cohesionVector.x - gameObject.transform.position.x);
        cohesionVector.Normalize();
        
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


            if (Vector2.Distance(transform.position, agent.transform.position) < flockingRadius)
            {
                separationVector.x += agent.transform.position.x - transform.position.x;
                separationVector.y += agent.transform.position.y - transform.position.y;
                neighbourCount++;
            }

        }

        separationVector /= neighbourCount;
        separationVector.Normalize();
        return separationVector;
    }
}

