using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform _transform;

    private Vector2 _verticalMovement  = Vector2.zero;
    [SerializeField] private Vector3 _JumpForce = new Vector3(5, 5, 0);

    private float speed = 5.0f;

    private float _maxHoldJumpBtn = .5f;
    private float _timeHoldBtn = 0;

    private bool _isMoving = false;
    private bool _isJumpBtnPressed = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        if (_isMoving) 
        {
            _transform.position += new Vector3(_verticalMovement.x, 0, 0) * speed * Time.deltaTime;
        }

        if (_isJumpBtnPressed) 
        {
            rb.useGravity = false;
        }
        else
            rb.useGravity = true;

        if (_timeHoldBtn < Time.time) 
        {
            _isJumpBtnPressed = false;
        }
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        _verticalMovement = context.ReadValue<Vector2>();
        _isMoving = Mathf.Abs(_verticalMovement.x) > 0;
        Debug.Log(_verticalMovement.x + "   ::   "+ _verticalMovement.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _timeHoldBtn = Time.time + _maxHoldJumpBtn;
        _isJumpBtnPressed = context.ReadValueAsButton();
        if(_isJumpBtnPressed)
        {
            rb.AddForce(_JumpForce);
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

    }
}
