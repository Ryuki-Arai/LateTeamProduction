using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J��
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("�V�[���J�ڐ�̖��O")]
    [SerializeField] string _sceneName;

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
