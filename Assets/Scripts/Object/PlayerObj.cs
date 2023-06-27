using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private static PlayerObj instance;

    public static PlayerObj Instance => instance;

 

    //Ѫ��
    public int nowHp;
    public int maxHp;

    //�ٶ�
    public float speed;
    //��ת�ٶ�
    public int roundSpeed;
    //Ŀ����Ԫ�ؽǶ�
    private Quaternion targetQ;
    //�Ƿ�����
    public bool isDead;
    //��ǰ��������ת��Ļ�ϵĵ�
    private Vector3 nowPos;
    //��һ�����λ��ǰ��λ�� 
    private Vector3 frontPos;

    private void Awake()
    {
        instance = this;
    }
    public void Dead()
    {
        isDead = true;
        //��ʾ��Ϸ�������
        GameOverPanel.Instance.ShowMe();
    }
    public void Wound()
    {
        if (isDead)
        {
            return;
        }
        this.nowHp -= 1;
        //������Ϸ����ϵ�Ѫ����ʾ
        GamePanel.Instance.ChangeHp(nowHp);
        if (nowHp <= 0)
        {
            Dead();
        }
    }

    private float hValue;
    private float vValue;
    private void Update()
    {
        if (isDead)
        {
            return;
        }

        //�ƶ� ��ת�߼�

        //��ת
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");

        //���û��AD�� ��ô�Ƕ�Ϊ0
        if (hValue == 0)
        {
            targetQ = Quaternion.identity;
        }
        //�����AD�� ����
        else
        {
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        }
        //�÷ɻ����� Ŀ����Ԫ�� ȥ��ת
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, roundSpeed * Time.deltaTime);

        //��λ��֮ǰ��¼λ��
        frontPos = this.transform.position;

        //�ƶ�
        this.transform.Translate(Vector3.forward * vValue * speed * Time.deltaTime);
        this.transform.Translate(Vector3.right * hValue * speed * Time.deltaTime, Space.World);
    
        //���м����ж�
        nowPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //��������ж�
        if (nowPos.x <= 0 || nowPos.x >= Screen.width)
        {
            this.transform.position = new Vector3(frontPos.x, this.transform.position.y, this.transform.position.z);

        }
        //��������ж�
        if (nowPos.y <= 0 || nowPos.y >= Screen.height)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, frontPos.z);
        }

        //���߼�� ���ڵ���ӵ�����
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            //ֻ����ӵ���
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer("Bullet")))
            {
                BulletObj bulletObj = hitInfo.transform.GetComponent<BulletObj>();
                bulletObj.Dead();
            }
        }
    }


}
