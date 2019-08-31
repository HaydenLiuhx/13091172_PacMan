using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public GameObject wayPointsGo;
    public float speed = 0.3f;
    private readonly List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;

    private void Start()
    {
        foreach (Transform t in wayPointsGo.transform)
        {
            wayPoints.Add(t.position);
        }
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (transform.position != wayPoints[index])
        {
            Vector2 temperation = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
            GetComponent<Rigidbody2D>().MovePosition(temperation);
            Debug.Log(transform.position + ": " + wayPoints[index]);
        }        else        {

            index = (index + 1) % wayPoints.Count;            Debug.Log(index);        }

        Vector2 dir = wayPoints[index] - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacm")
        {
            Destroy(collision.gameObject);
        }
    }

}
