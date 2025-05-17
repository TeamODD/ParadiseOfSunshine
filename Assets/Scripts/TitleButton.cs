using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleButton : MonoBehaviour
{
    public Image fadeImage;         
    public float fadeDuration = 1f;  

    void Start()
    {
        if (fadeImage != null)
        {
            
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void OnStart()
    {
        if (fadeImage != null)
        {
            
            StartCoroutine(FadeOutAndLoadScene("IntroScene"));
        }
        else
        {
            
            SceneManager.LoadScene("IntroScene");
        }
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        
        SceneManager.LoadScene(sceneName);
    }
}
