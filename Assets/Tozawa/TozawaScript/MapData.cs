using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
public class MapData
{
    [Tooltip("CSV�̃f�[�^�����J���邽�߂̕ϐ�")]
    public List<int[]> data = new List<int[]>();
    /// <summary>
    /// ���̃R���X�g���N�^��CSV�̃f�[�^��ǂݎ��
    /// CSV���Ńf�[�^�̐��������Ă���̂�0��ڂ͔j������
    /// �f�[�^��int�̑��d�z��Ō��J����
    /// <param name="filePath">�t�@�C���̃p�X</param>
    public MapData(string filePath)
    {
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (!file)//�p�X�̐�ɊY���t�@�C�����Ȃ��ꍇ�ɃG���[���O
        {
            Debug.LogError("!Warning! Your security clearance is not allowed to view this file.");
            return;
        }
        StringReader reader = new StringReader(file.text);
        reader.ReadLine(); //��s�ڔj�� 
        string rows2Bdiscarded;
        while((rows2Bdiscarded = reader.ReadLine()) != null)
        {
            var line = rows2Bdiscarded.Split(',').Select(x=>int.Parse(x)).ToArray(); //��s���Ƃɓǂݍ���
            data.Add(line);
        }
    }
    public enum MapElemantEntity
    {
        sleeperValue = 0,
        nakaiValue = 1,
        houseValue = 2,
        houseValueOnSolt = 3,
        houseValueInBaby = 4,
        houseValueInArrow = 5,
        houseValueDoubleType = 6,
    }
}