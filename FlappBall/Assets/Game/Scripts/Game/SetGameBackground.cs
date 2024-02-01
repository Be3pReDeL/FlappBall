using UnityEngine;
using UnityEngine.UI;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class SetGameBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgrounds;
    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();

        ApplyCurrentBackground();
    }

    private void ApplyCurrentBackground() {
        string currentBackground = PlayerPrefsManager.GetCurrentItem("Background_current");
        foreach (Sprite background in _backgrounds) {
            if (background != null && background.name == currentBackground) {
                _image.sprite = background;
                
                break;
            }
        }
    }
}
