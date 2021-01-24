using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
// Inheritables
    [Header("Traits")]
    public float speed;
    public float rotateSpeed;
    public float seeRange = 15f;
    public float lifetime = 10f;

    public GameController controller;

//  Random Tweaks
    [Header("Random Tweaks")]
    public float foodValue = 3;

    private float lifeCounter;

// DONT TOUCH... THIS HOW THEY MOVE
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;


    private Transform target;
    public string foodTag = "Food";

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        lifeCounter = lifetime;  // sets time until DEATH

        // speed = Random.Range(0f,3f);
        // rotateSpeed = Random.Range(30f,90f);

        InvokeRepeating ("UpdateTarget", 0f, 0.5f); // Updates nearest food location
    }


    void UpdateTarget ()            // Finds closest food..... DONT TOUCH
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag(foodTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestFood = null;

        foreach (GameObject food in foods)
        {
            float distanceToFood = Vector3.Distance(transform.position, food.transform.position);
            if (distanceToFood < shortestDistance)
            {
                shortestDistance = distanceToFood;
                nearestFood = food;
            }
        }

        if (nearestFood != null && shortestDistance <= seeRange)
        {
            target = nearestFood.transform;
        } else
        {
            target = null;
        }
    }

    void Update()
    {
        lifeCounter -= Time.deltaTime;  //Life shortener
        if (lifeCounter <= 0)
            Destroy(gameObject);        // Condition for un-life
            

        if (target == null)     // EVERYTHING IN UPDATE MUST GO BEFOR THIS LINE PLEASE!!!!!!!
            return;

        Vector3 dir = target.position - transform.position;       // direction of food

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;  // rotates to face food
        rb.rotation = angle;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed * Time.deltaTime * 100;   // Always on the run

        if (target != null)
            return;


        if (isWandering == false)    // when to wander
        {
            StartCoroutine(Wander());
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(Vector3.forward * -rotateSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Food")   // temporary aversion to death
        {
            lifeCounter = Mathf.Clamp(lifeCounter + foodValue, 0, lifetime);
            controller.Loser();
        }
    }
    
    void LoseText()
    {
        //lose screen goes here
        Debug.Log("you lose");
    }

    // How to wander
    IEnumerator Wander()
    {
        float rotTime = Random.Range(1f,2f);
        float rotWait = Random.Range(0f,2f);
        int rotLorR = Random.Range(1,3);

        isWandering = true;

        yield return new WaitForSeconds(rotWait);
        if (rotLorR == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        if (rotLorR == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
