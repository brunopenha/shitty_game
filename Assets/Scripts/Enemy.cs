using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    
    private Player _player;

    private Animator _destroyAnimation;
    // Start is called before the first frame update


    private AudioSource _audioSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null){
            Debug.LogError("Player is NULL");
        }

        _destroyAnimation = GetComponent<Animator>();

        if(_destroyAnimation == null){
            Debug.LogError("Animation is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null){
            Debug.LogError("AudioSource on Enemy is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8,8);
            transform.position = new Vector3(randomX,7f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Player"){
            // Damage the player
            Player player = other.transform.GetComponent<Player>();

            if(player != null){
                player.Damage();
            }
            _destroyAnimation.SetTrigger("OnEnemyDestroyed");
            _speed= 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
        }

        if(other.tag == "Laser"){
                        
            if(_player != null){
                _player.AddScore(10);
            } 
            _destroyAnimation.SetTrigger("OnEnemyDestroyed");
            _speed= 0;
            _audioSource.Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.8f);
               
        }
    }
}
