using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform goal;
    private int wavepointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // setting the transform to that f the first position or first waypoint in the points array
        goal = WaypointNav.points[0];

    }

    // Update is called once per frame
    void Update()
    {
        // the direction we have to pointin order to get to the way point
        Vector3 dir = goal.position - transform.position;
        //float rota = goal.rotation - transform-rotation;
        // move in the direction of the difference
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        

            // TO COMBAT MATHEMATICAL ERRORS
        if (Vector3.Distance(transform.position, goal.position) <= 0.2f)
        {
            Turning();
            GetNextWaypoint();
        }

    //GET NEXT WAYPOINY
    void GetNextWaypoint()
    {
        if (wavepointIndex>= WaypointNav.points.Length -1)
        {
            Destroy(gameObject);
        }

        else
        {
            wavepointIndex++; 
        }

        //makes the goal equail to new point position using the waypointindex
        //and the points transform array.
        goal = WaypointNav.points[wavepointIndex];         
       }
    }

    //come back to game object turning
    void Turning()
    {
        transform.LookAt(goal);
        
    }

}
