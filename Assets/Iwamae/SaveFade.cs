using UnityEngine;

/// <summary>
/// �t�F�[�h����I�u�W�F�N�g���V�[�����܂����ł��ێ����Ă���
/// </summary>
public class SaveFade : MonoBehaviour
{
    public static bool _Instance = false;

    void Start()
    {
        if (!_Instance)
        {
            DontDestroyOnLoad(this);
            _Instance = true;
        }
        else
        {
            Destroy(this);
        }
    }
}
