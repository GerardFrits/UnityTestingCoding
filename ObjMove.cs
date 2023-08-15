using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEaseType : byte
{
    NULL,
    EaseIn,       // ����
    EaseOut,      // ����
    EaseInOut,    // ���뻺��
}

public class ObjMove : MonoBehaviour
{

    // �ƶ�����
    private GameObject movingObj;
    // �ƶ����
    private Vector3 beginPos;
    // �յ�
    private Vector3 endPos;
    // ����ʱ��
    private float moveTime;
    // ��ʱ��
    private float movingTimer;
    // �Ƿ�ѭ��
    private bool isPingpong;
    // �Ƿ������ƶ�
    private bool isForward;
    // �Ƿ��ƶ���
    private bool isMoving;
    // ����ö��
    private EEaseType movingEase;

    public void move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, EEaseType easeType = EEaseType.NULL)
    {
		if (gameObject == null)
		{
            isMoving = false;
            return;
		}
		if (time <= 0)
		{
            gameObject.transform.position = end;
            return;
		}
        movingObj = gameObject;
        beginPos = begin;
        endPos = end;
        moveTime = time;
        isPingpong = pingpong;

        movingTimer = 0.0f;
        isMoving = true;
        isForward = true;

        movingEase = easeType;
    }

    void Update()
    {
		if (isMoving)
		{
            movingTimer += Time.deltaTime;
			if (movingTimer >= moveTime)
			{
				if (isPingpong)
				{
                    Vector3 tmp = beginPos;
                    beginPos = endPos;
                    endPos = tmp;
                    movingTimer -= moveTime;
                    isForward = !isForward;
				}
				else
				{
                    isMoving = false;
				}
			}

            float t = movingTimer / moveTime;
			if (movingEase == EEaseType.EaseIn)
			{
                t = Mathf.Lerp(0, 1, t * t);
			} 
            else if (movingEase == EEaseType.EaseOut)
			{
                t = Mathf.Lerp(0, 1, Mathf.Sqrt(t));
			}
            else if (movingEase == EEaseType.EaseInOut)
			{
				if (t < 0.5f)
				{
                    t = Mathf.Lerp(0, 1, t * t);
                }
				else
				{
                    t = Mathf.Lerp(0, 1, Mathf.Sqrt(t));
                }
			}
            movingObj.transform.position = Vector3.Lerp(beginPos, endPos, t);
		}
    }
}
