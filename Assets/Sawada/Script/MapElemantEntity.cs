using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapElemantEntity
{
    [Tooltip("�X�e�[�W���x��")]
    public int StageLevel = 0;
    [Tooltip("�G�u�Q�Ă�G�v�̑���")]
    public int SleeperValue = 0;
    [Tooltip("�G�u��������v�̑���")]
    public int NakaiValue = 0;
    [Tooltip("�������Ă���Ƃ�p�ӂ��邩")]
    public bool IsSynthesisHouse;
    [Tooltip("�ʏ�̉Ƃ̐�")]
    public int HouseValue;
    [Tooltip("���艖������Ƃ̐�")]
    public int HouseValueOnSolt;
    [Tooltip("�Ԃ���񂪂���Ƃ̐�")]
    public int HouseValueInBaby;
    [Tooltip("�j�������Ƃ̐�")]
    public int HouseValueInArrow;
}



