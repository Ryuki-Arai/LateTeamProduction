using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    public override void Init()
    {
        base.Init();
        //���g�̉Ƃ̒�����ɖ��̋N���鎞�Ԃ̐ݒ�
        _returnPillows.ToList().ForEach(x => x.GetUpTime(_getUpTime));
    }
}
