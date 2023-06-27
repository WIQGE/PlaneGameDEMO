using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// �ӵ����ݼ���
/// </summary>
public class BulletData 
{
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();

}

/// <summary>
/// �ӵ���������
/// </summary>
public class BulletInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type; //�ӵ����ƶ����� 1~5����ͬ�����ֹ���
    [XmlAttribute]
    public float forwardSpeed; //���� �ƶ��ٶ�
    [XmlAttribute]
    public float rightSpeed; //���� �ƶ��ٶ�
    [XmlAttribute]
    public float roundSpeed; //��ת�ٶ�
    [XmlAttribute]
    public string resName; //�ӵ�������Դ·��
    [XmlAttribute]
    public string deadEffRes; //�ӵ�������Ч��Դ·��
    [XmlAttribute]
    public float lifeTime; //�ӵ��Զ�������������ʱ��

}
