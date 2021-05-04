using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down 4m/sec

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of screen respawn at top with new random x position

        if (transform.position.y < -3.8f) 
        {
            transform.position =
                new Vector3(Random.Range(-11f,11f), 3.8f, 0);

        }
    }
}
