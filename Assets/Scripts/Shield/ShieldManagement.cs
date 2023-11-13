using UnityEngine;

public class ShieldManagement : MonoBehaviour
{
    private PlayerManagement _playerManagement;

    public GameObject _shield;
    private Transform _shieldTransform;
    private Material _shieldMaterial;
    private Vector3 _originalShieldScale;
    private Color _originalShieldColor;
    private float _originalShieldColorAlpha;
    private float _playerStamina;
    private float _playerMaxStamina;

    private bool _isDefending = false;

    private void Awake()
    {
        _shieldTransform = _shield.GetComponent<Transform>();
        _shieldMaterial = _shield.GetComponent<Renderer>().material;

        _originalShieldScale = _shieldTransform.localScale;
        _originalShieldColor = _shieldMaterial.color;
        _originalShieldColorAlpha = _originalShieldColor.a;
    }

    private void Start()
    {
        _playerManagement = GetComponent<PlayerManagement>();

        _playerMaxStamina = _playerManagement.GetMaxStamina();
    }

    private void Update()
    {
        _playerStamina = _playerManagement.GetStamina();

        SetScale(_playerStamina);
        SetColor(_playerStamina);
        SetActive(_playerStamina);
    }

    private void SetScale(float Stamina)
    {
        float slowStamina = _playerStamina + (_playerMaxStamina - _playerStamina) / 2;

        _shieldTransform.localScale = _originalShieldScale * slowStamina / 100f;
    }

    private void SetColor(float Stamina)
    {
        Color newColor = Color.Lerp(Color.white, Color.red, (_playerMaxStamina - _playerStamina) / 100f);
        newColor.a = _originalShieldColorAlpha;
        _shieldMaterial.color = newColor;
    }

    private void SetActive(float Stamina)
    {
        _shield.SetActive(_isDefending && _playerStamina > 0);
    }

    public void SetDefending(bool defending)
    {
        _isDefending = defending;
    }
}
