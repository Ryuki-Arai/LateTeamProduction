using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
public class MapData
{
    [Tooltip("CSV�̃f�[�^�����J���邽�߂̕ϐ�")]
    public int[][] data = null;
    /// <summary>
    /// ���̃R���X�g���N�^��CSV�̃f�[�^��ǂݎ��
    /// CSV���Ńf�[�^�̐��������Ă���̂�0��ڂ͔j������
    /// �f�[�^��int�̑��d�z��Ō��J����
    /// <param name="filePath">�t�@�C���̃p�X</param>
    public MapData(string filePath)
    {
        if (!File.Exists(filePath))//�p�X�̐�ɊY���t�@�C�����Ȃ��ꍇ�ɃG���[���O
        {
            Debug.LogError("!Warning! Your security clearance is not allowed to view this file.");
            return;
        }
        StreamReader reader = new StreamReader(filePath);
        string rows2Bdiscarded = reader.ReadLine(); //��s�ڔj�� 
        while(!reader.EndOfStream)
        {
            int i = 0;
            var line = reader.ReadLine().Split(',').Select(x=>int.Parse(x)).ToArray(); //��s���Ƃɓǂݍ���
            data[i]= line;
            i++;
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