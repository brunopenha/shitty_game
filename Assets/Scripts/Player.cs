using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // Getter and Setter
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShootActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null){
            Debug.LogError("Spawn_Manager is NULL");
        }
    }


    // Update is called once per frame
    void Update()
    {
        calculaMovimento();

        // To limitade the fire rage, use Time.time (how long the game is running)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            Shoot();
        }
    }

    public void SpeedUp()
    {
        _speed = 10.0f;
        StartCoroutine(SlowDownRoutine());
    }

    void calculaMovimento()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // One unit is equal one meter --> one meter per frame
        //transform.Translate( Vector3.right * horizontalInput * _speed * Time.deltaTime); // One metter per second
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        /* Clamp will replace this
                if(transform.position.y >= 0){
                    transform.position = new Vector3(transform.position.x, 0,0);
                }else if(transform.position.y <= -3.8f ){
                    transform.position = new Vector3(transform.position.x, -3.8f, 0);
                }
        */

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        _nextFire = Time.time + _fireRate;
        //Debug.Log("Piu!");

    if(_isTripleShootActive){
        Instantiate(_tripleShotPrefab,transform.position,Quaternion.identity);
    }else{
        // When we add the position with the Vector and this is called as offset
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

    }

        
    }

    public void Damage(){
        _lives--;

        if(_lives < 1){
            
            if(_spawnManager != null){
                _spawnManager.playerIsDeath();
            }
            
            Destroy(this.gameObject);
        }
    }

    public void  ActiveTripleShot()
    {
        _isTripleShootActive = true;
        StartCoroutine(TriplePowerDownRoutine());
    }

    IEnumerator TriplePowerDownRoutine(){

        while(true){

            yield return new WaitForSeconds(5.0f);
            _isTripleShootActive = false;
        }

    }

     private IEnumerator SlowDownRoutine()
    {
        while(true){
            yield return new WaitForSeconds(5.0f);
            _speed = 5.0f;
        }
    }
}
