using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShot;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.8f, 0);

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private int _lives = 3;

    private float _canFire = -1.0f;

    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;

    private SpawnManager _spawnManager;

    int lastRunnerTripleShot = 0;
    int lastRunnerSpeedBoost = 0;

    void Start()
    {
        transform.position
            = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown("space") && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_tripleShotActive == true)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
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
        if (_speedBoostActive == true)
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        

        transform.position
            = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

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

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive()
    {
        if (_tripleShotActive == false)
        {
            _tripleShotActive = true;
            StartCoroutine(TripleShotPowerDownRoutine());
        }
        else
        {
            lastRunnerTripleShot = lastRunnerTripleShot + 1;
            Debug.Log("lastRunner = " + lastRunnerTripleShot);
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(5.0f);
        if (lastRunnerTripleShot == 0)
        {
            _tripleShotActive = false;
        }
        else
        {
            lastRunnerTripleShot = lastRunnerTripleShot - 1;
            _tripleShotActive = false;
            Debug.Log("TS OFF");
            TripleShotActive();
        }
    }

    public void SpeedBoostActive()
    {
        if (_speedBoostActive == false)
        {
            _speedBoostActive = true;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        else
        {
            lastRunnerSpeedBoost = lastRunnerSpeedBoost + 1;
        }
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        if (lastRunnerSpeedBoost == 0)
        {
            _speedBoostActive = false;
        }
        else
        {
            lastRunnerSpeedBoost = lastRunnerSpeedBoost - 1;
            _speedBoostActive = false;
            SpeedBoostActive();
        }
        
    }

}
