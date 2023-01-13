using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HouseOnDevilArrow : HouseBase
{
    [Tooltip("�v���C���[���q���ɖ߂鎞��")]
    float _cancellationTime = 5f;
    [Tooltip("�J�E���g�_�E��")]
    float _time = 0;

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        _houseType = HouseType.DevilArrow;
        //�J�E���g�_�E���I�����Ɏq���ɂ���
        if (IsCountUp() && player.AdultState)
        {
            player.ModeChange(false);
        }
    }
    public override void PlayerExitHouseMotion(PlayerController player)
    {
        base.PlayerExitHouseMotion(player);
        ResetTimer();
    }

    //�J�E���g�_�E��
    public bool IsCountUp()
    {
        _time += Time.deltaTime;
        if (_time < _cancellationTime)
        {
            return false;
        }
        return true;
    }

    //�J�E���g���Z�b�g
    public void ResetTimer()
    {
        _time = 0;
    }
}
