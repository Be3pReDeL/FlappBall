using UnityEngine;

public class LoadingAnimationCompleted : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LoadScene.LoadSceneByIndex(GetBetaFeatures.SceneIndex);
    }
}
