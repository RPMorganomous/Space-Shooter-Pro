using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.8f, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.position
            = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        // if space key pressed
        // instantiate laser

        if (Input.GetKeyDown("space"))
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }

    }
    void CalculateMovement()
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

        transform.position
            = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        // X position control
        // if player position on the x axis is greater than 11
        // then x position = -11
        // if player position on the x axis is less than -11
        // then x position = 11

        if (transform.position.x > 11)
        {
            transform.position
                = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position
                = new Vector3(11, transform.position.y, 0);
        }
    }
}
