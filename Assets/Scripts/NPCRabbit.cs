using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCRabbit : MonoBehaviour
{
    Animator animator;
    public GameObject talkPanel;
    public TextMeshProUGUI text;
    public GameObject playerPanel;
    public TextMeshProUGUI textPlayer;
    [TextArea]
    public List<string> scripts;
    public List<int> playerIndex;
    public List<int> stop;
    public List<GameObject> npcs;
    public GameObject startFlower;
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
        animator = GetComponent<Animator>();
        talkPanel.SetActive(false);
        playerPanel.SetActive(false);
        foreach (GameObject go in npcs)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!finished)
        {
            if (currentIndex == 0)
            {
                if (Vector3.Distance(transform.position, playerMove.Instance.gameObject.transform.position) < 300f)
                    StartTalk();
            }
            //¿Ãµø ¿Â∏È
            if (currentIndex == 5)
            {
                StopTalk();
                if (waypoints.Length == 0) return;
                // «ˆ¿Á ∏Ò«• ¡ˆ¡°±Ó¡ˆ ¿Ãµø
                if (moveIndex < waypoints.Length)
                {
                    animator.SetBool("isWalking", true);
                    Transform target = waypoints[moveIndex];
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    // µµ¬¯«œ∏È ¥Ÿ¿Ω ¡ˆ¡°¿∏∑Œ
                    if (Vector3.Distance(transform.position, target.position) < 0.05f)
                    {
                        moveIndex++;
                    }
                }
                else if(moveIndex >= waypoints.Length)
                {
                    animator.SetBool("isWalking", false);
                    if(Vector3.Distance(transform.position, playerMove.Instance.gameObject.transform.position) < 350f)
                        StartTalk();
                }
            }
            //≤… »πµÊ ¿Â∏È
            if (currentIndex == 10)
            {
                StopTalk();
                if (SelectUI.Instance.isActive)
                {
                    StartTalk();
                }
            }
            //≤… √¨±‚±‚
            if (currentIndex == 12)
            {
                StopTalk();
                if (!(SelectUI.Instance.isActive))
                {
                    StartTalk();
                }
            }
            //ƒ˚¡Ó
            if (currentIndex == 14)
            {
                StopTalk();
                if (!(QuizUI.Instance.isActive))
                    StartTalk();
            }
            if (currentIndex >= scripts.Count)
            {
                StopTalk();
                finished = true;
                playerMove.Instance.isTuto = false;
                foreach(var npc in npcs)
                {
                    npc.SetActive(true);
                }
                Debug.Log("∆©≈‰≥°");
            }
            if (playerMove.Instance.isTalking && !finished && isTalking)
            {
                if (currentIndex == playerIndex[nextPlayerTalk] || currentIndex == 1)
                {
                    talkPanel.SetActive(false);
                    playerPanel.SetActive(true);
                    if (nextPlayerTalk < playerIndex.Count - 1)
                        nextPlayerTalk++;
                    textPlayer.text = scripts[currentIndex];
                }
                else
                {
                    talkPanel.SetActive(true);
                    playerPanel.SetActive(false);
                    text.text = scripts[currentIndex];
                }
                if (Input.GetMouseButtonDown(0) && isAbleNext)
                {
                    if (currentIndex < scripts.Count)
                    {
                        currentIndex++;
                        StartCoroutine(Wait());
                    }
                }
            }
        }
    }
    private void OnMouseDown()
    {
        if (finished)
            return;
        if (playerMove.Instance.isTalking)
            return;
        StartTalk();
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        isAbleNext = false;
        yield return new WaitForSeconds(0.2f);
        isAbleNext = true;
    }
    private void StopTalk()
    {
        isTalking = false;
        playerMove.Instance.isTalking = false;
        talkPanel.SetActive(false);
        playerPanel.SetActive(false);
    }
    private void StartTalk()
    {
        isTalking = true;
        playerMove.Instance.isTalking = true;
        talkPanel.SetActive(true);
        text.text = scripts[currentIndex];
    }

}
