using UnityEngine;

public class ShieldManagement : MonoBehaviour
{
    private PlayerManagement _playerManagement;

    public GameObject _shield;
    private Transform _shieldTransform;
    private Material _shieldMaterial;
    private Color _originalShieldColor;
    private float _originalShieldColorAlpha;
    private float _playerStamina;
    private float _playerMaxStamina;
    private Vector2 _minMaxScale = new Vector2(.5f, 2.5f);

    private bool _isDefending = false;

    private void Awake()
    {
        _shieldTransform = _shield.GetComponent<Transform>();
        _shieldMaterial = _shield.GetComponent<Renderer>().material;

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

        SetScale();
        SetColor();
        SetActive();
    }

    private void SetScale()
    {
        _shieldTransform.localScale = Vector3.one * Mathf.Lerp(_minMaxScale.x, _minMaxScale.y, _playerStamina / 100);
    }

    private void SetColor()
    {
        Color newColor = Color.Lerp(Color.white, Color.red, (_playerMaxStamina - _playerStamina) / 100f);
        newColor.a = _originalShieldColorAlpha;
        _shieldMaterial.color = newColor;
    }

    private void SetActive()
    {
        _shield.SetActive(_isDefending && _playerStamina > 0 );
    }

    public void SetDefending(bool defending)
    {
        _isDefending = defending;
    }
}
