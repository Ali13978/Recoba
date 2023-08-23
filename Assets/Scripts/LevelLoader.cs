using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
   // public GameObject loadingScreen;
    public Slider slider;
    //public Text progressText;
    void Start()
    {
        LoadLevel(2);
    }
    public void LoadLevel(int sceneIndex)
    {
        sceneIndex++;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
       // loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            slider.value = progress;
           // progressText.text = "Loading..." + progress * 100f +"%";
           // Debug.Log(progressText);
            yield return null;
        }

    }



}
