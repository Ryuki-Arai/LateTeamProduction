using UnityEngine;

/// <summary>
/// �|�[�Y���Ƀ|�[�Y���Ă��邩���m�F����UI�̕\��
/// </summary>
public class PauseUI : MonoBehaviour
{
    PauseManager _pm = default;

    void Awake()
    {
        _pm = GameObject.FindObjectOfType<PauseManager>();
    }

    void OnEnable()
    {
        _pm.OnPauseResume += ShowMessage;
    }

    void OnDisable()
    {
        _pm.OnPauseResume -= ShowMessage;
    }

    //�|�[�Y�{�^���������ꂽ��|�[�Y�A
    //������x��������ĊJ�̃��O��\������
    void ShowMessage(bool isPause)
    {
        if (isPause)
        {
            Debug.Log("�|�[�Y��");
        }
        else
        {
            Debug.Log("�ĊJ");
        }
    }
}
