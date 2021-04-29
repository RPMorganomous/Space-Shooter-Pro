using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position
            = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput =
            Input.GetAxis("Horizontal");

        float verticalInput =
            Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // Y position control
        // if the player position on the y axis is greater than zero
        // then y position = 0
        // if the player position on the y axis is less than -3.8f
        // then y position = -3.8f

        if (transform.position.y > 0 )
        {
            transform.position
                = new Vector3(transform.position.x, 0, 0);
        }
    }
}
