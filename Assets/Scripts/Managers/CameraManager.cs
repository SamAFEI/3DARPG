using DG.Tweening;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Image IM_Locked;
    public CinemachineCamera ActiveCamera;
    public CinemachineBrain CameraBrain;
    public CinemachineBasicMultiChannelPerlin cbmPerlin;
    public CinemachineInputAxisController CinCameraInput;
    public CinemachineOrbitalFollow CinOrbitalFollow;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        CameraBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        IM_Locked.enabled = false;
    }

    private void Update()
    {
        if (Instance.CameraBrain.ActiveVirtualCamera != null && Instance.ActiveCamera == null)
        {
            GetActiveCamera(); //first frame cant get ActiveVirtualCamera
        }
    }

    private void GetActiveCamera()
    {
        Instance.ActiveCamera = Instance.CameraBrain.ActiveVirtualCamera as CinemachineCamera;
        Instance.cbmPerlin = Instance.ActiveCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Instance.cbmPerlin.AmplitudeGain = 0;
        Instance.CinCameraInput = Instance.ActiveCamera.GetComponent<CinemachineInputAxisController>();
        Instance.CinOrbitalFollow = Instance.ActiveCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    public static void ApplyShark(float intensity = 10f, float time = 0.5f)
    {
        Instance.ActiveCamera = Instance.CameraBrain.ActiveVirtualCamera as CinemachineCamera;
        Instance.cbmPerlin = Instance.ActiveCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Instance.cbmPerlin.AmplitudeGain = intensity;
        DOTween.To(() => Instance.cbmPerlin.AmplitudeGain, x => Instance.cbmPerlin.AmplitudeGain = x, 0, time);
    }

    public static void SetInputGain(bool isGamepad)
    {
        if (Instance.CinCameraInput == null) { return; }

        Instance.CinCameraInput.Controllers[0].Input.Gain = 1f;
        Instance.CinCameraInput.Controllers[1].Input.Gain = -1f;
        if (isGamepad)
        {
            Instance.CinCameraInput.Controllers[0].Input.Gain = 0.01f;
            Instance.CinCameraInput.Controllers[1].Input.Gain = -0.01f;
        }
    }

    public static void ApplyInputController(bool value)
    {
        if (Instance.CinCameraInput == null) { return; }
        Instance.CinCameraInput.Controllers[0].Enabled = value;
    }

    public static void SetBindingMode(BindingMode mode)
    {
        if (Instance.CinOrbitalFollow == null) { return; }
        Instance.CinOrbitalFollow.TrackerSettings.BindingMode = mode;
    }

    public static void SetIM_LockedPostion(Vector3 worldPosition)
    {
        Instance.IM_Locked.enabled = true;
        Instance.IM_Locked.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
    }
}
