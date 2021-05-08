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
        float randomX = Random.Range(-8f, 9f);
        transform.position =
            new Vector3(randomX, 4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // move down 4m/sec

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of screen respawn at top with new random x position

        if (transform.position.y < -3.8f) 
        {
            float randomX = Random.Range(-8f, 9f);
            transform.position =
                new Vector3(randomX, 7f, 0);

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hit: " + other.transform.name);

        Player player = other.transform.GetComponent<Player>();

        // check tag through other

        // if other is player
        // damage the player (later)
        // destroy the enemy
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            
            if (player != null)
            {
                player.Damage();
            }
        }


        // if other is laser
        // destroy laser
        // destroy us

        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
