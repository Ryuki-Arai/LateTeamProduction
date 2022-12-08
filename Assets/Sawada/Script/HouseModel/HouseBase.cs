using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HouseBase
{
    [SerializeField, Tooltip("�N���鎞��")]
    protected float _getUpTime = 10f;

    [Tooltip("�Ƃ̉����S�Ă�Renderer")]
    protected Renderer[] _renderersInHouse = null;
    [Tooltip("�Ƃ̉����S�Ă�collider")]
    protected Collider2D[] _colliders = null;
    [Tooltip("GameManager���i�[����ϐ�")]
    protected GameManager _gameManager = null;
    [Tooltip("�Ƃ̒��ɂ��閍�̐�")]
    protected Returnpillow[] _returnPillows = null;
    [Tooltip("�|����")]
    protected HangingScroll _hangingScroll = null;


    public virtual void Init() { }
    public virtual void PlayerEntryHouseMotion(PlayerController player) { }
    public virtual void PlayerInHouseMotion(PlayerController player) { }
    public virtual void PlayerExitHouseMotion(PlayerController player) { }


    public void SetValue(GameManager gameManager)
    {
        _gameManager = gameManager;
        Init();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="allPillowValue"></param>
    /// <returns></returns>
    public int SetPillow(int allPillowValue)
    {
        int pillowValue = 0;
        if (allPillowValue >= _returnPillows.Length)
        {
            pillowValue = UnityEngine.Random.Range(1, 4);
        }
        else
        {
            pillowValue = allPillowValue;
        }
        for (int i = 0; i < pillowValue; i++)
        {
            _returnPillows[i].enabled = true;
        }
        Array.ForEach(_returnPillows, x => x.GetUpTime(_getUpTime));
        allPillowValue -= pillowValue;
        return allPillowValue;
    }
    /// <summary>
    ///�@�R���|�[�l���g�L�����ɌĂԊ֐�
    /// </summary>
    
}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DevilArrow = 3,
    DoubleType = 4
}

