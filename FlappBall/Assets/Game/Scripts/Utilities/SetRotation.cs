using UnityEngine;

public class SetRotation : MonoBehaviour
{
    [SerializeField] private ScreenOrientation _screenOrientation;
    private void Start(){
        Screen.orientation = _screenOrientation;
    }
}
