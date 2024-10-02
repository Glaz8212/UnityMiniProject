using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // UI 관련 변수
    public GameObject settingPanel;
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    private CameraController cameraController;
    private AudioListener audioListener;

    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 초기 설정값 불러오기
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 5f);

        // 슬라이더 이벤트 등록
        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void Start()
    {
        // 처음 시작할 때 메인 카메라 할당
        FindMainCamera();
    }

    // 씬 전환 시 Main Camera와 AudioListener 재할당
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
        // 씬이 로드될 때마다 카메라를 찾아서 할당
        FindMainCamera();
    }

    // MainCamera 및 AudioListener 자동 할당 함수
    private void FindMainCamera()
    {
        // MainCamera를 찾고 CameraController 할당
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraController = mainCamera.GetComponent<CameraController>();
            audioListener = mainCamera.GetComponent<AudioListener>();

            // AudioListener와 CameraController가 없을 경우 추가 처리
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

            // 슬라이더의 초기값을 현재 카메라와 오디오 설정값으로 초기화
            volumeSlider.value = savedVolume;
            sensitivitySlider.value = savedSensitivity;

            cameraController.speed = 200 * savedSensitivity;
            AudioListener.volume = savedVolume;
        }
    }

    // 볼륨 설정
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume); // 볼륨 저장
    }

    // 민감도 설정
    public void OnSensitivityChanged(float sensitivity)
    {
        if (cameraController != null)
        {
            cameraController.speed = 200 * sensitivity;
            PlayerPrefs.SetFloat("sensitivity", sensitivity); // 민감도 저장
        }
    }

    // 설정창 열기
    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    // 설정창 닫기
    public void CloseSettings()
    {
        settingPanel.SetActive(false);
    }

    // 씬 로드 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 다음 스테이지 이동 용 인덱스 사용
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
