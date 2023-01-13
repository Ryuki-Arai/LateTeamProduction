using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IsGame;

//�}�b�v�̎��������N���X
public class MapInstance : MonoBehaviour
{
    [SerializeField, Header("CSV�t�@�C���̃p�X")]
    string _filePath = "MapElemant";
    [SerializeField, Header("���݂̃��x��")]
    int _currentMapLevel = 0;
    [SerializeField, Header("��������Transform")]
    Transform[] _insTrans = null;

    [Tooltip("�Ƃ̑���")]
    int[] _houseTypeValue = null;
    [Tooltip("���̑���")]
    int _allPillowValue = 0;
    [Tooltip("Transform�������_���Ŏw�肷��ׂ̕ϐ�")]
    System.Random _random = new System.Random();
    [Tooltip("�}�b�v�f�[�^")]
    MapData _mapData = null;

    SpawnPosState[] _insPos = null;
    [Tooltip("�Ƃ̃v���n�u")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("�Ƃ̃f�[�^�̔z��")]
    HouseBase[] _houseDatas = new HouseBase[]�@//�Ƃ̋��������߂�N���X�̔z��
                {
                    new HouseBase(),
                    new HouseInBaby(),
                    new HouseOnSolt(),
                    new HouseOnDevilArrow()
                };


    /// <summary>
    /// �Ƃ̑���
    /// </summary>
    public int AllHouseValue => _houseTypeValue.Sum();
    /// <summary>
    /// ���̑���
    /// </summary>
    public int AllPillowValue => _allPillowValue;


    void Awake()
    {
        _mapData = new MapData(_filePath);�@//CSV�f�[�^�̓ǂݍ���
        _allPillowValue = _mapData.data[_currentMapLevel][0]; //���̑�����������
        GameManager.Instance.SleepingEnemy = _allPillowValue; //�Q�[���}�l�[�W���[�ɖ��̓G�̑�������
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").ToArray();�@//�Ƃ̃v���n�u�f�[�^�̎擾
        _insPos = _insTrans.Select(x => new SpawnPosState(x)).ToArray(); //�Ƃ̏o���n�_���擾

        SetStage();
    }

    /// <summary>
    /// �X�e�[�W��������������֐�
    /// </summary>
    public void SetStage()
    {
        //�p�ӂ���Ƃ̐���z��ňꎞ�ۑ�
        _houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.Baby + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.Solt + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType + 2]};

        //�Q�[���ɑ��݂���Ƃ̎�ނ̐������񂵁A���ꂼ��w�肳�ꂽ��������������
        for (int houseTypes = 0; houseTypes < _houseTypeValue.Length; houseTypes++)
        {
            if ((_houseTypeValue[houseTypes]) <= 0) continue;//�������鐔���O�������ꍇ��΂�

            HouseBehaviour[] houses = new HouseBehaviour[_houseTypeValue[houseTypes]];
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
            _houseTypeValue[houseTypes] = unUsePosValue.Length < _houseTypeValue[houseTypes]
                                        ? unUsePosValue.Length : _houseTypeValue[houseTypes];
            switch ((HouseType)houseTypes)
            {
                case HouseType.DoubleType:
                    houses = CreateHouse(HouseType.Solt, HouseType.DevilArrow, _houseTypeValue[houseTypes]); //��ȏ�̗v�f������Ƃ𐶐�
                    break;
                default:
                    houses = CreateHouse((HouseType)houseTypes, _houseTypeValue[houseTypes]); //��ȏ�̗v�f������Ƃ𐶐�
                    break;
            }
            int remainPos = SetHouse(houses);
            if (remainPos <= 0) break;
        }
        _currentMapLevel++;
    }
    /// <summary>
    /// ��������̉ƃI�u�W�F�N�g�𐶐����邽�߂̊֐�
    /// </summary>
    /// <param name="type1">�Ƃ̑���</param>
    /// <param name="targetHouseValue">�p�ӂ���Ƃ̐�</param>
    /// <returns>�ݒu�����v�f1�̉Ƃ̔z��</returns>
    HouseBehaviour[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(_houseBases[0]);
            _allPillowValue = houses[i].CreateHouseObject(this, _houseDatas[(int)type1]);
        }
        return houses;
    }
    /// <summary>
    /// ��������̉ƃI�u�W�F�N�g�𐶐����邽�߂̊֐�
    /// </summary>
    /// <param name="type1">�Ƃ̑���1</param>
    /// <param name="type2">�Ƃ̑���2</param>
    /// <param name="targetHouseValue">�p�ӂ���Ƃ̐�</param>
    /// <returns>�ݒu�����v�f2�̉Ƃ̔z��</returns>
    DoubleHouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        DoubleHouseBehaviour[] houses = new DoubleHouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate((DoubleHouseBehaviour)_houseBases[1]);
            houses[i].CreateHouseObject(this, _houseDatas[(int)type1], _houseDatas[(int)type2]);
        }
        return houses;
    }
    /// <summary>
    /// �Q�[����̍��W���烉���_���ɑI�����Ƃ��Z�b�g����֐�
    /// </summary>
    /// <param name="house">�Z�b�g����Ƃ̃v���n�u</param>
    int SetHouse(HouseBehaviour[] houses)
    {
        SpawnPosState[] unUsePosValue = new SpawnPosState[0];
        for (int i = 0; i < houses.Length; i++)
        {
            unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();�@//�g���Ă��Ȃ����W��z��Ŏ擾
            if (unUsePosValue.Length > 0)
            {
                SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)]; //�z��̒����烉���_���ɗv�f���擾
                houses[i].transform.position = targetPos.spawnPos.position;
                houses[i].transform.rotation = targetPos.spawnPos.rotation;
                houses[i].Activate();
                targetPos.State = SpawnPosState.SpawnState.used; //�g��ꂽ�v�f�̏�Ԃ��X�V����

            }
        }
        return unUsePosValue.Length;
    }

    /// <summary>
    /// �Q�[���I�����ɒl�����Z�b�g����֐�(�V�[����J�ڂ���ꍇ�͌Ă΂Ȃ��Ă���)
    /// </summary>
    public void ResetValue()
    {
        _houseTypeValue = null;
        Array.ForEach(_insPos, x => x.State = SpawnPosState.SpawnState.none);
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

