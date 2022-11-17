using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HouseOnSolt : HouseBase
{
    [Tooltip("�h�A�̃I�u�W�F�N�gRenderer�̔z��")]
    Renderer[] _doorRenderers = null;
    [Tooltip("�h�A�̃I�u�W�F�N�g��Collider�̔z��")]
    Collider2D[] _doorColliers = null;

    public override void Init()
    {
        base.Init();
        //�擾
        _doorColliers = gameObject.GetComponentsInChildren<Collider2D>().Where(x => x.tag == "Door").ToArray();
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
        //ToDo:�v���C���[�̑�l������v���p�e�B�Ō��J���Ă��炤�B
        foreach (Collider2D col in _doorColliers)
        {
            col.enabled = player.AdultState;
        }
        foreach (Renderer ren in _doorRenderers)
        {
            ren.enabled = player.AdultState;
        }
    }

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        //if(!player.AdultState)
        //{
        //    �����ɃQ�[���I�[�o�[�̏���������
        //}
    }
}
