using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 실제 카메라셰이크를 동작시키는 클래스
// 필요속성 : 타겟카메라, 재생시간, 카메라셰이크정보, 카메라셰이크타입, 실행시킬 카메라셰이크클래스
public class YS_CameraShake : MonoBehaviour
{
    //타겟카메라
    public Transform targetCamera;
    //재생시간
    public float playTime = 0.1f;
    [SerializeField]
    //카메라셰이크정보
    CameraShakeInfo info;

    //카메라셰이크타입
    public enum CameraShakeType
    {
        Random
    }
    public CameraShakeType cameraShakeType = CameraShakeType.Random;

    //실행시킬 카메라셰이크클래스
    YS_CameraShakeBase cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = CreateCameraShake(cameraShakeType);
    }

    public static YS_CameraShakeBase CreateCameraShake(CameraShakeType type)
    {
        switch (type)
        {
            case CameraShakeType.Random:
                return new YS_CSRandom();
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayCameraShake();
        }
    }

    void PlayCameraShake()
    {
        StopAllCoroutines();
        StartCoroutine(Play());
    }

    // 재생시간동안 카메라셰이크 실행
    IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);

        cameraShake.Init(targetCamera.position);

        float currentTime = 0;
        // 재생시간동안 
        while (currentTime < playTime)
        {
            currentTime += Time.deltaTime;
            //카메라셰이크 실행
            cameraShake.Play(targetCamera, info);
            yield return null;
        }
        // 끝나면 Stop
        cameraShake.Stop(targetCamera);
    }
}
