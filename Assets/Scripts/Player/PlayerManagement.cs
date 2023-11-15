using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    private float _stamina = 100;
    private float _maxStamina = 100;

    public GameObject[] _attacks = new GameObject[2]; // [0] = lightAttack // [1] = powerfulAttack

    [SerializeField] private float _life = 0;
    [SerializeField] private float _maxLife = 100;
    [SerializeField] private int _nbLife = 3;
    [SerializeField] private int _maxNbLife = 3;

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
        if (!GameManager.Instance.IsPaused)
        {
            StaminaManagement();
        }
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

            _waitCounter += Time.deltaTime;

            if (_waitCounter >= _waitToRegainStamina)
            {
                _canRegainStamina = true;
                _waitCounter = 0;
            }
        }

        if (_canRegainStamina && _stamina < 100)
        {
            _stamina += 10 * Time.deltaTime;
        }
        else if (_stamina > 100)
        {
            _stamina = 100;
        }
    }


    public void SetDefending(bool defending)
    { _isDefending = defending; }

    public float GetStamina()
    { return _stamina; }

    public float GetMaxStamina()
    { return _maxStamina; }



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
        CheckLife();
    }

    private void CheckLife()
    {
        if (_life <= 0)
        {
            Death();
        }
    }

    public void ResetLife()
    {
        _nbLife -= 1;
        if (_nbLife > 0)
            _life = _maxLife;
    }

    public void Death()
    {
        ResetLife();

        if (_nbLife > 0)
        {
            transform.position = SpawnPointManager.Instance.GetSpawnPoint();
            GetComponent<PlayerController>().Death();
        }
        else
        {
            _life = 0;
            gameObject.SetActive(false);
        }
    }

    public float GetLife()
    {
        return _life;
    }

    public float GetMaxLife()
    { return _maxLife; }

    public int GetMaxNbLife()
    { return _maxNbLife; }

    public int NbLife { get => _nbLife; set => _nbLife = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    
    public bool IsDefending { get => _stamina > 0 ? _isDefending : false;}
    
}
