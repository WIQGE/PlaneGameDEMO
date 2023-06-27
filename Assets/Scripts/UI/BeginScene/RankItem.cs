using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labRank;
    public UILabel labName;
    public UILabel labTime;

    /// <summary>
    /// ���� ���а����� ����� ������ʾ��ʼ��
    /// </summary>
    /// <param name="rank">����</param>
    /// <param name="name">����</param>
    /// <param name="time">ʱ��</param>
    public void InitInfo(int rank,string name,int time)
    {
        //����
        labRank.text = rank.ToString();
        //����
        labName.text = name;
        //ʱ��
        string str = "";
        //Сʱ
        if (time / 3600 > 0)
        {
            str += time / 3600 + "h";
        }
        //��
        if (time % 3600 / 60 > 0 || str != "")
        {
            str += time % 3600 / 60 + "m";
        }
        //��
        str += time % 60 + "s";

        labTime.text = str;
    }
}
