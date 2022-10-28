using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[�Y�@�\
/// </summary>
public class PauseManager : MonoBehaviour
{
	[SerializeField]
	//�@�|�[�Y�������ɕ\������UI�̃v���n�u
	GameObject pauseUIPrefab;
	//�@�|�[�YUI�̃C���X�^���X
	GameObject pauseUIInstance;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			if (pauseUIInstance == null)
			{
				pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
			}
			else
			{
				Destroy(pauseUIInstance);
				Time.timeScale = 1f;
			}
		}
	}
}
