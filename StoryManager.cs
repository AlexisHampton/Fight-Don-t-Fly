using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : Singleton<StoryManager>
{
    [Header("Gen Story Stuff")]
    [SerializeField] private int place = 0;
    public NPC taos;
    public NPC rasmira;

    [Header("Start and Endgame")]
    public Transform cameraPos;
    public Transform cam;
    public Transform endGameCamPos;
    public Transform lleyaStartPos;
    public GameObject endGamePanel;

    private void Start()
    {
        place = 0;
        taos.canTalk = true;
        rasmira.canTalk = false;
        endGamePanel.SetActive(false);
    }

    public void IncreasePlace()
    {
        place++;
        if(place % 2 == 0)
        {
            taos.canTalk = true;
            rasmira.canTalk = false;
        }
        else
        {
            taos.canTalk = false;
            rasmira.canTalk = true;
        }

        if(place == 4)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("Min Yoongi");
        cam.position = cameraPos.position;
        cam.rotation = cameraPos.rotation;
    }

    public void EndGame()
    {
        cam.position = endGameCamPos.position;
        cam.rotation = endGameCamPos.rotation;
        PlayerMovement.Instance.gameObject.transform.position = lleyaStartPos.position;
        PlayerMovement.Instance.gameObject.transform.rotation = lleyaStartPos.rotation;
        endGamePanel.SetActive(true);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
