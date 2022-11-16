using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �V�[���J�ڎ��̃t�F�[�h�����̊Ǘ�
/// </summary>

public class FadeController : MonoBehaviour
{
    public static bool _FadeInstance = false;
    public bool _isFadeIn = false;
    public bool _isFadeOut = false;
    public float _alpha = 0.0f;
    public float _fadeSpeed = 0.2f;

    public void FadeIn()
    {
        _isFadeIn = true;
        Debug.Log("�t�F�[�h�C�����܂���");
    }

    public void FadeOut()
    {
        _isFadeOut = true;
        Debug.Log("�t�F�[�h�A�E�g���܂���");
    }

    void Awake()
    {
        if (!_FadeInstance)
        {
            DontDestroyOnLoad(this);
            _FadeInstance = true;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        //�t�F�[�h�C���̃t���O��true�̎��͏��X�ɖ��邭����
        if (_isFadeIn)
        {
            _alpha -= Time.deltaTime / _fadeSpeed;
            if (_alpha <= 0.0f)
            {
                _isFadeIn = false;
                _alpha = 0.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, _alpha);
        }
        //�t�F�[�h�A�E�g�̃t���O��true�̎��͏��X�ɈÂ�����
        else if (_isFadeOut)
        {
            _alpha += Time.deltaTime / _fadeSpeed;
            if (_alpha >= 1.0f)
            {
                _isFadeOut = false;
                _alpha = 1.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, _alpha);
        }
    }
}
