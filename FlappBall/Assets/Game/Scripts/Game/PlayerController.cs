using UnityEngine.UI;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Sprite[] _skins;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private Image[] _yellowCards;
    [SerializeField] private GameObject _scene, _endGameScreen;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;

    private int health = 2;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        ApplyCurrentSkin();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            Jump();
        } else {
            _animator.SetBool("Jump", false);
        }
    }

    private void Jump() {
        if(_rb != null) {
            _animator.SetBool("Jump", true);
            _rb.velocity = Vector2.up * _jumpForce;

            _audioSource.PlayOneShot(_jumpSound);

            HapticFeedback.MediumFeedback();
        }
    }

    private void ApplyCurrentSkin() {
        string currentSkin = PlayerPrefsManager.GetCurrentItem("Skin_current");
        foreach (Sprite skin in _skins) {
            if (skin != null && skin.name == currentSkin) {
                _spriteRenderer.sprite = skin;
                
                break;
            }
        }
    }

    public void TakeDamage(int amount){
        if((health + amount) <= 2)
            health += amount;

        switch(health){
            case 2:
                _yellowCards[0].color = new Color(0f, 0f, 0f, 0.5f);
                _yellowCards[1].color = new Color(0f, 0f, 0f, 0.5f);
                break;
            case 1:
                _yellowCards[0].color = new Color(0f, 0f, 0f, 0.5f);
                _yellowCards[1].color = Color.white;
                break;
            case 0:
                _yellowCards[0].color = Color.white;
                _yellowCards[1].color = Color.white;
                break;
        }

        if(health <= 0){
            _endGameScreen.SetActive(true);
            
            _scene.SetActive(false);
        }
    }
}
