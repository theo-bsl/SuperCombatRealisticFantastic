using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected float _damage = 10;
    [SerializeField] protected int _frameAttack = 2;
    [SerializeField] protected int _preparationFrame = 1;
    [SerializeField] protected int _recuperationFrame = 1;
    [SerializeField] protected float _time = 0;
    [SerializeField] protected float _waitTime = 0;
    [SerializeField] protected float _stunTime = 1;

    [SerializeField] protected PlayerManagement _playerManagement;
    [SerializeField] protected PlayerController _playerController;

    protected bool _canMakeDamage = false;

    [SerializeField] protected State _currentState;
    protected List<GameObject> _alreadyKick = new List<GameObject>();
    //[SerializeField] protected SpriteRenderer _spriteRenderer;

    protected virtual void  Awake()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GetComponentInParent<PlayerController>();
        _playerManagement = GetComponentInParent<PlayerManagement>();
    }

    protected int GetCurrentFrame(AnimatorClipInfo[] animationClip)
    {
        Debug.Log("Animation :::: " + animationClip[0].weight + "    ////    " + animationClip[0].clip.length + "    ////    " + animationClip[0].clip.frameRate);
        return (int)((1 - animationClip[0].weight) * (animationClip[0].clip.length * animationClip[0].clip.frameRate));
    }

    protected enum State
    {
        Preparation, Attack, Recuperation
    }

}
