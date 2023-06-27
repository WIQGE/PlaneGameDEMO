using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource bkAudio;

    private void Awake()
    {
        instance = this;
        //得到依附对象上的 音效组件
        bkAudio = GetComponent<AudioSource>();

        //初始化 音乐数据
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicIsOpen);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }
    /// <summary>
    /// 开关 背景音乐的函数
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }

    /// <summary>
    /// 设置背景音乐大小
    /// </summary>
    /// <param name="value"></param>
    public void SetBKMusicValue(float value)
    {
        bkAudio.volume = value;
    }
}
