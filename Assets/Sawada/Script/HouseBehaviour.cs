using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour,IHousePool
{
    [Tooltip("GameManager���i�[����ϐ�")]
    protected GameManager _gameManager = null;
    [Tooltip("�Ƃ̃f�[�^1")]
    HouseBase _data1 = null;
    [Tooltip("�|����")]
    protected HangingScroll _hangingScroll = null;
    [Tooltip("�Ƃ̉����S�Ă�Renderer")]
    protected Renderer[] _renderersInHouse = null;
    [Tooltip("�Ƃ̉����S�Ă�collider")]
    protected Collider2D[] _colliders = null;
    [Tooltip("�Ƃ̒��ɂ��閍�̐�")]
    protected Returnpillow[] _returnPillows = null;

    public HouseBase HouseData => _data1;    

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerEntryHouseMotion(player);
        }
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerInHouseMotion(player);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerExitHouseMotion(player);
        }
    }

    public void Activate()
    {
        Array.ForEach(_colliders, x => x.enabled = true);
        Array.ForEach(_renderersInHouse, x => x.enabled = true);
        Array.ForEach(_returnPillows, x => x.enabled = true);
    }
    /// <summary>
    /// �R���|�[�l���g��L�����ɌĂԊ֐�
    /// </summary>
    public void Desactivate()
    {
        Array.ForEach(_colliders, x => x.enabled = false);
        Array.ForEach(_renderersInHouse, x => x.enabled = false);
        Array.ForEach(_returnPillows, x => x.enabled = false);
    }

    public virtual void CreateHouseObject(HouseBase house1,HouseBase house2)
    {
        _renderersInHouse = GetComponentsInChildren<Renderer>();
        _colliders = GetComponentsInChildren<Collider2D>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        _hangingScroll = GetComponentInChildren<HangingScroll>();
        _data1 = house1;
    }
}
