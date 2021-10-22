using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Slider slider;           
    public Text text;
    public int sceneNum;
    private float temp;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LoadScene")
            StartCoroutine(LoadingScene()); 
    }
    
    private IEnumerator LoadingScene()
    {
        yield return null;
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Single); 
        asyncOperation.allowSceneActivation = false;                         

        while(!asyncOperation.isDone){
            temp = Mathf.Lerp(temp, asyncOperation.progress, Time.deltaTime*5);
            int full = 100;

            text.text = (((int)(temp/9*10*100)).ToString() + "%");
            slider.value = temp/9*10;

            if(temp/9*10 >= 0.99){
                text.text = full.ToString() + "%";
                slider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }  
            yield return null;
        }
    }

}