using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint : MonoBehaviour
{
    //one of four groups waypoints
    public GameObject wayPointsGo;
    public float speed = 0.3f;
    private readonly List<Vector2> wayPoints = new List<Vector2>();
    //waypoints index
    public int index = 0;
    //hided textbox
    public GameObject Tix;
    public bool gameend;
    //time for defeat bgm
    public float endtime=10f;
    public GameObject Player;
    private void Start()
    {
        foreach (Transform t in wayPointsGo.transform)
        {
            Vector2 v2= new Vector2(t.transform.position.x, t.transform.position.y);
            wayPoints.Add(v2);
        }
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Vector2 thisTransform = new Vector2(this.transform.position.x, this.transform.position.y);
        if (gameend != true|Player.GetComponent<Pacman>().gameend != true )
        {
              if (thisTransform != wayPoints[index])
            {

                Vector2 direction = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
                GetComponent<Rigidbody2D>().MovePosition(direction);
                Debug.Log(transform.position + ": " + wayPoints[index]);
            }
            else
            {
                //cycle(12,13,14,1,...)
                index = (index + 1) % wayPoints.Count;
                Debug.Log(index);
            }
        }

        Vector2 dir = wayPoints[index] - thisTransform;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

        if(gameend==true)
        {
            if (endtime > 0)
            {
                endtime -= Time.deltaTime;
            }
            else
            {
                gameend = false;
                endtime = 0;
                //ten seconds time fall to main menu
                SceneManager.LoadScene(0);
            }
        }
       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacm")
        {
            if (collision.gameObject.GetComponent<Pacman>().canEat == false)
            {
                collision.gameObject.SetActive(false);
                Tix.SetActive(true);
                gameend = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

}
