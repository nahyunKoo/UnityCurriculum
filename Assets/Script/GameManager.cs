using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int totalPoint;
    public int stageIndex;
    public int stagePoint;
    public PlayerMove player;
    public GameObject[] stages;

    public TextMeshProUGUI Uiscore;
    public TextMeshProUGUI Uistage;

    void Update()
    {
        Uiscore.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        Debug.Log("게임매니저 실행됐다");
        GameObject.Find("Canvas").transform.Find("Success!").gameObject.SetActive(false);

        if(stageIndex < stages.Length - 1)
        {
            //change stage
            stages[stageIndex].gameObject.SetActive(false);
            stageIndex++;
            stages[stageIndex].gameObject.SetActive(true);
            PlayerReposition();

            //ui change
            Uistage.text = "Stage" + (stageIndex + 1);
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("StartScene");
            Debug.Log("게임 클리어");

        }

        //calculate point
        totalPoint += stagePoint;
        stagePoint = 0;

    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-12.20001f, -2.47998f, 1f);
        player.VelocityZero();
    }
}
