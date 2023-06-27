using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 所有角色的数据集合
/// </summary>
public class RoleData 
{
    public List<RoleInfo> roleList = new List<RoleInfo>();
}
/// <summary>
/// 单个角色数据
/// </summary>
public class RoleInfo
{
    [XmlAttribute]
    public int hp; //血量
    [XmlAttribute]
    public float speed; //速度
    [XmlAttribute]
    public int volume; //体积
    [XmlAttribute]
    public string resName; //资源路径
    [XmlAttribute]
    public float scale; //选角面板使用的 模型缩放大小
}
