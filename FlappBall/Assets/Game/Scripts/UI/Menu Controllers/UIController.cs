using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {
    [SerializeField] private float _animationDuration = 1f;
    public List<ITweanable> _tweenObjects;

    protected virtual void Awake() {
        _tweenObjects = new List<ITweanable>();
    }

    public void AddTweenObject(ITweanable tweanable) {
        if (tweanable != null) {
            _tweenObjects.Add(tweanable);
        }
    }

    public void RemoveTweenObject(ITweanable tweanable) {
        if (tweanable != null) {
            _tweenObjects.Remove(tweanable);
        }
    }

    protected virtual void OnEnable() {
        AnimateObjects(true);
    }

    public virtual void Start(){
        OnEnable();
    }

    public void StartDisappearAnimation() {
        StartCoroutine(DisappearAndDeactivate());
    }

    private IEnumerator DisappearAndDeactivate() {
        AnimateObjects(false);
        yield return new WaitForSeconds(_animationDuration);
        gameObject.SetActive(false);
    }

    public virtual void AnimateObjects(bool appear) {
        foreach (var tweenObject in _tweenObjects) {
            if (tweenObject != null) {
                if (appear) {
                    tweenObject.Appear(_animationDuration);
                } else {
                    tweenObject.Disappear(_animationDuration);
                }
            }
        }
    }
}