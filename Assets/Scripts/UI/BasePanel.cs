using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������嶼��̳��� ����ʹ��
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;
    public static T Instance => instance;



    protected virtual void Awake()
    {
        instance = this as T;
    }

    void Start()
    {
        //���൱�л�ǿ�е��� ��ʼ������
        //�ó�ʼ������ ����һ�������� ����ͱ���ȥʵ��
        Init();
    }

    //��Ҫ���� ��ʼ�� �ؼ����¼����� �ȵ��߼�
    public abstract void Init();

    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
