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
            //隐藏自己
            HideMe();
        }));
        sliderMusic.onChange.Add(new EventDelegate(() =>
        {
            //改变音量大小 以及数据
            GameDataMgr.Instance.SetMusicValue(sliderMusic.value);
        }));
        sliderSound.onChange.Add(new EventDelegate(() =>
        {
            //改变音量大小 以及数据
            GameDataMgr.Instance.SetSoundValue(sliderSound.value);

        }));
        togMusic.onChange.Add(new EventDelegate(() =>
        {
            //开关背景音乐
            GameDataMgr.Instance.SetMusicIsOpen(togMusic.value);
        }));
        togSound.onChange.Add(new EventDelegate(() =>
        {
            //开关音效
            GameDataMgr.Instance.SetSoundIsOpen(togSound.value);
        }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己并更新上面的内容
        MusicData musicData = GameDataMgr.Instance.musicData;
        sliderMusic.value = musicData.musicValue;
        sliderSound.value = musicData.soundValue;
        togMusic.value = musicData.musicIsOpen;
        togSound.value = musicData.soundIsOpen;
    }
    public override void HideMe()
    {
        base.HideMe();
        //隐藏自己 并保存数据
        GameDataMgr.Instance.SaveMusicData();
    }
}
