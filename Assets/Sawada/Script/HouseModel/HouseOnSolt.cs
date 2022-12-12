using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public class HouseOnSolt : HouseBase
{
    [Tooltip("�h�A�̃I�u�W�F�N�gRenderer�̔z��")]
    Renderer[] _doorRenderers = null;
    [Tooltip("�h�A�̃I�u�W�F�N�g��Collider�̔z��")]
    Collider2D[] _doorColliers = null;

    public override void Init<T>(T house)
    {
        base.Init(house);
        //�擾(�h�A�̃I�u�W�F�N�g�ɂ́hDoor�h�Ƃ����^�O��t���Ă�������)
        _doorColliers = house.ColidersInHouse.Where(x => x.tag == "Door").ToArray();
        _doorRenderers = new Renderer[_doorColliers.Length];
        for (int i = 0; i < _doorColliers.Length; i++)
        {
            _doorRenderers[i] = _doorColliers[i].GetComponent<Renderer>();
        }
    }
    public override void PlayerEntryHouseMotion(PlayerController player)
    {
        base.PlayerEntryHouseMotion(player);
        //�v���C���[�̏󋵂ɉ����ăh�A���J����
        Array.ForEach(_doorColliers, x => x.enabled = player.AdultState);
        Array.ForEach(_doorRenderers, x => x.enabled = player.AdultState);
    }
    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        if (!player.AdultState)
        {
            
        }
    }
}
