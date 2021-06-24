using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosionPreFab;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //check for LASER collision (trigger)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //instantiate explosion at the position of the asteroid

            Instantiate(_explosionPreFab, transform.position, Quaternion.identity);

            //destroy explosion after 3 seconds
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.25f);
        }
    }


}
