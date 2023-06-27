using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //�����������
    public MusicData musicData;

    //���а�����
    public RankData rankData;

    //��ɫ����
    public RoleData roleData;
    //�ӵ�����
    public BulletData bulletData;
    //���������
    public FireData fireData;

    //��ǰѡ��Ľ�ɫ ���
    public int nowSelHeroIndex = 0;

    private GameDataMgr() 
    {
        //��ȡ����Ӳ���д洢����������
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData; ;
    
        //һ��ʼ �Ͷ�ȡ���� ���а�����
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData),"RankData") as RankData;

        //һ��ʼ ��ȡ��ɫ����
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData),"RoleData") as RoleData;
        //Debug.Log(roleData.rolelist[1].hp);
        //һ��ʼ��ȡ�ӵ�����
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        //һ��ʼ��ȡ���������
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;

    }

    #region ������Ч���
    //�����������ݷ���
    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    //�������ֿ��ط���
    public void SetMusicIsOpen(bool isOpen)
    {
        //������
        musicData.musicIsOpen = isOpen;
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);
    }
    //�������ִ�С���� 0~1
    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        BKMusic.Instance.SetBKMusicValue(value);
    }
    //������Ч���ط���
    public void SetSoundIsOpen(bool isOpen)
    {
        //������
        musicData.soundIsOpen = isOpen;
    }
    //������Ч��С���� 0~1
    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;
    }
    #endregion

    #region ���а����
    /// <summary>
    /// ������а�����
    /// </summary>
    /// <param name="name">�����</param>
    /// <param name="time">ͨ��ʱ��</param>
    public void AddRankData(string name,int time)
    {
        //��������
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);

        //����
        rankData.rankList.Sort((a, b) =>
        {
            return a.time > b.time ? -1 : 1;
        });

        //�Ƴ�����20��������
        if (rankData.rankList.Count > 20)
        {
            rankData.rankList.RemoveRange(20, rankData.rankList.Count - 20);
        }

        //��������
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    #endregion

    #region ����������
    /// <summary>
    /// �ṩ���ⲿ ��ȡ��ǰѡ��� ��ɫ����
    /// </summary>
    /// <returns></returns>
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    #endregion

}
