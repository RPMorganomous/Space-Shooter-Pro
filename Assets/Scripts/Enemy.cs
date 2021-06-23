using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    //handle to animator component
    private Animator _anim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        // null check _player
        if (_player == null)
        {
            Debug.Log("The Player is NULL");
        }

        //assign the component
        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.Log("The Animator is NULL");
        }
    }

    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -3.8f) 
        {
            float randomX = Random.Range(-8f, 9f);
            transform.position =
                new Vector3(randomX, 7f, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Hit: " + other.transform.name);

        Player player = other.transform.GetComponent<Player>();

        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }
            //trigger animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }
            //trigger animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            
        }
    }
}
