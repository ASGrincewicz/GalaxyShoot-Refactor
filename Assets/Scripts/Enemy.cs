using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private IntUpdateChannel _scoreUpdateChannel;
    [SerializeField] private UpdateChannel _damageChannel;
    [SerializeField] private int _pointValue = 10;
    [SerializeField] private float _speed = 4f;
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField] private AudioClip _explosionClip;

    private readonly int _animatorTrigger = Animator.StringToHash("OnEnemyDeath");

    private void Start()
    {
        
        if (!TryGetComponent(out _anim))
        {
            Debug.LogError("Animator is NULL!");
        }
        
        if(!TryGetComponent(out _audio))
        {
            Debug.LogError("AudioSource is NULL!");
        }
    }

    private void Update()
    {
       Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if(transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-8, 8), 5, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")|| other.CompareTag("Laser"))
        {
            _anim.SetTrigger(_animatorTrigger);
            _audio.PlayOneShot(_explosionClip);
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            _scoreUpdateChannel.CallIntUpdate(_pointValue);
            Destroy(gameObject,2.3f);
           
            if (other.CompareTag("Player"))
            {
               _damageChannel.CallUpdate();
            }
        }
    }
}
