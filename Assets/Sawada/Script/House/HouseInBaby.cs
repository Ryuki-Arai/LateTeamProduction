using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    [SerializeField,Tooltip("���̋N���鎞��")] 
    float _getUpTime = 0f;


    public override void Init()
    {
        base.Init();
        _type = HouseType.Baby;
        //���g�̉Ƃ̒�����ɖ��̋N���鎞�Ԃ̐ݒ�
        _returnPillows.ToList().ForEach(x => x.GetUpTime(_getUpTime));
    }
}
