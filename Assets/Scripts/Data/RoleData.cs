using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// ���н�ɫ�����ݼ���
/// </summary>
public class RoleData 
{
    public List<RoleInfo> roleList = new List<RoleInfo>();
}
/// <summary>
/// ������ɫ����
/// </summary>
public class RoleInfo
{
    [XmlAttribute]
    public int hp; //Ѫ��
    [XmlAttribute]
    public float speed; //�ٶ�
    [XmlAttribute]
    public int volume; //���
    [XmlAttribute]
    public string resName; //��Դ·��
    [XmlAttribute]
    public float scale; //ѡ�����ʹ�õ� ģ�����Ŵ�С
}
