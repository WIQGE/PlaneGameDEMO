using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// ��������ݼ���
/// </summary>
public class FireData 
{
    public List<FireInfo> fireInfoList = new List<FireInfo>();
}

/// <summary>
/// �������������
/// </summary>
public class FireInfo
{
    [XmlAttribute]
    public int id; //�����ID
    [XmlAttribute]
    public int type; //��������� ��1ɢ��/2��˳��
    [XmlAttribute]
    public int num; //���� �����ӵ� �ж��ٿ�
    [XmlAttribute]
    public float cd; //ÿ���ӵ��ļ��ʱ��
    [XmlAttribute]
    public string ids; //�������ӵ�ID  1,10���� 1~10�ӵ����������\
    [XmlAttribute]
    public float delay; //ÿ�鿪�� ���ʱ��

}