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
        //���g�̉Ƃ̒�����ɖ��̋N���鎞�Ԃ̐ݒ�
        //ToDo:Player���ŋN���鎞�Ԃ�ݒ肷��֐���������Public�Ō��J���Ă��炤
        //_returnPillows.Select(x => x.SetGetUpTime(_getUpTime));
    }
}
