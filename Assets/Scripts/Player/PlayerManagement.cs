using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public float _stamina = 100;
    private float _maxStamina = 100;
    public float _waitToRegainStamina = 2;
    public float _waitCounter = 0;
    public bool _canRegainStamina = true;

    public bool _isDefending = false;

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
}
