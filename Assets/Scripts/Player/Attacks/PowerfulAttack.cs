using System.Collections;
using UnityEngine;

public class PowerfulAttack : Attack
{
    [SerializeField] private Vector3 _ejectionVector = new Vector3(-5, 50, 0);
    protected override void Awake() 
    {
        base.Awake();
        _damage = 35;
        _timeAttack = 0.3f;
        _preparationTime = .2f;
        _recuperationTime = .2f;
        _stunTime = 2;
    }
    private void OnEnable()
    {
        //_spriteRenderer.color = Color.white;
        _currentState = State.Preparation;
        _time = Time.time + _preparationTime;
        _alreadyKick.Clear();
        StartCoroutine(IPowerfulAttack());
    }

    private IEnumerator IPowerfulAttack()
    {
        while (_playerManagement.IsAttacking)
        {
            if (Time.time > _time)
            {
                switch (_currentState)
                {
                    case State.Preparation:
                        _currentState = State.Attack; _time = Time.time + _timeAttack; _canMakeDamage = true; /*_spriteRenderer.color = Color.red;*/
                        break;

                    case State.Attack:
                        _currentState = State.Recuperation; _time = Time.time + _recuperationTime; _canMakeDamage = false; /*_spriteRenderer.color = Color.black;*/
                        break;

                    case State.Recuperation:
                        gameObject.SetActive(false); /*_spriteRenderer.color = Color.yellow;*/ _playerManagement.IsAttacking = false;
                        break;
                }
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
