using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    [System.Serializable]
    public class ChildArrayBGM
    {
        public AudioClip BGMSet;
        public string BGMName;
    }

    [System.Serializable]
    public class ChildArraySE
    {
        public AudioClip SESet;
        public string SEName;
    }

    public ChildArrayBGM[] BGM;
    public ChildArraySE[] SE;
    public AudioSource audioSource;

    // Start is called before the first frame update

    /// <summary>
    /// インデックス番号に対応したBGMに切り替える
    /// 最初に呼ぶBGMのインデックスは0
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBGM(int value)
    {
        Debug.Log("ChangeBGM");
        Debug.Log("BGMName:" + BGM[value].BGMName);
        audioSource.clip = BGM[value].BGMSet;
        audioSource.Play();
    }

    /// <summary>
    /// インデックス番号に対応したSEを生成する
    /// </summary>
    /// <param name="value"></param>
    public void MakeSE(int value)
    {
        Debug.Log("ChangeBGM");
        Debug.Log("SEName:" + SE[value].SEName);
        audioSource.PlayOneShot(SE[value].SESet);

    }

}
