using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    public float timer = 10.0f;

    public GameController controller;

    float horizontal;
    float vertical;

    private Rigidbody2D rb;

    AudioSource audioSource;

    public AudioClip coinPickup;

    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        timerText.text = "Time Remaining: " + timer.ToString("f2");

        if(timer <= 0)
        {
            controller.Loser();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 position = rb.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rb.MovePosition(position);
    }

    void WinText()
    {
        // win screen code goes here.
        Debug.Log("you win");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Coin")
        {
            PlaySound(coinPickup);
            controller.ChangeScore();
            Destroy(col.gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
