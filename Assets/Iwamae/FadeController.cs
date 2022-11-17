using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڎ��̃t�F�[�h�����̊Ǘ�
/// </summary>

public class FadeController : MonoBehaviour
{
    [SerializeField]float _time = 0;
    float _fadeSpeed = 0.2f;
    Image _image;

    void Start()
    {
       _image = GetComponentInChildren<Image>();
        StartCoroutine(Fade(false));
    }

    public IEnumerator Fade(bool isFadeOut)
    {
        while(true)
        {
            //���X�ɖ��邭�Ȃ�
            if (!isFadeOut)
            {
                _time -= Time.deltaTime;
                Color c = _image.color;
                c.a = _time / _fadeSpeed;
                _image.color = c;
                if(_time <= 0.0f)
                {
                    yield break;
                }
            }
            //���X�ɈÂ��Ȃ�
            else
            {
                _time += Time.deltaTime;
                Color c = _image.color;
                c.a = _time / _fadeSpeed;
                _image.color = c;
                if (_time  >= 1.0f)
                {
                    //������
                    SceneManager.LoadScene("test2");
                    yield break;
                }
                
            }
             yield return null;
        }
    }
}
