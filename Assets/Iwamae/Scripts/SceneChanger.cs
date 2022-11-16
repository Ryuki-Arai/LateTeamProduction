using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// �V�[���J��
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Header("�t�F�[�h�L�����o�X�̃v���n�u�����ꏊ")]
    [SerializeField] GameObject fade;
    GameObject _fadeCanvas;

    void FindFadeCanvas()
    {
        _fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        _fadeCanvas.GetComponent<FadeController>().FadeIn();
    }

    public async void ChangeScene(string name)
    {
        _fadeCanvas.GetComponent<FadeController>().FadeOut();
        //200�~���b(0.2�b)�҂�
        await Task.Delay(200);
        SceneManager.LoadScene(name);
        _fadeCanvas.GetComponent<FadeController>().FadeIn();
    }

    void Awake()
    {
        //0.2�b���FindFadeCanvas���\�b�h�����s
        if (!FadeController._FadeInstance)
        {
            Instantiate(fade);
        }
        Invoke("FindFadeCanvas", 0.2f);
    }
}
