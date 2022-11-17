using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// �������Ǘ�����R���|�[�l���g
/// �֐���Public�ɂȂ��Ă�̂�UnityEvent�ŌĂԂ��߂�
/// �C�x���g�ݒ莞�͊֐��̈����̌^��CriAtomSource�ɂȂ��Ă�̂�I�ׁi�W�F�l���b�N�j
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("�J�n���ɌĂ΂��ׂ�����"),  SerializeField] 
    UnityEvent<CriAtomSource> _onGameStart;
    [Header("�Q�[���I�[�o�[���ɌĂ΂��ׂ�����"),  SerializeField] 
    UnityEvent<CriAtomSource> _onGameOver;
    [Header("�N���A���ɌĂ΂��ׂ�����"),SerializeField] 
    UnityEvent<CriAtomSource> _onGameClear;
    public void GameStart(CriAtomSource source)
    {
        _onGameStart.Invoke(source);
    }

    /// <summary>
    /// ���̊֐��̓{�^���Ȃǂ���Ă΂Ȃ��B���ډ������w�肵�ēn���Ȃ����炾
    /// Event����ĉ����w�肷�邱�ƂŃ{�^���Ȃǂ���ł��Đ��ł���
    /// </summary>
    /// <param name="source"></param>
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
