using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton btnClose;

    public UISlider sliderMusic;
    public UISlider sliderSound;

    public UIToggle togMusic;
    public UIToggle togSound;
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            //�����Լ�
            HideMe();
        }));
        sliderMusic.onChange.Add(new EventDelegate(() =>
        {
            //�ı�������С �Լ�����
            GameDataMgr.Instance.SetMusicValue(sliderMusic.value);
        }));
        sliderSound.onChange.Add(new EventDelegate(() =>
        {
            //�ı�������С �Լ�����
            GameDataMgr.Instance.SetSoundValue(sliderSound.value);

        }));
        togMusic.onChange.Add(new EventDelegate(() =>
        {
            //���ر�������
            GameDataMgr.Instance.SetMusicIsOpen(togMusic.value);
        }));
        togSound.onChange.Add(new EventDelegate(() =>
        {
            //������Ч
            GameDataMgr.Instance.SetSoundIsOpen(togSound.value);
        }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ����������������
        MusicData musicData = GameDataMgr.Instance.musicData;
        sliderMusic.value = musicData.musicValue;
        sliderSound.value = musicData.soundValue;
        togMusic.value = musicData.musicIsOpen;
        togSound.value = musicData.soundIsOpen;
    }
    public override void HideMe()
    {
        base.HideMe();
        //�����Լ� ����������
        GameDataMgr.Instance.SaveMusicData();
    }
}
