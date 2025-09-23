using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController _controller;
    [SerializeField]
    private GameObject _hole;
    [SerializeField]
    private GameObject _obstacle;
    [SerializeField]
    private GameObject _bg;

    ObjectMovement _mapMove;
    [SerializeField]
    private ObjectMovement _firstMapMove;

    private ObjectPool<GameObject> _holePool;
    private ObjectPool<GameObject> _obstaclePool;

    private ObjectPool<GameObject> _bgPool;

    private Queue<GameObject> _platQueue;
    private Queue<GameObject> _bgQueue;


    private float _coolTime = 0f;

    private bool _isBgSpawn = false;
    private bool _isBgFlib = true;

    void Start()
    {
        _platQueue = new Queue<GameObject>();
        _bgQueue = new Queue<GameObject>();

        _mapMove = GetComponent<ObjectMovement>();

        _holePool = new ObjectPool<GameObject>(
            Createhole,
            OnGetGround,
            OnReleasetGround,
            OnDestroyGround
        );
        _obstaclePool = new ObjectPool<GameObject>(
            CreateObstacle,
            OnGetGround,
            OnReleasetGround,
            OnDestroyGround,
            false,
            3,
            50
        );
        _bgPool = new ObjectPool<GameObject>(
            CreateBg,
            OnGetGround,
            OnReleasetGround,
            OnDestroyGround,
            false,
            3,
            50
        );
    }
    void Update()
    {
        _coolTime += Time.deltaTime;

        if (!_controller.isDie)
        {
            if (_coolTime > 2.5f)
            {
                SpawnNextGruond();
                RealeseGruond();
                _coolTime = 0;
            }
        }
        else
        {
            _firstMapMove.moveSpeed = 0f;
            _mapMove.moveSpeed = 0f;
        }
    }

    private GameObject Createhole()
    {
        var ground = Instantiate(_hole);
        ground.transform.SetParent(this.transform);
        return ground;
    }
    private GameObject CreateObstacle()
    {
        var ground = Instantiate(_obstacle);
        ground.transform.SetParent(this.transform);
        return ground;
    }
    private GameObject CreateBg()
    {
        var ground = Instantiate(_bg);
        ground.transform.SetParent(this.transform);
        return ground;
    }
    private void OnGetGround(GameObject pfb)
    {
        pfb.SetActive(true);
    }
    private void OnReleasetGround(GameObject pfb)
    {
        pfb.SetActive(false);
    }
    private void OnDestroyGround(GameObject pfb)
    {
        Destroy(pfb);
    }

    private void SpawnNextGruond()
    {
        GameObject plat;
        GameObject bg;
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            plat = _obstaclePool.Get();

        }
        else
        {
            plat = _holePool.Get();
        }
        

        plat.transform.position = new Vector3(15, -4.5f, 0);
        plat.transform.SetParent(this.transform, true);
        _platQueue.Enqueue(plat);

        if (!_isBgSpawn)
        {
            bg = _bgPool.Get();
            if (!_isBgFlib)
            {
                var flip = bg.GetComponent<SpriteRenderer>();
                flip.flipX = true;
                _isBgFlib = true;
            }
            else _isBgFlib = false;

            _isBgSpawn = true;
            bg.transform.position = new Vector3(30, -3f, 0);
            bg.transform.SetParent(this.transform, true);
            _bgQueue.Enqueue(bg);
        } else _isBgSpawn = false;
    }

    private void RealeseGruond()
    {
        if (_platQueue.Count > 3)
        {
            GameObject temp = _platQueue.Dequeue();

            if (temp.CompareTag("Hole"))
            {
                _holePool.Release(temp);
            }
            else if (temp.CompareTag("Obstarcle"))
            {
                _obstaclePool.Release(temp);
            }
            
        }
        if (_bgQueue.Count > 3)
        {
            var test = _bgQueue.Dequeue();
            _bgPool.Release(test);
        }
    }

    public void ResetPool()
    {

        for (int i = 0; i < _platQueue.Count; i++)
        {
            GameObject temp = _platQueue.Dequeue();

            if (temp.CompareTag("Hole"))
            {
                _holePool.Release(temp);
            }
            else if (temp.CompareTag("Obstarcle"))
            {
                _obstaclePool.Release(temp);
            }
        }
        for (int i = 0; i < _bgQueue.Count; i++)
        {
            GameObject temp = _bgQueue.Dequeue();
            _bgPool.Release(temp);
        }

       
        
    }
}
