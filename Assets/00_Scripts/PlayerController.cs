using Unity.Hierarchy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce =350;

    public Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private InputReader _inputReader;

    Collider2D col;
    public IAnimation curState;

    public int HP = 2;   
    public Animator _animator;

    public bool IsJump = false;
    public bool IsGround = true;
    public bool IsDie = false;
    public bool IsDameged = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Die"))
        {
            IsDie = true;
        }
        if(col.gameObject.CompareTag("Damaged") && IsDameged == false)
        {
            TakeDamage();
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _inputReader = GetComponent<InputReader>();

        _inputReader.OnClickReset += ReSetButton;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            IsJump = true;
            IsGround = false;
            Jump();
        }

        float frezeTime =0;
        if(IsDameged)
        {
            frezeTime += Time.deltaTime;
            Debug.Log(frezeTime);
        }
    }
    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce);
    }

    private void TakeDamage()
    {
        --HP;
        if (HP < 1)
        {
            IsDie = true;
            return;
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Obstarcle"), true);
        _sr.color = new Color(1,1,1,0.5f);
        IsDameged = true;
        
        Invoke("ReturnPlayerPlay", 2);
    }

    private void ReturnPlayerPlay()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Obstarcle"), false);
        _sr.color = new Color(1, 1, 1, 1); ;
        IsDameged = false;
        CancelInvoke("ReturnPlayerPlay");
    }

    private void ReSetButton()
    {
        ReturnPlayerPlay();
        HP = 2;
    }
}
