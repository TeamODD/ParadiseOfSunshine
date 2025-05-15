using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCRabbit : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI text;
    public GameObject playerPanel;
    public TextMeshProUGUI textPlayer;
    [TextArea]
    public List<string> scripts;
    public List<int> playerIndex;
    public List<int> stop;
    public GameObject startFlower;
    public playerMove player;
    public Transform[] waypoints;
    public float speed = 10f;

    private int currentIndex = 0;
    private int nextPlayerTalk = 0;
    private bool isAbleNext = true;
    private bool finished = false;
    private bool isTalking = false;
    private int moveIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        talkPanel.SetActive(false);
        playerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ÀÌµ¿ Àå¸é
        if(currentIndex == 4)
        {
            StopTalk();
            if (waypoints.Length == 0) return;
            // ÇöÀç ¸ñÇ¥ ÁöÁ¡±îÁö ÀÌµ¿
            if ( moveIndex < waypoints.Length)
            {
                Transform target = waypoints[moveIndex];
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                // µµÂøÇÏ¸é ´ÙÀ½ ÁöÁ¡À¸·Î
                if (Vector3.Distance(transform.position, target.position) < 0.05f)
                {
                    moveIndex++;
                }
            }
            else if (Vector3.Distance(transform.position, player.gameObject.transform.position) < 300f)
            {
                StartTalk();
            }
        }
        //²É È¹µæ Àå¸é
        if(currentIndex == 10)
        {
            StopTalk();
            if (SelectUI.Instance.isActive)
            {
                StartTalk();
            }
        }
        //²É Ã¬±â±â
        if (currentIndex == 11)
        {
            StopTalk();
            if(QuizUI.Instance.isActive)
                StartTalk();
        }
        //ÄûÁî
        if(currentIndex == 13)
        {
            StopTalk();
            if(!(QuizUI.Instance.isActive))
                StartTalk();
        }
        if (currentIndex >= scripts.Count)
        {
            finished = true;
            StopTalk();
        }
        if(player.isTalking && !finished && isTalking)
        {
            if(currentIndex == playerIndex[nextPlayerTalk] || currentIndex == 1)
            {
                talkPanel.SetActive(false);
                playerPanel.SetActive(true);
                if(nextPlayerTalk < playerIndex.Count - 1)
                    nextPlayerTalk++;
                textPlayer.text = scripts[currentIndex];
            }
            else
            {
                talkPanel.SetActive(true);
                playerPanel.SetActive(false);
                text.text = scripts[currentIndex];
            }
            if (Input.GetMouseButton(0) && isAbleNext)
            {
                if (currentIndex < scripts.Count)
                {
                    currentIndex++;
                    StartCoroutine(Wait());
                }
            }
        }
    }
    private void OnMouseDown()
    {
        if (finished)
            return;
        if (player.isTalking)
            return;
        StartTalk();
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        isAbleNext = false;
        yield return new WaitForSeconds(0.2f);
        isAbleNext = true;
        Debug.Log(currentIndex);
    }
    private void StopTalk()
    {
        isTalking = false;
        player.isTalking = false;
        talkPanel.SetActive(false);
        playerPanel.SetActive(false);
    }
    private void StartTalk()
    {
        isTalking = true;
        player.isTalking = true;
        talkPanel.SetActive(true);
        text.text = scripts[currentIndex];
    }

}
