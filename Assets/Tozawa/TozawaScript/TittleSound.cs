using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
public class TittleSound : MonoBehaviour
{
    /// <summary>
    /// �{�^���ł���ǂ�ŃV�[�����Decide�����Ă�ŉ������v
    /// </summary>
    /// <param name="source"></param>
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
