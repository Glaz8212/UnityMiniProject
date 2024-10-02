using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // UI ���� ����
    public GameObject settingPanel;
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    private CameraController cameraController;
    private AudioListener audioListener;

    private void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // �ʱ� ������ �ҷ�����
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 5f);

        // �����̴� �̺�Ʈ ���
        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void Start()
    {
        // ó�� ������ �� ���� ī�޶� �Ҵ�
        FindMainCamera();
    }

    // �� ��ȯ �� Main Camera�� AudioListener ���Ҵ�
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ ī�޶� ã�Ƽ� �Ҵ�
        FindMainCamera();
    }

    // MainCamera �� AudioListener �ڵ� �Ҵ� �Լ�
    private void FindMainCamera()
    {
        // MainCamera�� ã�� CameraController �Ҵ�
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraController = mainCamera.GetComponent<CameraController>();
            audioListener = mainCamera.GetComponent<AudioListener>();

            // AudioListener�� CameraController�� ���� ��� �߰� ó��
            if (audioListener == null)
            {
                audioListener = mainCamera.gameObject.AddComponent<AudioListener>();
            }

            if (cameraController == null)
            {
                cameraController = mainCamera.gameObject.AddComponent<CameraController>();
            }

            float savedSensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
            float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

            // �����̴��� �ʱⰪ�� ���� ī�޶�� ����� ���������� �ʱ�ȭ
            volumeSlider.value = savedVolume;
            sensitivitySlider.value = savedSensitivity;

            cameraController.speed = 200 * savedSensitivity;
            AudioListener.volume = savedVolume;
        }
    }

    // ���� ����
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume); // ���� ����
    }

    // �ΰ��� ����
    public void OnSensitivityChanged(float sensitivity)
    {
        if (cameraController != null)
        {
            cameraController.speed = 200 * sensitivity;
            PlayerPrefs.SetFloat("sensitivity", sensitivity); // �ΰ��� ����
        }
    }

    // ����â ����
    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    // ����â �ݱ�
    public void CloseSettings()
    {
        settingPanel.SetActive(false);
    }

    // �� �ε� �Լ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ���� �������� �̵� �� �ε��� ���
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
