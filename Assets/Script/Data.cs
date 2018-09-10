using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

    [Header("StageData")]
    public StageData[] stageData;
}

[System.Serializable]
public class StageData
{
    public string lavel;
    public int _objCount;
}
