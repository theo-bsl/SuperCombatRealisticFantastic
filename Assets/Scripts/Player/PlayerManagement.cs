using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public float _stamina = 100;
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
}
