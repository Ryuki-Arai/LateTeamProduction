using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// �������Ǘ�����R���|�[�l���g
/// �֐���Public�ɂȂ��Ă�̂�UnityEvent�ŌĂԂ��߂�
/// �C�x���g�ݒ莞�͊֐��͈����̌^��CriAtomSource�ɂȂ��Ă�̂�I�ׁi�W�F�l���b�N�j
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("�J�n���ɌĂ΂��ׂ�����"),  SerializeField] 
    UnityEvent _onGameStart;
    [Header("�Q�[���I�[�o�[���ɌĂ΂��ׂ�����"),  SerializeField] 
    UnityEvent _onGameOver;
    [Header("�N���A���ɌĂ΂��ׂ�����"),SerializeField] 
    UnityEvent _onGameClear;
    /// <summary>
    /// �Q�[���J�n�̎��Ɉ��Ă�ł�������
    /// </summary>
    public void GameStart()
    {
        _onGameStart.Invoke();
    }

    /// <summary>
    /// �Q�[���I�[�o�[�̎��Ɉ��Ă�ł�������
    /// </summary>
    public void GameOver()
    {
        _onGameOver.Invoke();
    }

    /// <summary>
    /// �Q�[���N���A�̎��Ɉ��Ă�ł�������
    /// </summary>
    public void GameClear()
    {
        _onGameClear.Invoke();
    }

    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
