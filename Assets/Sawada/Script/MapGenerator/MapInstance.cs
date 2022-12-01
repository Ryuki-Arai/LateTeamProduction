using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    [SerializeField, Tooltip("�}�b�v�f�[�^")]
    MapElemant _entity = null;
    [SerializeField, Tooltip("���݂̃��x��")]
    int _currentMapLevel = 0;
    [SerializeField, Tooltip("��������Transform")]
    GameObject _insPosParent = null;

    [Tooltip("Transform�������_���Ŏw�肷��ׂ̕ϐ�")]
    System.Random _random = new System.Random();
    [Tooltip("��������Transform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("�Ƃ̃v���n�u")]
    HouseBase[] _houseBases = null;

    public MapElemant Entity => _entity;

    void Start()
    {
        if (_entity != null)
        {
            SetValue();
            
        }
    }

    public void SetValue()
    {
        //�v���n�u�f�[�^���擾
        _houseBases = Resources.LoadAll<HouseBase>("HousePrefab");

        int[] houseTypeValue = new int[4] { _entity.MapTable[_currentMapLevel].houseValue
                                       , _entity.MapTable[_currentMapLevel].houseValueOnSolt
                                       , _entity.MapTable[_currentMapLevel].houseValueInBaby
                                       , _entity.MapTable[_currentMapLevel].houseValueInArrow };
        int houseValueSum = houseTypeValue.Sum();

        _insPos = GetComponentsInChildren<Transform>().Where(x => x.tag == "SpawnPos").Select(x => new SpawnPosState(x)).ToArray();
        for(int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        {
            for (int i = 0; i < houseValueSum; i++)
            {
                if (_entity.MapTable[_currentMapLevel].isSynthesisHouse)
                {

                }
                else
                {
                    CreateHouse((HouseType)i, houseTypeValue[i]);
                }
            }
        }
    }

    HouseBase[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBase[] houses = new HouseBase[targetHouseValue];
        HouseBase housePrefab = _houseBases[(int)type1];
        houses.ToList().ForEach(x => x = Instantiate(housePrefab));
        return houses;
    }

    HouseBase[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        HouseBase[] houses = new HouseBase[targetHouseValue];
        HouseBase insHouse = (HouseBase)_houseBases.Where(x => x.Type == type1 && x.Type == type2);
        houses.ToList().ForEach(x => Instantiate(insHouse));
        return houses;
    }

    /// <summary>
    /// �Q�[����̍��W���烉���_���ɑI�����Ƃ��Z�b�g����֐�
    /// </summary>
    /// <param name="house">�Z�b�g����Ƃ̃v���n�u</param>
    void SetHouse(HouseBase house)
    {
        SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
        SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];
        house.transform.position = targetPos.SpawnPos.position;
        targetPos.State = SpawnPosState.SpawnState.used;
    }
}

public class SpawnPosState
{
    public Transform SpawnPos;
    public SpawnState State;
    public SpawnPosState(Transform spawnPos)
    {
        SpawnPos = spawnPos;
        State = SpawnState.none;
    }
    public enum SpawnState
    {
        none = 0,
        used = 1
    }
}

