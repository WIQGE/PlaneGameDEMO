using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    //������ť
    public UIButton btnClose;
    public UIButton btnLeft;
    public UIButton btnRight;
    public UIButton btnStart;

    //ģ�͸�����
    public Transform heroPos;

    //������ض���
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;

    //��ǰ��ʾ�ķɻ�ģ�Ͷ���
    private GameObject airPlaneObj;

    public Camera UICamera;

    public override void Init()
    {
        //ѡ���ɫ�� �����ʼ �г���
        btnStart.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        //��ť��� �߼�
        btnLeft.onClick.Add(new EventDelegate(() =>
        {
            //��ť ������
            GameDataMgr.Instance.nowSelHeroIndex--;
            //���С����С������ ֱ�ӵ������һ������
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;

            ChangeNowHero();
        }));
        //�Ұ�ť��� �߼�
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            //�Ұ�ť ������
            GameDataMgr.Instance.nowSelHeroIndex++;
            //��������������� ֱ�ӵ���0
            if (GameDataMgr.Instance.nowSelHeroIndex > GameDataMgr.Instance.roleData.roleList.Count - 1)
                GameDataMgr.Instance.nowSelHeroIndex = 0;

            ChangeNowHero();
        }));
        //�رհ�ť �߼�
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero() ;
    }
    public override void HideMe()
    {
        base.HideMe();
        DestorObj();
    }
    private void ChangeNowHero()
    {
        //�õ���ǰѡ��� ��ɫ����
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();

        //����ģ��
        //��ɾ����һ�εķɻ�ģ��
        DestorObj();
        //�ٴ�����ǰ�ķɻ�ģ��
        airPlaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        //���ø�����
        airPlaneObj.transform.SetParent(heroPos, false);
        //���ýǶȡ�λ�á�����
        airPlaneObj.transform.localPosition = Vector3.zero;
        airPlaneObj.transform.localRotation = Quaternion.identity;
        airPlaneObj.transform.localScale = Vector3.one * info.scale;
        //�޸�ΪUI��
        airPlaneObj.layer = LayerMask.NameToLayer("UI");


        //��������
        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumeObjs[i].SetActive(i < info.volume);
        }
    }

    private void DestorObj()
    {
        if (airPlaneObj != null)
        {
            Destroy(airPlaneObj);
            airPlaneObj = null;
        }
    }

    private float time;
    //����Ƿ�ѡ��
    private bool isSel;
    void Update()
    {
        //�÷ɻ����¸���
        time += Time.deltaTime;
        heroPos.Translate(Vector3.up * Mathf.Sin(time) * 0.0001f, Space.World);

        //���߼�� �÷ɻ� ���� ����ת��
        if (Input.GetMouseButtonDown(0))
        {
            //���������� UI����ײ�� ��Ϊ��Ҫ �Ϸɻ�
            if (Physics.Raycast(UICamera.ScreenPointToRay(Input.mousePosition),
                                1000, 1 << LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
        }
        //̧��ȡ����ת
        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }
        //��ת����
        if (Input.GetMouseButton(0) && isSel)
        {
            heroPos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -20, Vector3.up);
        }
    }
}
