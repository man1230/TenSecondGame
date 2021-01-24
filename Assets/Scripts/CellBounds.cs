using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 18 || transform.position.x < -18)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0);
        }
    }
}
