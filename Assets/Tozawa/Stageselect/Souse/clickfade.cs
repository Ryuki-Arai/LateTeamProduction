using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using IsGame;
public class ClickFade : MonoBehaviour
{
    //�X�e�[�W�Z���N�g����w��V�[���֔��
    [Header("�t�F�[�hUI���Z�b�g"), SerializeField]
    GameObject _fadein;
    [Header("�X�e�[�W�w�聦�{�^�����ꂼ���"), SerializeField]
    string _stagename;
    [Header("UI�̃A�j���[�V�������Z�b�g"), SerializeField]
    Animator _uianim;
    [SerializeField]Button _button;
    [SerializeField] Animator _anim;
    [SerializeField] int _ownNum = 1;
    [SerializeField] GameObject _yajirusi;
    const int ONE_NUM = 1;
    private void Start()
    {
        //
        if (_ownNum < GameManager.StageIndex)
        {
            _button.interactable = true;
        }
       else
        {
            _button.interactable = false;
            _anim.enabled = false;
            _yajirusi.SetActive(false);
        }
    }

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
