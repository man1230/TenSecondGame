using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore()
    {
        score += 1;

        if(score >= 6)
        {
            //win screen
            SceneManager.LoadScene("WinScreen");
            Destroy(player.gameObject);
        }
    }

    public void Loser()
    {
        //lose screen
        SceneManager.LoadScene("LoseScreen");
        Destroy(player.gameObject);
    }
}
