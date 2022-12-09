using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour,IHousePool
{
    [Tooltip("GameManager���i�[����ϐ�")]
    protected GameManager _gameManager = null;
    [Tooltip("�Ƃ̃f�[�^1")]
    protected HouseBase _data1 = null;
    [Tooltip("�|����")]
    protected HangingScroll _hangingScroll = null;
    [Tooltip("�Ƃ̉����S�Ă�Renderer")]
    protected Renderer[] _renderersInHouse = null;
    [Tooltip("�Ƃ̉����S�Ă�collider")]
    protected Collider2D[] _collidersInHouse = null;
    [Tooltip("�Ƃ̒��ɂ��閍�̐�")]
    protected Returnpillow[] _returnPillows = null;
    
    public Renderer[] RenderersInHouse => _renderersInHouse;
    public Collider2D[] ColidersInHouse => _collidersInHouse;
    public Returnpillow[] ReturnPillows => _returnPillows;

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
        Array.ForEach(_collidersInHouse, x => x.enabled = true);
        Array.ForEach(_renderersInHouse, x => x.enabled = true);
        Array.ForEach(_returnPillows, x => x.enabled = true);
    }
    /// <summary>
    /// �R���|�[�l���g��L�����ɌĂԊ֐�
    /// </summary>
    public void Desactivate()
    {
        Array.ForEach(_collidersInHouse, x => x.enabled = false);
        Array.ForEach(_renderersInHouse, x => x.enabled = false);
        Array.ForEach(_returnPillows, x => x.enabled = false);
    }

    public virtual void CreateHouseObject(HouseBase house1,GameManager gameManager)
    {
        _gameManager = gameManager;
        _renderersInHouse = GetComponentsInChildren<Renderer>();
        _collidersInHouse = GetComponentsInChildren<Collider2D>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        _hangingScroll = GetComponentInChildren<HangingScroll>();
        _data1 = house1;
        _data1.Init(this);
    }
}
