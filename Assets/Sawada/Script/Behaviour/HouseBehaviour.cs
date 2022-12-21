using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseBehaviour : MonoBehaviour,IHousePool
{
    [SerializeField,Tooltip("�|����")]
    protected HangingScroll _hangingScroll = null;
    [SerializeField, Tooltip("�Ƃ̉����S�Ă�Renderer")]
    protected Renderer[] _renderersInHouse = null;
    [SerializeField, Tooltip("�Ƃ̉����S�Ă�collider")]
    protected Collider2D[] _collidersInHouse = null;
    [SerializeField, Tooltip("�Ƃ̒��ɂ��閍�̐�")]
    protected Returnpillow[] _returnPillows = null;

    [Tooltip("�v���C���[���i�[����ϐ�")]
    protected PlayerController _playerController;
    [Tooltip("�Ƃ̃f�[�^1")]
    protected HouseBase _data1 = null;
    
    public Collider2D[] ColidersInHouse => _collidersInHouse;
    public Returnpillow[] ReturnPillows => _returnPillows;

    private void Start()
    {
        //CreateHouseObject(new HouseBase());
    }

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

    /// <summary>
    /// �R���|�[�l���g�L�����ɌĂԊ֐�
    /// </summary>
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
    /// <summary>
    /// �R���|�[�l���g�������ɌĂԊ֐�
    /// </summary>
    /// <param name="house1"></param>
    public virtual void CreateHouseObject(HouseBase house1)
    {
        _hangingScroll.Init();
        _data1 = house1;
        _data1.Init(this);
    }
}
