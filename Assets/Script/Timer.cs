using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float seconds;  //制限時間
    private Text timerText;
    private bool isPlay;    //タイマーを動いてるかどうか

    private UIScript UIscript;

    public PlayerController playerScript;

	void Start ()
    {
        seconds = 5f;
        isPlay = true;

        UIscript = this.gameObject.GetComponent<UIScript>();

        timerText = GameObject.Find("Timer").GetComponent<Text>();    
	}

    public void TimerStop()
    {
        isPlay = false;
    }

    public void TimerPlay()
    {
        //制限時間を初期化
        seconds = 5f;
        isPlay = true;
    }

    void Update ()
    {
        if(isPlay == true)
        {
            seconds -= Time.deltaTime;
            //マイナスは表示しない
            if (seconds < 0f)
            {
                seconds = 0f;
            }
            timerText.text = seconds.ToString("F2");
        }
        //時間切れになったらパネルとボタンを表示
        if(seconds <= 0f)
        {
            UIscript.setGameOverUI();
            playerScript.setIsGamePlay();
        }

    }
     
        
}

    

