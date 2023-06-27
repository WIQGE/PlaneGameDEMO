using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 子弹数据集合
/// </summary>
public class BulletData 
{
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();

}

/// <summary>
/// 子弹单条数据
/// </summary>
public class BulletInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type; //子弹的移动规则 1~5代表不同的五种规则
    [XmlAttribute]
    public float forwardSpeed; //正向 移动速度
    [XmlAttribute]
    public float rightSpeed; //横向 移动速度
    [XmlAttribute]
    public float roundSpeed; //旋转速度
    [XmlAttribute]
    public string resName; //子弹特性资源路径
    [XmlAttribute]
    public string deadEffRes; //子弹销毁特效资源路径
    [XmlAttribute]
    public float lifeTime; //子弹自动销毁生命周期时间

}
