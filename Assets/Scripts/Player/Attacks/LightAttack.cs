using System.Collections;
using UnityEngine;

public class LightAttack : Attack
{
    bool _isPlaying = false;
    protected override void Awake()
    {
        base.Awake();
        _damage = 10;
        _preparationFrame = 17;
        _frameAttack = 30;
        _recuperationFrame = 40;
        _stunTime = 1;
    }

    private void OnEnable()
    {
        //_spriteRenderer.color = Color.white;
        _currentState = State.Preparation;
        _alreadyKick.Clear();
        _isPlaying = false;
        StartCoroutine(ILightAttack());
    }

    private IEnumerator ILightAttack()
    {
        while (_playerController.GetAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LightAttack") || !_isPlaying)
        {
            if (_playerController.GetAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LightAttack"))
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
                Debug.Log("make light attack");
            }
            yield return null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerManagement>(out PlayerManagement manager) && !_alreadyKick.Contains(other.gameObject) && _canMakeDamage)
        {
            PlayerController _targetController = manager.gameObject.GetComponentInParent<PlayerController>();
            if (!manager.IsDefending)
            {
                _alreadyKick.Add(other.gameObject);
                _targetController.SetStunTime = Time.time + _stunTime;
                manager.TakeDamage(_damage);
            }
        }
    }
}
