using System.Collections;
using UnityEngine;

public class PowerfulAttack : Attack
{
    private bool _isPlaying = false;
    [SerializeField] private Vector3 _ejectionVector = new Vector3(-5, 50, 0);
    protected override void Awake() 
    {
        base.Awake();
        _damage = 35;
        _preparationFrame = 32;
        _frameAttack = 40;
        _recuperationFrame = 42;
        _stunTime = 2;
    }
    private void OnEnable()
    {
        //_spriteRenderer.color = Color.white;
        _currentState = State.Preparation;
        _alreadyKick.Clear();
        _isPlaying = false;
        StartCoroutine(IPowerfulAttack());
    }

    private IEnumerator IPowerfulAttack()
    {
        while (_playerController.GetAnimator.GetCurrentAnimatorStateInfo(0).IsTag("PowerfulAttack") || !_isPlaying)
        {
            if (_playerController.GetAnimator.GetCurrentAnimatorStateInfo(0).IsTag("PowerfulAttack"))
            {
                _isPlaying = true;

                int currentFrame = GetCurrentFrame(_playerController.GetAnimator.GetCurrentAnimatorClipInfo(0));
                Debug.Log("Frame :: " + currentFrame);

                if (currentFrame < 0)
                {
                    _canMakeDamage = false;
                    _currentState = State.Preparation;
                    Debug.Log(_currentState);
                }

                else if (currentFrame < _preparationFrame)
                {
                    _canMakeDamage = true;
                    _currentState = State.Attack;
                    Debug.Log(_currentState);
                }
                else if (currentFrame < _frameAttack)
                {
                    _canMakeDamage = false;
                    _currentState = State.Recuperation;
                    Debug.Log(_currentState);
                }
                else
                {
                    _playerManagement.IsAttacking = false;
                    gameObject.SetActive(false);
                }
                Debug.Log("make Powerful Attack");
            }
            //Debug.Log("avsjgv");
            yield return null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerManagement>(out PlayerManagement manager) && !_alreadyKick.Contains(other.gameObject) && _canMakeDamage)
        {
            if (!manager.IsDefending)
            {
                _alreadyKick.Add(other.gameObject);
                PlayerController _playerController = gameObject.GetComponentInParent<PlayerController>();
                PlayerController _targetController = manager.gameObject.GetComponentInParent<PlayerController>();
                if (_playerController.GetWatchingDir)
                {
                    //Debug.Log("right");
                    _ejectionVector.x = Mathf.Abs(_ejectionVector.x);
                    _targetController.SetEjectionVector = _ejectionVector;
                }
                else
                {
                    //Debug.Log("left");
                    _ejectionVector.x = -Mathf.Abs(_ejectionVector.x);
                    _targetController.SetEjectionVector = _ejectionVector;
                }
                _targetController.SetStunTime = Time.time + _stunTime;
                manager.TakeDamage(_damage);
            }
        }

    }
}
