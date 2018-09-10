using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// 15：00～24：30
    /// 【問題】-90度の時だけ貼れない。
    /// -89.0001と数値が若干ずれるのが原因か　⇒　数値を合わせても解決せず
    /// ひとまず90度で置いておく
    /// </summary>

    public GameObject sticker;  //マウスについてくるステッカー
    public Sprite stickerSprite;//ステッカー画像
    private int count;  //回転させた回数

    public int objCount;    //貼るステッカーの数
    private int stickerCount;//貼ったステッカーの数

    public Timer timerScript;
    public UIScript UIscript;

    private bool isGamePlay;//プレイ中かどうか
    private bool clear; //クリアしたかどうか
    private int clearCount;     //クリアした回数
    private float distance;   //カメラの移動距離

    public StageData[] stageData;   //各ステージのデータ
    private int number;             //stageDataの配列番号


	void Start ()
    {
        count = 0;
        number = 0;
        objCount = stageData[0]._objCount;
        stickerCount = 0;
        distance = 0f;
        isGamePlay = true;
        clear = false;
        clearCount = 0;
	}

    //ゲームオーバー時に呼び出される関数
    public void setIsGamePlay()
    {
        isGamePlay = false;
    }
	
	void Update ()
    {
        if(isGamePlay)
        {
            //クリックしたオブジェクトを取得
            GameObject obj = GetObject();

            if (obj != null)
            {
                //向きがあっていたらステッカーを貼る(画像を差し替える)
                if (obj.transform.rotation.z == sticker.transform.rotation.z)
                {
                    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                    renderer.sprite = stickerSprite;
                    stickerCount++;
                }
            }
            //全て張り切れたらクリア、タイマーを止める
            if (stickerCount == objCount)
            {
                timerScript.TimerStop();               
                stickerCount = 0;
                
                //次のステージのデータをセット
                if (number < 1)
                {
                    number++;
                    objCount = stageData[number]._objCount;
                }
                //操作不能にする
                isGamePlay = false;
                clear = true;
                clearCount++;
            }

            //ステッカーをマウスの位置に
            Vector2 stickerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sticker.transform.position = stickerPosition;

            //右クリックでステッカー回転
            if (Input.GetMouseButtonDown(1))
            {
                setRotate();               
            }
        }
        
        if(clear)
        {
            //クリアしたら次のステージへ
            if (clearCount < 2)
            {
                setNextStage();
            }
            //全ステージクリア
            else
            {
                UIscript.setFinishUI();
            }
        }
        
    }

    //ステッカーを回転させる関数
    void setRotate()
    {
        int i = (int)sticker.transform.rotation.z + 90;
        sticker.transform.Rotate(new Vector3(0, 0, i));
        count++;
        //360度まわったら初期化
        if (count == 4)
        {
            Quaternion stickerRotate = sticker.transform.rotation;
            Quaternion q = new Quaternion(stickerRotate.x, stickerRotate.y, stickerRotate.z, 1f);
            q.z = 0;
            sticker.transform.rotation = q;
            count = 0;
        }
        Debug.Log(sticker.transform.rotation);
    }

    //カメラを左に移動させる関数
    void setNextStage()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.x -= 0.3f;
        distance -= 0.3f;
        Camera.main.transform.position = cameraPosition;
        //-30f動いたら、次のステージスタート
        if (distance <= -30f)
        {
            distance = 0f;
            clear = false;
            isGamePlay = true;
            timerScript.TimerPlay();
        }
    }

    //クリックしたオブジェクトを取得する関数
    private GameObject GetObject()
    {
        GameObject resultObj = null;

        //左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(tapPoint);
            if(col)
            {
                resultObj = col.transform.gameObject;
            }
        }
        return resultObj;
        //returnは関数の型を返す(voidとかGameObjectとか)
    }
}
