using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// �V�[����̃T�E���h�}�l�[�W���[���Q�Ƃ��āI
/// �������Ǘ�����R���|�[�l���g
/// �֐���Public�ɂȂ��Ă�̂�UnityEvent�ŌĂԂ��߂�
/// �C�x���g�ݒ莞�͊֐��͈����̌^��CriAtomSource�ɂȂ��Ă�̂�I�ׁi�W�F�l���b�N�j
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("�J�n���ɌĂ΂��ׂ�����"),  SerializeField,Tooltip("�J�n���̏������i�[���ꂽUnityEvent")] 
    UnityEvent _onGameStart;
    [Header("�Q�[���I�[�o�[���ɌĂ΂��ׂ�����"),  SerializeField, Tooltip("�Q�[���I�[�o�[���̏������i�[���ꂽUnityEvent")] 
    UnityEvent _onGameOver;
    [Header("�N���A���ɌĂ΂��ׂ�����"),SerializeField, Tooltip("�N���A���̏������i�[���ꂽUnityEvent")] 
    UnityEvent _onGameClear;
    [Header("����Ԃ����ۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("����Ԃ������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onMakuraReverse;
    [Header("�L�����Z���{�^�����������ۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("�L�����Z���{�^�������������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onCanceled;
    [Header("�|�[�Y�{�^�����������ۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("�|�[�Y�{�^�����������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onPaused;
    [Header("����{�^�����������ۂɌĂ΂��ׂ�����"), SerializeField,Tooltip("����{�^�������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onDecited;
    [Header("�|���������ۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("�|�������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onKakejikued;
    [Header("�J�b�g�C�������ۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("�J�b�g�C���������̏������i�[���ꂽUnityEvent")]
    UnityEvent _onCutined;
    [Header("�Q�Ă�l�ڋߍۂɌĂ΂��ׂ�����"), SerializeField, Tooltip("�Q�Ă�l�ڋߎ��̏������i�[���ꂽUnityEvent")]
    UnityEvent _onSleepNear;
    [Header("�Q�Ă�l�ڔ�ڋ߂ɌĂ΂��ׂ�����"), SerializeField, Tooltip("�Q�Ă�l��ڋߎ��̏������i�[���ꂽUnityEvent")]
    UnityEvent _onSleepFar;
    [Header("�������ꂽ���ɌĂ΂��ׂ�����"), SerializeField, Tooltip("�������ꂽ���̏������i�[���ꂽUnityEvent")]
    UnityEvent _onDiscovered;
    [Header("�Q�[�W�㏸���ɌĂ΂��ׂ�����"), SerializeField, Tooltip("�Q�[�W�㏸���̏������i�[���ꂽUnityEvent")]
    UnityEvent _onGauging;
    [Header("�Q�[�W�㏸��~���ɌĂ΂��ׂ�����"), SerializeField, Tooltip("�Q�[�W�㏸��~���̏������i�[���ꂽUnityEvent")]
    UnityEvent _onGaugeStop;
    private void Start()
    {
        GameStart();
    }
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

    /// <summary>
    /// ����Ԃ����Ƃ��Ɏ��Ɉ��Ă�ł�������
    /// </summary>
    public void MakuraReverse()
    {
        _onMakuraReverse.Invoke();
    }

    /// <summary>
    /// �L�����Z���������Ɉ��Ă�ł�������
    /// </summary>
    public void Canceled()
    {
        _onCanceled.Invoke();
    }

    /// <summary>
    /// �|�[�Y�������Ɉ��Ă�ł�������
    /// </summary>
    public void Paused()
    {
        _onPaused.Invoke();
    }
    /// <summary>
    /// ���肵�����Ɉ��Ă�ł�������
    /// </summary>
    public void Decited()
    {
        _onPaused.Invoke();
    }

    /// <summary>
    /// �|�����������Ɉ��Ă�ł�������
    /// </summary>
    public void Kakejikued()
    {
        _onKakejikued.Invoke();
    }
    /// <summary>
    /// �J�b�g�C���������Ɉ��Ă�ł�������
    /// </summary>
    public void Cutin()
    {
        _onCutined.Invoke();
    }
    /// <summary>
    /// �Q�Ă�z�̋߂����鎞�H�Ɉ��Ă�ł�������
    /// </summary>
    public void SleepingVoice()
    {
        _onSleepNear.Invoke();
    }
    /// <summary>
    /// �Q�Ă�z�̋߂����痣�ꂽ���E�������Ɉ��Ă�ł�������
    /// </summary>
    public void KillSleeping()
    {
        _onSleepFar.Invoke();
    }
    /// <summary>
    /// �������ꂽ���Ɉ��Ă�ł�������
    /// </summary>
    public void Discoverd()
    {
        _onDiscovered.Invoke();
    }
    /// <summary>
    /// �Q�[�W�J�n���Ɉ��Ă�ł�������
    /// </summary>
    public void Gauging()
    {
        _onGauging.Invoke();
    }
    /// <summary>
    /// �Q�[�W��~���Ɉ��Ă�ł�������
    /// </summary>
    public void GaugeStop()
    {
        _onGaugeStop.Invoke();
    }
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
