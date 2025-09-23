using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce =350;
    public Rigidbody2D rb;
    Collider2D col;
    public IAnimation curState;

    public Animator _animator;

    public bool isJump = false;
    public bool isGround = true;
    public bool isDie = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Die"))
        {
            isDie = true;
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
            isGround = false;
            Jump();
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

}
