using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected float _damage = 10;
    [SerializeField] protected float _timeAttack = 2.0f;
    [SerializeField] protected float _preparationTime = 1;
    [SerializeField] protected float _recuperationTime = 1;
    [SerializeField] protected float _time = 0;
    [SerializeField] protected float _stunTime = 1;

    [SerializeField] protected PlayerManagement _playerManagement;

    protected bool _canMakeDamage = false;

    [SerializeField] protected State _currentState;
    protected List<GameObject> _alreadyKick = new List<GameObject>();
    //[SerializeField] protected SpriteRenderer _spriteRenderer;

    protected virtual void  Awake()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _playerManagement = GetComponentInParent<PlayerManagement>();
    }

    protected enum State
    {
        Preparation, Attack, Recuperation
    }

}
