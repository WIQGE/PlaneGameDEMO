using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton btnBack;
    public UILabel labTime;

    public List<GameObject> hpObjs;

    //��ǰ��Ϸ����ʱ��
    public float nowTime = 0;
    public override void Init()
    {
        btnBack.onClick.Add(new EventDelegate(() =>
        {
            //����˳���ť ��ʾȷ���˳����
            QuitPanel.Instance.ShowMe();
        }));

        ChangeHp(5);

        
    }

    /// <summary>
    /// �ṩ���ⲿ �ı�Ѫ���ķ���
    /// </summary>
    /// <param name="hp"></param>
    public void ChangeHp(int hp)
    {
        for (int i = 0; i < hpObjs.Count; i++)
        {
            hpObjs[i].SetActive(i < hp);
        }

    }


    private void Update()
    {
        nowTime += Time.deltaTime;
        //����ʱ����ʾ
        labTime.text = "";
        //Сʱ
        if ((int)nowTime / 3600 > 0)
        {
            labTime.text += (int)nowTime / 3600 + "h";
        }
        //��
        if ((int)nowTime % 3600 / 60 > 0 || labTime.text != "")
        {
            labTime.text += (int)nowTime % 3600 / 60 + "m";
        }
        //��
        labTime.text += (int)nowTime % 60 + "s";
    }
}
