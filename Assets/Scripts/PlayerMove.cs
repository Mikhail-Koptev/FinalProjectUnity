using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private Rigidbody2D _rb;
    public bool _isGround;
    public bool _isDamaged;
    private Vector3 _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = Vector3.zero;
        _isGround = true;
        _isDamaged = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !_isDamaged)
        {
            _movement.x = _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isDamaged)
        {
            _movement.x = -_speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _movement.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
        {
            _isGround = false;
            _movement.y = _jumpForce;
            _rb.velocity = _movement;
        }
        
        if (!_isGround)
        {
            _movement.y -= _gravityScale * Time.deltaTime;
            _rb.velocity = _movement;
        }

        if (_isGround)
        {
            if (_isDamaged == true)
                _isDamaged = false;
        }

        _movement.y = _rb.velocity.y;
        _rb.velocity = _movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = false;
        }
    }

    public void GetDamage()
    {
        _isDamaged = true;
        _isGround = false;
        _movement.y = _jumpForce;
        _rb.velocity = _movement;
    }
}
