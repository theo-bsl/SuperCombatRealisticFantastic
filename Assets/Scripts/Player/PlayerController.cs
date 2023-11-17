using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(Pla))]
public class PlayerController : MonoBehaviour
{
    //private Rigidbody rb;
    private Transform _transform;

    private PlayerManagement _playerManager;
    private ShieldManagement _shieldManager;
    public Animator _animator;

    public LayerMask _groundLayerMask;
    public LayerMask _collisionMask; 

    private Vector2 _verticalMovement  = Vector2.zero;
    [SerializeField] private Vector3 _ejectionVector = Vector3.zero;
    [SerializeField] private Vector3 _velocity = Vector3.zero;

    [SerializeField] private Quaternion _watchLeft = new Quaternion(0, 180, 0,0);
    [SerializeField] private Quaternion _watchRight = new Quaternion(0, 0, 0, 0);

    [SerializeField] private float _JumpVelocity = 0;
    [SerializeField] private float _JumpStartVelocity = 10;
    [SerializeField] private float _velocityMultiplicator = .8f;
    [SerializeField] private float _gravityScale = 9.1f;
    [SerializeField] private float _gravity = 0;
    [SerializeField] private float _maxGravity = 20;
    [SerializeField] private float _gravityMultiplicator = 1.2f;
    [SerializeField] private float _moveSpeedGround = 5.0f;
    [SerializeField] private float _moveSpeedAir = 7.5f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    [SerializeField] private float _maxHoldJumpBtn = .5f;
    [SerializeField] private float _dragResistance = 0.8f;
    [SerializeField] private float _stunTimer = 0;
    [SerializeField] private float _timeHoldBtn = 0;
    [SerializeField] private float _stayDistance = 1; // The distance in x the player need to stay far from.

    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isJumpBtnPressed = false;
    [SerializeField] private bool _isGrounded = false;
    private bool _ejectionCanBeReset = false;
    [SerializeField] bool _isStun = false;
    [SerializeField] private bool _canDoubleJump = true;
    [SerializeField] private bool _isWatchingRight = false;
    private bool _lastDirEjection = true;
    [SerializeField] private bool _isClickOnDefending = false;
    [SerializeField] private bool _isInPause = false;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        _playerManager = GetComponent<PlayerManagement>();
        _shieldManager = GetComponent<ShieldManagement>();
        _transform = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPaused)
        {
            IsGrounded();
            Stun();
            Move();
            Jump();
            Gravity();
            EjectionCollisionVerification();
            ApplyVelocity();
            Animation();
        }

        ProtectionStopPause();
    }

    private void Animation()
    {

        if(!_playerManager.IsAttacking)
        {
            _animator.SetBool("LightAttack", false);
            _animator.SetBool("PowerfulAttack", false);
        }

        _animator.SetBool("IsStun", _isStun);
        _animator.SetBool("IsDefending", _playerManager.IsDefending);

        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        {
            _animator.SetBool("GetHit", false);
        }
    }

    private void Move()
    {
        StayAtDistance();
        if (!_playerManager.IsAttacking && !_isStun && !_playerManager.IsDefending)
        {
            if (_verticalMovement.x > 0)
            {
                _isWatchingRight = true;
                _transform.rotation = _watchRight;
            }
            else if (_verticalMovement.x < 0)
            {
                _isWatchingRight = false;
                _transform.rotation = _watchLeft;
            }
            if (_isMoving && CollisionVerification())
            {
                _velocity.x += _verticalMovement.x * (_isGrounded ? _moveSpeedGround : _moveSpeedAir);
            }
        }
    }

    private void StayAtDistance()
    {
        if (Physics.Raycast(_transform.position, Vector3.left, out RaycastHit hitInfo, 1.1f, _collisionMask))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.distance < _stayDistance)
            {
                _velocity.x += 1;
                //_ejectionVector.x = 0;
            }

        }
        if (Physics.Raycast(_transform.position, Vector3.right, out RaycastHit hitInf, 1.1f, _collisionMask))
        {
            Debug.Log(hitInf.collider.gameObject.name);
            if (hitInf.distance < _stayDistance)
            {
                _velocity.x += -1;
                //_ejectionVector.x = 0;
            }
        }
    }

    private bool CollisionVerification()
    {
        Debug.DrawRay(_transform.position, _isWatchingRight ? Vector3.right : Vector3.left* 1.1f);
        if (Physics.Raycast(_transform.position, _isWatchingRight ? Vector3.right : Vector3.left, out RaycastHit hitInfo, 1.1f, _collisionMask))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.distance < 1)
                _velocity.x +=  -_verticalMovement.x;
            return false;
        }
        return true;
    }

    private void EjectionCollisionVerification()
    {
        if(_ejectionVector.x != 0)
        {
            if (Physics.Raycast(_transform.position, _ejectionVector.x > 0 ? Vector3.right : Vector3.left, out RaycastHit hitInfo, 1.1f, _collisionMask))
            {
                Debug.Log(hitInfo.collider.gameObject.name);
                _ejectionVector.x = 0; 
            }
        }
    }

    private void Stun()
    {
        if (Time.time > _stunTimer)
        {
            _isStun = false;
        }
        else
            _isStun = true;
    }
    private void Gravity()
    {
        if (_isJumpBtnPressed || _isGrounded)
        {
            _gravity = 0;
        }
        else if(_gravity == 0)
            _gravity = _gravityScale;


        if (_gravityScale != 0)
        {
            _velocity.y += -_gravity;

            if (_gravity < _maxGravity)
                _gravity *= _gravityMultiplicator;
            else
                _gravity = _maxGravity;
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
        if(Physics.Raycast(_transform.position, -transform.up, out RaycastHit hitInfo, 1.1f, _groundLayerMask))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            _transform.position = hitInfo.point + Vector3.up;
            _isGrounded = true;
            _canDoubleJump = true;
            if(_ejectionCanBeReset)
            {
                _ejectionCanBeReset = false;
                _ejectionVector = Vector3.zero;
            }

        }
        else
        {
            _ejectionCanBeReset = true;
            _isGrounded = false;
        }

        _animator.SetBool("IsGrounded", _isGrounded);
    }

    private void ApplyVelocity()
    {
        _ejectionVector *= _dragResistance;
        _lastDirEjection = _ejectionVector.x > 0;

        _velocity += _ejectionVector;
        _transform.position += _velocity * Time.deltaTime;

        _animator.SetFloat("VelocityY",_velocity.y);

        _velocity = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _verticalMovement = context.ReadValue<Vector2>();
        _isMoving = Mathf.Abs(_verticalMovement.x) > 0;

        _animator.SetBool("IsMoving", _isMoving);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpBtnPressed = context.ReadValueAsButton() && !GameManager.Instance.IsPaused;
        if ((_isGrounded || _canDoubleJump) && !_playerManager.IsAttacking && !_isStun && _isJumpBtnPressed && !_playerManager.IsDefending)
        {
            if (!_isGrounded)
            {
                _velocity.y = 0;
                _gravity = 0;
                _canDoubleJump = false;
            }
            _timeHoldBtn = Time.time + _maxHoldJumpBtn;

            if (_isJumpBtnPressed)
            {
                _JumpVelocity = _JumpStartVelocity;
                _animator.SetBool("Jump", true);
            }
        }
        else
            _animator.SetBool("Jump", false);
    }

    public void OnLightAttack(InputAction.CallbackContext context) 
    {
        if(!_isStun && !_playerManager.IsDefending &&!GameManager.Instance.IsPaused && context.ReadValueAsButton())
        {
            _animator.SetBool("LightAttack", true);
            _playerManager.ActiveLightAttack();
        }
            
    }

    public void OnPowerfulAttack(InputAction.CallbackContext context)
    {
        if (!_isStun && !_playerManager.IsDefending && !GameManager.Instance.IsPaused && context.ReadValueAsButton())
        {
            _animator.SetBool("PowerfulAttack", true);
            _playerManager.ActivePowerfulAttack();
        }
    }

    public void OnProtect(InputAction.CallbackContext context)
    {
        _isClickOnDefending = context.ReadValueAsButton();

        if(!_playerManager.IsAttacking && !_isStun && !GameManager.Instance.IsPaused)
        {
            _playerManager.SetDefending(_isClickOnDefending);
            _shieldManager.SetDefending(_isClickOnDefending);
        }
        else if (GameManager.Instance.IsPaused)
        {
            _isInPause = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null)
        {
            //_isMoving = false;
            //_ejectionVector.x = 0;
            _velocity.x = 0;
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    //_velocity.x += _lastDirEjection ? -2f : 2f;
    //}

    private void ProtectionStopPause()
    {
        if (_isInPause && !GameManager.Instance.IsPaused)
        {
            _playerManager.SetDefending(_isClickOnDefending);
            _shieldManager.SetDefending(_isClickOnDefending);
            _isInPause = false;
        }
    }

    public void Death()
    {
        _ejectionVector = Vector3.zero;
        _velocity = Vector3.zero;
        _stunTimer = 0;
        _isStun = false;
    }

    

    public Vector3 SetEjectionVector { set => _ejectionVector = value; }
    public bool GetWatchingDir { get => _isWatchingRight; }
    public float SetStunTime { set => _stunTimer = value; }

    public Animator GetAnimator { get => _animator; }
}
