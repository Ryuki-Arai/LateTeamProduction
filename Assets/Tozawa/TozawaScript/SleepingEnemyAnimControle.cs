using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Q�Ă���G�̖{�̂ƃ��A�N�V�����i�@�񓔂Ƃ��j�̃A�j���[�V�������Ǘ�����R���|�[�l���g
/// </summary>
public class SleepingEnemyAnimControle : MonoBehaviour
{
    [SerializeField, Tooltip("�Q�Ă���l�Ԃ̃A�j���[�^�[")]
    Animator _bodyAnim;
    [SerializeField, Tooltip("���A�N�V�����̃A�j���[�^�[")]
    Animator _reactionAnim;
    /// <summary>
    /// �Q�Ă���G���N�����Ƃ��ɌĂ�ł�������
    /// </summary>
    public void Awaken()
    {
        _bodyAnim.SetTrigger("IsAwake");
        _reactionAnim.SetTrigger("IsAwake");
    }
    /// <summary>
    /// �ēx�Q�鎞�ɌĂ�ł�������
    /// </summary>
    public void Sleeping()
    {
        _bodyAnim.SetTrigger("IsSleep");
        _reactionAnim.SetTrigger("IsSleep");
    }
    public void Discover()
    {
        _bodyAnim.SetTrigger("IsDiscover");
        _reactionAnim.SetTrigger("IsDiscover");
    }
}
