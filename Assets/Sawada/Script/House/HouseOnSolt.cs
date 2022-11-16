using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HouseOnSolt : HouseBase
{
    [Tooltip("�h�A�̃I�u�W�F�N�g�̂̔z��")]
    GameObject[] _doorObjects = null;
    Renderer[] _doorRenderers = null;
    Collider2D[] _doorColliers = null;

    public override void Init()
    {
        base.Init();
        //�擾
        _doorObjects = GetComponentsInChildren<GameObject>().Where(x => x.tag == "Door").ToArray();
        for(int i = 0; i < _doorObjects.Length; i++)
        {
            _doorRenderers[i] = _doorObjects[i].GetComponent<Renderer>();
            _doorColliers[i] = _doorColliers[i].GetComponent<Collider2D>();
        }
    }

    public override void HouseEntryMotion(PlayerController player)
    {
        base.HouseEntryMotion(player);
        //�v���C���[�̏󋵂ɉ����ăh�A���J����
        //ToDo:�v���C���[�̑�l������v���p�e�B�Ō��J���Ă��炤�B
        //foreach (Collider2D col in _doorColliers)
        //{
        //    col.enabled = player.AdaltState;
        //}
        //foreach (Renderer ren in _doorRenderers)
        //{
        //    ren.enabled = player.AdaltState;
        //}
    }
}
