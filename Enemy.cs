using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector: Enemy")] // make set in inspector vars
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public string direction = "NW";
    private BoundsCheck bndCheck;

    private float number = 0;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); // on awake, check object against code in boundsCheck
    }

    public Vector3 pos // set position as inspector chosen initial position
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }


	// Use this for initialization
	void Start () {
        number = Random.Range(0, 4); // initializes random number var for random direction
    }

    // Update is called once per frame
    void Update () {
        Move();
        if(bndCheck != null && !bndCheck.isOnScreen)
        {
            if(pos.y < bndCheck.camHeight - bndCheck.radius) // if enemy is off screen,
            {
                Destroy(gameObject); // destroy
            }
        }
        if(bndCheck != null & bndCheck.offDown) // if enemy is off the bottom of the screen,
        {
            Destroy(gameObject); // destroy
        }
	}
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        if (direction == "S")
        {
            tempPos.y -= speed * Time.deltaTime;
        }
        if (direction == "RANDOM")
        {
            if (number < 1)
            {
                tempPos.y -= speed * Time.deltaTime;
                tempPos.x -= speed * Time.deltaTime;
            }
            else if (number < 2)
            {
                tempPos.y -= speed * Time.deltaTime;
                tempPos.x += speed * Time.deltaTime;
            }
            else if (number < 3)
            {
                tempPos.y += speed * Time.deltaTime;
                tempPos.x += speed * Time.deltaTime;
            }
            else if (number < 4)
            {
                tempPos.y += speed * Time.deltaTime;
                tempPos.x -= speed * Time.deltaTime;
            }
        }
        pos = tempPos;
    }
}
