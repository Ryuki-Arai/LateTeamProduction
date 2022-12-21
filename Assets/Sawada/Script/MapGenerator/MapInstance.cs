using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IsGame;

//�}�b�v�̎��������N���X
public class MapInstance : MonoBehaviour
{
    [SerializeField, Tooltip("CSV�t�@�C���̃p�X")]
    string _filePath = null;
    [SerializeField, Tooltip("���݂̃��x��")]
    int _currentMapLevel = 0;

    [Tooltip("Transform�������_���Ŏw�肷��ׂ̕ϐ�")]
    System.Random _random = new System.Random();
    [Tooltip("�}�b�v�f�[�^")]
    MapData _mapData = null;
    [Tooltip("��������Transform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("�Ƃ̃v���n�u")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("�Ƃ̃f�[�^�̔z��")]
    HouseBase[] _houseDatas = null;


    void Start()
    {
        _mapData = new MapData(_filePath);�@//CSV�f�[�^�̓ǂݍ���
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").ToArray();�@//�Ƃ̃v���n�u�f�[�^�̎擾
        _houseDatas = new HouseBase[]�@//�Ƃ̋��������߂�N���X�̔z��𐶐�
        {
            new HouseBase(),
            new HouseInBaby(),
            new HouseOnSolt(),
            new HouseOnDevilArrow()
        };
        _insPos = GetComponentsInChildren<Transform>()�@�@//�����n�_�̎擾
            .Where(x => x.tag == "SpawnPos")
            .Select(x => new SpawnPosState(x))
            .ToArray();
        SetStage();
    }

    /// <summary>
    /// �X�e�[�W�̎�����������֐�
    /// </summary>
    public void SetStage()
    {
        //�p�ӂ���Ƃ̐���z��ňꎞ�ۑ�
        int[] houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Baby + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Solt + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType + 2]};

        //�Q�[���ɑ��݂���Ƃ̎�ނ̐������񂵁A���ꂼ��w�肳�ꂽ��������������
        for (int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        {
            HouseBehaviour[] houses = null;
            if ((houseTypeValue[houseTypes]) <= 0) continue;�@//�������鐔���O�������ꍇcontinue�Ŕ�΂�
            switch ((HouseType)houseTypes)
            {
                case HouseType.DoubleType:
                    houses = CreateHouse(HouseType.Solt, HouseType.DevilArrow, houseTypeValue[houseTypes]);�@//��ȏ�̗v�f������Ƃ𐶐�
                    break;
                default:
                    houses = CreateHouse((HouseType)houseTypes, houseTypeValue[houseTypes]);�@//��ȏ�̗v�f������Ƃ𐶐�
                    break;
            }
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
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(_houseBases[0]);
            houses[i].CreateHouseObject(_houseDatas[(int)type1]);
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
    DoubleHouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        DoubleHouseBehaviour[] houses = new DoubleHouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate((DoubleHouseBehaviour)_houseBases[1]);
            houses[i].CreateHouseObject(_houseDatas[(int)type1], _houseDatas[(int)type2]);
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
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();�@//�g���Ă��Ȃ����W��z��Ŏ擾
            SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];�@//�z��̒����烉���_���ɗv�f���擾
            houses[i].transform.position = targetPos.spawnPos.position;
            houses[i].transform.rotation = targetPos.spawnPos.rotation;
            targetPos.State = SpawnPosState.SpawnState.used; //�g��ꂽ�v�f�̏�Ԃ��X�V����
        }
    }
}

//�Ƃ��Z�b�g������W�Ƃ��̏�Ԃ�ێ�����N���X
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

