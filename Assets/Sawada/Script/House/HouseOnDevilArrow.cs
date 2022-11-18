using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOnDevilArrow : HouseBase
{
    [SerializeField, Tooltip("�v���C���[���q���ɖ߂鎞��")]
    float _cancellationTime = 0f;
    [SerializeField, Tooltip("�J�E���g�_�E��")]
    float _time = 0;

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            PlayerInHouseMotion(player);
        }
    }

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        //�J�E���g�_�E���I�����Ɏq���ɂ���
        if (IsCountUp() && player.AdultState)
        {
            player.ModeChange(false);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
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

    //�J�E���g�[���Z�b�g
    public void ResetTimer()
    {
        _time = 0;
    }
}
