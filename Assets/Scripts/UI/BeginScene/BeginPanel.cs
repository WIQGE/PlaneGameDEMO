using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnBegin;
    public UIButton btnRank;
    public UIButton btnSetting;
    public UIButton btnQuit;
    public override void Init()
    {
        //������ť�¼�
        btnBegin.onClick.Add(new EventDelegate(() =>
        {
            //��ʾ ѡ�����
            ChoosePanel.Instance.ShowMe();
            //�����Լ�
            HideMe();
        }));

        btnRank.onClick.Add(new EventDelegate(() =>
        {
            //��ʾ ���а�
            RankPanel.Instance.ShowMe();
        }));

        btnSetting.onClick.Add(new EventDelegate(() =>
        {
            //��ʾ ����
            SettingPanel.Instance.ShowMe();
        }));

        btnQuit.onClick.Add(new EventDelegate(() =>
        {
            //�˳���Ϸ
            Application.Quit();
        }));
    }
}
