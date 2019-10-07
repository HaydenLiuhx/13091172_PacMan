using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public float speed = 0.3f;
    Vector2 dest = Vector2.zero;

    public float cdTime;
    public bool canEat;

    public GameObject Yan;
    public GameObject Coin;

    public bool showcoin;
    public float showtime=0;

    public int winnumber;
    public int curnum;
    public GameObject win;
    public bool gameend;
    public float endtime=10.0f;
    public Text CurNum;
    public Text CanEatTime;
    //bgm
    //public AudioSource collisionSource;
    public AudioClip[] audioClips = new AudioClip[2];
    public AudioSource reference;
    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 temperation = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temperation);

        if (Input.GetKey(KeyCode.W) && Valid(Vector2.up))
        {
            dest = (Vector2)transform.position + Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) && Valid(-Vector2.right))
        {
            dest = (Vector2)transform.position - Vector2.right;
        }

        if (Input.GetKey(KeyCode.S) && Valid(-Vector2.up))
        {
            dest = (Vector2)transform.position - Vector2.up;
        }
        if (Input.GetKey(KeyCode.D) && Valid(Vector2.right))
        {
            dest = (Vector2)transform.position + Vector2.right;
        }
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        CheckEatBigDots();
        CheckEating();
        CheckWin();
        //CollisionSource();
        //CollisionSource();
        CanEatTime.text = cdTime.ToString("00.00");
        CurNum.text = curnum.ToString();
    }

        void CheckEatBigDots()
        {
            //Pacman eat big dots
            if (cdTime > 0)
            {
                cdTime -= Time.deltaTime;
            }
            else
            {
                cdTime = 0;
                canEat = false;
                Yan.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        void CheckEating()
        {
            //Eating dots effects,there will be five golden dots
            //golden dots showing time is 0.5s;
            if (showcoin)
            {
                showcoin = false;
                Coin.SetActive(true);
                showtime = 0.5f;
            }
            if (showtime > 0)
            {
                showtime -= Time.deltaTime;
            }
            else
            {
                Coin.SetActive(false);
            }
        }

        void CheckWin()
        {
            //win or defeat
            //250 dots---win
            if (curnum == winnumber)
            {
                win.SetActive(true);
                gameend = true;
            //GameObject.Find("Camera").GetComponent<AudioSource>().clip = null;
            //GetComponent<AudioSource>().clip = audioClips[2];
           
                //GetComponent<AudioSource>().Play();
            }
            if (gameend)
            {
            //GameObject.Find("Camera").GetComponent<AudioSource>().clip = null;
            //GetComponent<AudioSource>().clip = audioClips[1];
                //GetComponent<AudioSource>().Play();
            
                if (endtime > 0)
                {
                    endtime -= Time.deltaTime;
                }
                else
                {
                    gameend = false;
                    endtime = 0;
                    SceneManager.LoadScene(0);
                }
            }
        }
    //void CollisionSource()
    //{
    //    //collisionSource = GetComponent<AudioSource>();
    //    if (showcoin == true)
    //    {
    //        GetComponent<AudioSource>().clip = audioClips[1];
            
    //        reference.volume = 0.35f;
    //    }
    //    else
    //    {
    //        reference.volume = 1.0f;
    //        GetComponent<AudioSource>().clip = null;
    //    }
    //}

    //show current dots number
    //show crazytime as 00:00



    bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    //eat big dots
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Big")
        {
            canEat = true;
            cdTime = 10f;
            Yan.GetComponent<SpriteRenderer>().color = Color.red;
        }
        // time to show coins around pacman
        if(collision.gameObject.name.Contains("pacdot")&& collision.gameObject.tag != "Big")
        {
            showcoin = true;
            
            GetComponent<AudioSource>().Play();
            reference.volume = 0.35f;
            
        }
        
    }

}
