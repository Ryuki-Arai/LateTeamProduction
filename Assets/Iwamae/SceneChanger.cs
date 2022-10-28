using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// �V�[���J��
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("�V�[���J�ڐ�̖��O")]
    [SerializeField] string _sceneName;

    public GameObject fade;//�C���X�y�N�^����Prefab������Canvas������
    public GameObject fadeCanvas;//���삷��Canvas�A�^�O�ŒT��

    void Start()
    {
        if (!FadeController.isFadeInstance)//isFadeInstance�͌�ŗp�ӂ���
        {
            Instantiate(fade);
        }
        Invoke("findFadeObject", 0.02f);//�N�����p��Canvas�̏�����������Ƒ҂�
    }

    void findFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");//Canvas���݂���
        fadeCanvas.GetComponent<FadeController>().fadeIn();//�t�F�[�h�C���t���O�𗧂Ă�
    }

    public async void ChangeScene(string name)
    {
        fadeCanvas.GetComponent<FadeController>().fadeOut();//�t�F�[�h�A�E�g�t���O�𗧂Ă�
        await Task.Delay(200);//�Ó]����܂ő҂�
        SceneManager.LoadScene(name);
    }
}
