using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private int _amount;

    [SerializeField] private GameObject _vfx;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable(){
        _spriteRenderer.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if(collider2D.CompareTag("Player")) {
            PlayerPrefsManager.AddCupsCount(_amount);

            _spriteRenderer.enabled = false;

            Instantiate(_vfx, transform.position, Quaternion.identity);

            GetComponent<BoxCollider2D>().enabled = false;

            _audioSource.Play();
        }
    }
}
