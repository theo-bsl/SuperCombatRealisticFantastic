using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //private Rigidbody rb;
    private Transform _transform;
    private PlayerManagement _playerManagement;
    private ShieldManagement _shieldManagement;

    public LayerMask _groundLayerMask;

    private Vector2 _verticalMovement  = Vector2.zero;
    [SerializeField] private Vector3 _velocity = Vector3.zero;

    [SerializeField] private float _JumpVelocity = 0;
    [SerializeField] private float _JumpStartVelocity = 5;
    [SerializeField] private float _velocityMultiplicator = .8f;
    [SerializeField] private float _gravityScale = 5;
    [SerializeField] private float _gravity = 0;
    [SerializeField] private float _maxGravity = 15;
    [SerializeField] private float _gravityMultiplicator = 1.05f;
    [SerializeField] private float _moveSpeedGround = 10.0f;
    [SerializeField] private float _moveSpeedAir = 7.5f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    [SerializeField] private float _maxHoldJumpBtn = 0.2f;
    private float _timeHoldBtn = 0;

    private bool _isMoving = false;
    private bool _isJumpBtnPressed = false;
    private bool _isGrounded = false;
    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _playerManagement = GetComponent<PlayerManagement>();
        _shieldManagement = GetComponent<ShieldManagement>();
    }


    private void Update()
    {
        IsGrounded();
        //Debug.Log(_isGrounded);
        //Debug.Log("qdnofsnjfn   "+_isJumpBtnPressed);
        Move();
        Jump();
        Gravity();
        ApplyVelocity();
    }

    private void Move()
    {
        if (_isMoving)
        {
            _velocity.x += _verticalMovement.x * (_isGrounded ?  _moveSpeedGround : _moveSpeedAir);
        }
    }
    private void Gravity()
    {
        if (_isJumpBtnPressed || _isGrounded)
        {
            _gravity = 0;
        }
        else if(_gravity == 0)
            _gravity = _gravityScale;
        else if (_gravity > _maxGravity)
        {
            _gravity = _maxGravity;
        }


        if (_gravityScale != 0)
        {
            _velocity.y += -_gravity;
            _gravity *= _gravityMultiplicator;

        }

    }

    private void Jump()
    {
        if (_timeHoldBtn < Time.time)
        {
            _isJumpBtnPressed = false;
        }

        if (_JumpVelocity > 0.01f)
        {
            _transform.position += new Vector3(0, _JumpVelocity,0) * _jumpSpeed * Time.deltaTime;
            
            if(!_isJumpBtnPressed) 
                _JumpVelocity *= _velocityMultiplicator;
        }
    }

    private void IsGrounded()
    {
        Debug.DrawRay(_transform.position, -transform.up * 1.1f, Color.red);
        if(Physics.Raycast(_transform.position, -transform.up, out RaycastHit hitInfo, 1.1f, _groundLayerMask))
        {
            //Debug.Log("aaaa  ::: " + hitInfo.point);
            _transform.position = hitInfo.point+ Vector3.up;
            _isGrounded = true;
        }
        else
        {
            _isGrounded= false;
        }
    }

    private void ApplyVelocity()
    {
        //Debug.Log(_velocity);
        _transform.position += _velocity * Time.deltaTime;
        _velocity = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _verticalMovement = context.ReadValue<Vector2>();
        _isMoving = Mathf.Abs(_verticalMovement.x) > 0;
        Debug.Log(_verticalMovement.x + "   ::   "+ _verticalMovement.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpBtnPressed = context.ReadValueAsButton();
        if (_isGrounded)
        {
            _timeHoldBtn = Time.time + _maxHoldJumpBtn;
            if (_isJumpBtnPressed)
            {
                _JumpVelocity = _JumpStartVelocity;
            }
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context) 
    {
    
    }

    public void OnPowerfulAttack(InputAction.CallbackContext context)
    {

    }

    public void OnProtect(InputAction.CallbackContext context)
    {
        _playerManagement.SetDefending(context.ReadValueAsButton());
        _shieldManagement.SetDefending(context.ReadValueAsButton());
    }
}
