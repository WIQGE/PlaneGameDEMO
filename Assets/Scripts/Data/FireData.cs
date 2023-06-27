using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 开火点数据集合
/// </summary>
public class FireData 
{
    public List<FireInfo> fireInfoList = new List<FireInfo>();
}

/// <summary>
/// 单个开火点数据
/// </summary>
public class FireInfo
{
    [XmlAttribute]
    public int id; //开火点ID
    [XmlAttribute]
    public int type; //开火点类型 （1散弹/2按顺序）
    [XmlAttribute]
    public int num; //数量 该组子弹 有多少颗
    [XmlAttribute]
    public float cd; //每颗子弹的间隔时间
    [XmlAttribute]
    public string ids; //关联的子弹ID  1,10代表 1~10子弹数据中随机\
    [XmlAttribute]
    public float delay; //每组开火 间隔时间

}