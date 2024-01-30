using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    public static void LoadPreviousScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    } 

    public static void LoadSceneByIndex(int index){
        SceneManager.LoadScene(index);
    } 

    public static void LoadSceneByRelativeIndex(int index){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    } 
}
