using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Yube.Camera
{
    public class CameraManager : Singleton<CameraManager>
    {
        public bool IsShaking { get; private set; }
        public bool IsZooming { get; private set; }

        public Action OnZoomEnd = null;
        public Action OnCamShakeEnd = null;

        public CinemachineVirtualCamera CurrentVCam { get; private set; }

        private void Start()
        {
            FindVCam();
        }

        public void FindVCam()
        {
            CurrentVCam = FindObjectOfType<CinemachineVirtualCamera>();
            multiChannelPerlin = CurrentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            CurrentVCam.Follow = origin;
        }

        public void StartZoom(CameraZoomData camZoomData, float overrideZoomIn = -1.0f, bool inifinite = false)
        {
            if (!IsZooming)
                zoomRoutine = StartCoroutine(Zoom(camZoomData.ZoomDelta, overrideZoomIn != -1.0f ? overrideZoomIn : camZoomData.ZoomIn, camZoomData.ZoomStay, camZoomData.ZoomOut, isInfinite: inifinite));
        }

        public void StartZoomOnPlayer(CameraZoomData camZoomData)
        {
            if (IsZooming)
                StopCurrentZoom();
            originalPos = CurrentVCam.transform.position;
            zoomRoutine = StartCoroutine(Zoom(camZoomData.ZoomDelta, camZoomData.ZoomIn, camZoomData.ZoomStay, camZoomData.ZoomOut, false));
            StartCoroutine(MoveToPosition(Vector3.zero /* REPLACE BY PLAYER POS */, camZoomData.ZoomIn));
        }

        public void ReinitZoom()
        {
            CurrentVCam.m_Lens.OrthographicSize = baseZoom;
            CurrentVCam.transform.position = originalPos;
        }

        public void StopCurrentZoom()
        {
            if (IsZooming && zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                IsZooming = false;
                CurrentVCam.m_Lens.OrthographicSize = baseZoom;
                OnZoomEnd?.Invoke();
            }
        }

        public void StartShakeCam(CameraShakeData camShakeData, bool isInfinite = false)
        {
            if (!IsShaking)
                shakeRoutine = StartCoroutine(ShakeCam(camShakeData.Amplitude, camShakeData.Frequency, camShakeData.ShakeTime, camShakeData.InTime, camShakeData.OutTime, isInfinite));
        }

        public void StopCurrentShakeCam()
        {
            if (!IsShaking)
                return;
            multiChannelPerlin.m_AmplitudeGain = 0.0f;
            multiChannelPerlin.m_FrequencyGain = 0.0f;
            IsShaking = false;
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
            OnCamShakeEnd?.Invoke();
        }

        private IEnumerator ShakeCam(float intensity, float frequency, float shakeTime, float inTime, float outTime, bool isInfinite = false)
        {
            IsShaking = true;
            float elapsedTime = 0.0f;
            while (elapsedTime < inTime)
            {
                yield return null;
                elapsedTime += Time.unscaledDeltaTime;
                multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0.0f, intensity, elapsedTime / inTime);
                multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(0.0f, frequency, elapsedTime / inTime);
            }
            multiChannelPerlin.m_AmplitudeGain = intensity;
            multiChannelPerlin.m_FrequencyGain = frequency;
            if (isInfinite)
            {
                while (true)
                    yield return null;
            }
            yield return new WaitForSecondsRealtime(shakeTime);
            elapsedTime = 0.0f;
            while (elapsedTime < outTime)
            {
                yield return null;
                elapsedTime += Time.unscaledDeltaTime;
                multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0.0f, elapsedTime / outTime);
                multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(frequency, 0.0f, elapsedTime / outTime);
            }
            multiChannelPerlin.m_AmplitudeGain = 0.0f;
            multiChannelPerlin.m_FrequencyGain = 0.0f;
            IsShaking = false;
            shakeRoutine = null;
            OnCamShakeEnd?.Invoke();
        }

        private IEnumerator Zoom(float zoomDelta, float timeZoomIn, float timeZoomStay, float timeZoomOut, bool restoreZoom = true, bool isInfinite = false)
        {
            IsZooming = true;
            float elapsedTime = 0.0f;
            baseZoom = CurrentVCam.m_Lens.OrthographicSize;
            while (elapsedTime < timeZoomIn)
            {
                yield return null;
                elapsedTime += Time.unscaledDeltaTime;
                CurrentVCam.m_Lens.OrthographicSize = Mathf.Lerp(baseZoom, baseZoom + zoomDelta, elapsedTime / timeZoomIn);
            }
            CurrentVCam.m_Lens.OrthographicSize = baseZoom + zoomDelta;
            if (isInfinite)
            {
                while (true)
                    yield return null;
            }
            yield return new WaitForSecondsRealtime(timeZoomStay);
            elapsedTime = 0.0f;
            while (elapsedTime < timeZoomOut)
            {
                yield return null;
                elapsedTime += Time.unscaledDeltaTime;
                CurrentVCam.m_Lens.OrthographicSize = Mathf.Lerp(baseZoom + zoomDelta, baseZoom, elapsedTime / timeZoomOut);
            }
            if (restoreZoom)
                CurrentVCam.m_Lens.OrthographicSize = baseZoom;
            IsZooming = false;
            zoomRoutine = null;
            OnZoomEnd?.Invoke();
        }

        private IEnumerator MoveToPosition(Vector3 position, float time)
        {
            float elapsedTime = 0.0f;
            originalPos = CurrentVCam.transform.position;
            while (elapsedTime < time)
            {
                yield return null;
                elapsedTime += Time.unscaledDeltaTime;
                Vector2 newPos = Vector2.Lerp(originalPos, position, elapsedTime / time);
                CurrentVCam.transform.position = new Vector3(newPos.x, newPos.y, CurrentVCam.transform.position.z);
            }
        }

        //[EasyButtons.Button]
        //private void PlayShake()
        //{
        //    if(shakeData != null)
        //        StartShakeCam(shakeData);
        //}

        //[EasyButtons.Button]
        //private void PlayZoom()
        //{
        //    if (zoomData != null)
        //        StartZoom(zoomData);
        //}

        private CinemachineBasicMultiChannelPerlin multiChannelPerlin = null;

        private float baseZoom = 0.0f;
        private Vector3 originalPos = Vector3.zero;
        private Coroutine zoomRoutine = null;
        private Coroutine shakeRoutine = null;

        [SerializeField]
        private Transform origin = null;
        //[SerializeField]
        //private CameraShakeData shakeData = null;
        //[SerializeField]
        //private CameraZoomData zoomData = null;
    }
}