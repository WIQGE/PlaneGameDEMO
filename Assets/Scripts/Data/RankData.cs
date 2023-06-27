using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// ���а�����
/// </summary>
public class RankData 
{
    public List<RankInfo> rankList = new List<RankInfo>();
}

/// <summary>
/// ���а�������
/// </summary>
public class RankInfo
{
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public int time;
}