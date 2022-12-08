using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̃��x��")]
    string _filePath = null;
    [SerializeField, Tooltip("���݂̃��x��")]
    int _currentMapLevel = 0;

    [Tooltip("Transform�������_���Ŏw�肷��ׂ̕ϐ�")]
    System.Random _random = new System.Random();
    [Tooltip("�}�b�v�f�[�^")]
    MapData _mapData = null;
    [Tooltip("GameManager���i�[����ϐ�")]
    GameManager _gameManager = null;
    [Tooltip("��������Transform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("�Ƃ̃v���n�u")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("�Ƃ̃f�[�^�̔z��")]
    HouseBase[] _houseDatas = null;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _mapData = new MapData(_filePath);
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").OrderBy(x => x.Type).ToArray();
    }

    public void SetStage()
    {
        int[] houseTypeValue = null;
        //�p�ӂ���Ƃ̑������J�E���g
        houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Baby]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Solt]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType]};
        int houseValueSum = houseTypeValue.Sum();

        _insPos = GetComponentsInChildren<Transform>().Where(x => x.tag == "SpawnPos").Select(x => new SpawnPosState(x)).ToArray();
        for (int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        {
            if((houseTypeValue[houseTypes]) <= 0) return;
            HouseBehaviour[] houses = CreateHouse((HouseType)houseTypes, houseTypeValue[houseTypes]);
            SetHouse(houses);
        }
        _currentMapLevel++;
    }
    /// <summary>
    /// ��������̉ƃI�u�W�F�N�g�𐶐����邽�߂̊֐�
    /// </summary>
    /// <param name="type1">�Ƃ̑���</param>
    /// <param name="targetHouseValue">�p�ӂ���Ƃ̐�</param>
    /// <returns></returns>
    HouseBehaviour[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        HouseBehaviour housePrefab = _houseBases[(int)type1];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(housePrefab);
            houses[i].CreateObject();
            houses[i].HouseData.SetValue(_gameManager);
        }
        return houses;
    }
    /// <summary>
    /// ��������̉ƃI�u�W�F�N�g�𐶐����邽�߂̊֐�
    /// </summary>
    /// <param name="type1">�Ƃ̑���1</param>
    /// <param name="type2">�Ƃ̑���2</param>
    /// <param name="targetHouseValue">�p�ӂ���Ƃ̐�</param>
    /// <returns></returns>
    HouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        HouseBehaviour housePrefab = (HouseBehaviour)_houseBases.Where(x => x.Type == type1 && x.Type == type2);
        if (housePrefab == null) return null;
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(housePrefab);
        }
        return houses;
    }
    /// <summary>
    /// �Q�[����̍��W���烉���_���ɑI�����Ƃ��Z�b�g����֐�
    /// </summary>
    /// <param name="house">�Z�b�g����Ƃ̃v���n�u</param>
    void SetHouse(HouseBehaviour[] houses)
    {
        for (int i = 0; i < houses.Length; i++)
        {
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
            SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];
            houses[i].transform.position = targetPos.spawnPos.position;
            houses[i].transform.rotation = targetPos.spawnPos.rotation;
            targetPos.State = SpawnPosState.SpawnState.used;
        }
    }
}

//�Ƃ��Z�b�g������W�̏��
public class SpawnPosState
{
    public Transform spawnPos;
    public SpawnState State;
    public SpawnPosState(Transform position)
    {
        this.spawnPos = position;
        State = SpawnState.none;
    }
    public enum SpawnState
    {
        none = 0,
        used = 1
    }
}

