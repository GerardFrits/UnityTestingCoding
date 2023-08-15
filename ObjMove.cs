using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEaseType : byte
{
    NULL,
    EaseIn,       // 缓入
    EaseOut,      // 缓出
    EaseInOut,    // 缓入缓出
}

public class ObjMove : MonoBehaviour
{

    // 移动对象
    private GameObject movingObj;
    // 移动起点
    private Vector3 beginPos;
    // 终点
    private Vector3 endPos;
    // 单程时间
    private float moveTime;
    // 计时器
    private float movingTimer;
    // 是否循环
    private bool isPingpong;
    // 是否正向移动
    private bool isForward;
    // 是否移动中
    private bool isMoving;
    // 缓动枚举
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
