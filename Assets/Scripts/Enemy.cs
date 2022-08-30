using System.Collections;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private IntUpdateChannel _scoreUpdateChannel;
    [SerializeField] private UpdateChannel _damageChannel;
    [SerializeField] private int _pointValue = 10;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private GameObject _enemyLaser;
    [SerializeField] private Vector3 _laserOffset = new Vector3(0, 0.5f, 0);
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField] private AudioClip _explosionClip;

    private readonly int _animatorTrigger = Animator.StringToHash("OnEnemyDeath");

    private void Start()
    {
        TryGetComponent(out _anim);
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }

        TryGetComponent(out _audio);
        if(_audio == null)
        {
            Debug.LogError("AudioSource is NULL!");
        }
        StartCoroutine(EnemyFireRoutine());
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if(transform.position.y < -6)
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-8, 8), 5, 0);
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

    private IEnumerator EnemyFireRoutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f, 3));
        Instantiate(_enemyLaser, transform.position - _laserOffset, Quaternion.identity);
    }
}
