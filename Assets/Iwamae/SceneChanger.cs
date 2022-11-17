using UnityEngine;

/// <summary>
/// �V�[���J��
/// </summary>
public class SceneChanger : MonoBehaviour
{


    GameObject _fadeCanvas;

    void Awake()
    {
        _fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
    }

    //�{�^�����������Ƃ��Ɏ��s���鏈��(������)
    public void ChangeScene(string name)
    {
        var fadeIn = _fadeCanvas.GetComponent<FadeController>().Fade(true);
        StartCoroutine(fadeIn);
    }
}
