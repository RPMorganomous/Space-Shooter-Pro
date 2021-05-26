using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 (adjustable)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //destroy when leave screen
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            
            if (player != null)
            {
                player.PowerUp();
            }
            
        }

    }

}
