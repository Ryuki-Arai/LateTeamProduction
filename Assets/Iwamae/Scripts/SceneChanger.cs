using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J��
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("�J�ڐ�̃V�[����")]
    [SerializeField] string _sceneName = "test2";
    [Tooltip("�t�F�[�h�L�����o�X�̃v���n�u")]
    [SerializeField] GameObject _fadeImage;
    [Tooltip("�V�[���J�ڂ���܂ő҂���")]
    [SerializeField ]float _waitTime = 0.8f;

    public void ChangeScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        Instantiate(_fadeImage);
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(_sceneName);
    }
}
