using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserLauncher : MonoBehaviour
{
    [SerializeField] private bool _isAI = false;
    [SerializeField] private Laser _pooledLaser;
    [SerializeField] private Laser _pooledTripleShotLaser;
    [SerializeField] private int _maxPoolSize = 10;
    [SerializeField] private float _fireRate = 0.5f;
    //[SerializeField] private Vector3 _laserOffset = new Vector3(0,0.5f,0);
    private IObjectPool<Laser> _objectPool;
    private float _canFire = -1.0f;
    private Player _player;
    private List<GameObject> _createdLasers = new List<GameObject>();
    
    private void Awake()
    {
        _objectPool = new ObjectPool<Laser>(CreateObject,OnGet,OnRelease,OnLaserDestroy,maxSize: _maxPoolSize);
        TryGetComponent(out _player);
    }

    private void OnDisable()
    {
        foreach (GameObject obj in _createdLasers)
        {
            Destroy(obj);
        }
    }

    private void Start()
    {
        StartCoroutine(AIFireRoutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& Time.time > _canFire && !_isAI)
        {
            Shoot();
        }
    }

    private Laser CreateObject()
    {
       Laser obj = Instantiate(_pooledLaser);
        
        obj.SetPool(_objectPool);
        _createdLasers.Add(obj.gameObject);
        return obj;
    }
    private void OnGet(Laser laser)
    {
       laser.transform.MatchPosition(this.gameObject);
       laser.transform.OffsetPosition( new Vector3(0,0.5f,0));
       laser.gameObject.SetActive(true);
    }
    private void OnRelease(Laser laser)
    {
        laser.gameObject.SetActive(false);
    }
    private void OnLaserDestroy(Laser laser)
    {
        _createdLasers.Remove(laser.gameObject);
        Destroy(laser.gameObject);
    }
    
    private void Shoot()
    {
        _canFire = Time.time + _fireRate;
        switch(_player.FireMode)
        {
            case 0:
                _objectPool.Get();
                break;
            case 1:
                print("Triple Shot");
                //_pooledLaser = _pooledTripleShotLaser;
                _objectPool.Get();
                break;
        }
        //_audio.PlayOneShot(_laserClip, 10);
    }
    private IEnumerator AIFireRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        _objectPool.Get();
    }
}
