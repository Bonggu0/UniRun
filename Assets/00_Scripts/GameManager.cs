using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _firstMap;
    [SerializeField]
    GroundManager _groundMap;
    [SerializeField]
    GameObject _player;
    private Vector3 _playerFirsPos;
    public void Start()
    {
        _playerFirsPos = _player.transform.position;

    }
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            ResetGround();
        }
    }
    private void ResetGround()
    {
        _firstMap.transform.position = Vector3.zero;
        _player.transform.position = _playerFirsPos;  
        _groundMap.ResetPool();

        ObjectMovement first = _firstMap.GetComponent<ObjectMovement>();
        ObjectMovement gruond = _groundMap.GetComponent<ObjectMovement>();
        first.moveSpeed = -4f;
        gruond.moveSpeed = -4f;

        PlayerController player = _player.GetComponent<PlayerController>();
        player.isDie = false;
        player.isGround = true;
        
    }
}
