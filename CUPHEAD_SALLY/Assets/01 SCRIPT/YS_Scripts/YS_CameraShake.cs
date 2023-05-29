using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ī�޶����ũ�� ���۽�Ű�� Ŭ����
// �ʿ�Ӽ� : Ÿ��ī�޶�, ����ð�, ī�޶����ũ����, ī�޶����ũŸ��, �����ų ī�޶����ũŬ����
public class YS_CameraShake : MonoBehaviour
{
    //Ÿ��ī�޶�
    public Transform targetCamera;
    //����ð�
    public float playTime = 0.1f;
    [SerializeField]
    //ī�޶����ũ����
    CameraShakeInfo info;

    //ī�޶����ũŸ��
    public enum CameraShakeType
    {
        Random
    }
    public CameraShakeType cameraShakeType = CameraShakeType.Random;

    //�����ų ī�޶����ũŬ����
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

    // ����ð����� ī�޶����ũ ����
    IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);

        cameraShake.Init(targetCamera.position);

        float currentTime = 0;
        // ����ð����� 
        while (currentTime < playTime)
        {
            currentTime += Time.deltaTime;
            //ī�޶����ũ ����
            cameraShake.Play(targetCamera, info);
            yield return null;
        }
        // ������ Stop
        cameraShake.Stop(targetCamera);
    }
}
