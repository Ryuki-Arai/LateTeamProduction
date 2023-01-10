using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ClickFade : MonoBehaviour
{
    //�X�e�[�W�Z���N�g����w��V�[���֔��
    [Header("�t�F�[�hUI���Z�b�g"), SerializeField]
    GameObject _fadein;
    [Header("�X�e�[�W�w�聦�{�^�����ꂼ���"), SerializeField]
    string _stagename;
    [Header("UI�̃A�j���[�V�������Z�b�g"), SerializeField]
    Animator _uianim;
    const int ONE_NUM = 1;
    public void HighlightCommand()
    {
        _uianim.SetInteger("Anumber", ONE_NUM);
    }
    public void NormalCommand()
    {
        _uianim.SetInteger("Anumber", 0);
    }
    public void ClickCommand()
    {
        _fadein.SetActive(true);
        Invoke(nameof(MoveScene), ONE_NUM); //�}�[�W����Ƃ��͂����ƃR�[�h������V�c ���̓����͂���
    }
    void MoveScene()
    {
        SceneManager.LoadScene(_stagename);
    }

}
