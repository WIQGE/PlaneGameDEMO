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
        //�õ����������ϵ� ��Ч���
        bkAudio = GetComponent<AudioSource>();

        //��ʼ�� ��������
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicIsOpen);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }
    /// <summary>
    /// ���� �������ֵĺ���
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }

    /// <summary>
    /// ���ñ������ִ�С
    /// </summary>
    /// <param name="value"></param>
    public void SetBKMusicValue(float value)
    {
        bkAudio.volume = value;
    }
}
