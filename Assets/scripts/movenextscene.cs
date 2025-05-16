using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class movenextscene : MonoBehaviour
{
    public PlayableDirector director;
    public string nextSceneName;
    public GameObject panel;
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // director.stopped += OncutsceneFinished;

    }

    // Update is called once per frame

    //void OncutsceneFinished(PlayableDirector pd)
    //{
    //    SceneManager.LoadScene(nextSceneName);
    //}
    public void NextScene()
    {
        director.Stop();
        SceneManager.LoadScene("playertestFlower");
    }
    public void EndingScene()
    {
        if(PlayerHappiness.Instance != null)
            text.text += PlayerHappiness.Instance.currentHappy;
        panel.SetActive(true);
    }
}
