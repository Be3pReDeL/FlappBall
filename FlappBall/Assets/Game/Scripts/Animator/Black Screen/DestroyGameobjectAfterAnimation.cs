using UnityEngine;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class DestroyGameobjectAfterAnimation : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject);
    }
}
