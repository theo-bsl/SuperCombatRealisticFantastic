using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public float _stamina = 100;
    private float _maxStamina = 100;
    public float _waitToRegainStamina = 2;
    public float _waitCounter = 0;
    public bool _canRegainStamina = true;

    public GameObject[] _attacks = new GameObject[2]; // [0] = lightAttack // [1] = powerfulAttack

    [SerializeField] private float _life = 0;
    [SerializeField] private float _maxLife = 100;

    private float _waitToRegainStamina = 2;
    private float _waitCounter = 0;
    private bool _canRegainStamina = true;
    private bool _isAttacking = false;
    private bool _isDefending = false;

    private void Awake()
    {
        _life = _maxLife;
    }

    private void Update()
    {
        StaminaManagement();
    }

    private void StaminaManagement()
    {
        if (_isDefending && _stamina > 0)
        {
            _stamina -= 20 * Time.deltaTime;
        }

        if (_stamina <= 0)
        {
            _stamina = 0;
            _canRegainStamina = false;

            _waitCounter += _isDefending ? 0 : Time.deltaTime;

            if (_waitCounter >= _waitToRegainStamina)
            {
                _canRegainStamina = true;
                _waitCounter = 0;
            }
        }

        if (_canRegainStamina && _stamina < _maxStamina)
        {
            _stamina += 10 * Time.deltaTime;
        }
        
        if (_stamina > _maxStamina)
        {
            _stamina = _maxStamina;
        }
    }


    public void SetDefending(bool defending)
    { _isDefending = defending; }

    public float GetStamina()
    { return _stamina; }

    public float GetMaxStamina()
    { return _stamina; }
    
    public void ActiveLightAttack()
    {
        if(!_isAttacking)
        {
        _isAttacking = true;
        _attacks[0].SetActive(true);
        }
    }

    public void ActivePowerfulAttack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _attacks[1].SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        _life -= damage;
    }

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
}
