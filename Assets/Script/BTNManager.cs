using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public enum BTNtype
{
    Start,
    HowToPlay,
    BackToStart,
    exit,
    retry
}
public class BTNManager : MonoBehaviour
{
    public BTNtype type;
    public CanvasGroup MainCanvas;
    public CanvasGroup OptionCanvas;
    public CanvasGroup DiscriptionCanvas;
    bool isbool = false;
   
    // Update is called once per frame
    public void ButtonClick()
    {
        switch (type)
        { 
            case BTNtype.Start:
                SceneManager.LoadScene("GameScene");
                Debug.Log("���ӽ���");
                break;
            case BTNtype.HowToPlay:
                GameObject.Find("DiscriptionObject").transform.Find("DiscriptionCanvas").gameObject.SetActive(true);
                GameObject.Find("MainCanvasObject").transform.Find("MainCanvas").gameObject.SetActive(false);
                Debug.Log("���ӹ��");
                break;
            case BTNtype.BackToStart:
                GameObject.Find("DiscriptionObject").transform.Find("DiscriptionCanvas").gameObject.SetActive(false);
                GameObject.Find("MainCanvasObject").transform.Find("MainCanvas").gameObject.SetActive(true);
                Debug.Log("����ȭ�鵹�ư���");
                break;
            case BTNtype.exit:
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                break;
            case BTNtype.retry:
                SceneManager.LoadScene("GameScene");
                Time.timeScale = 1;
                break;

        }

    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isbool == false)
            {
                isbool = true;
                Time.timeScale = 0;
            }
            else
            {
                isbool = false;
                Time.timeScale = 1;
            }

            if (OptionCanvas.alpha == 0)
            {
                OptionCanvas.alpha = 1;
                OptionCanvas.blocksRaycasts = true;
                Debug.Log("����â");
            }
            else
            {
                OptionCanvas.alpha = 0;
                OptionCanvas.blocksRaycasts = false;
                Debug.Log("����â ����");
            }
        }
    }
}
