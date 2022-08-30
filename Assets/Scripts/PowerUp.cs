using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private int _powerUpID;
    [SerializeField] private IntUpdateChannel _powerUpChannel;

    private void Update()
    {
       Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _powerUpChannel.CallIntUpdate(_powerUpID);
            Destroy(gameObject);
        }
    }
}
