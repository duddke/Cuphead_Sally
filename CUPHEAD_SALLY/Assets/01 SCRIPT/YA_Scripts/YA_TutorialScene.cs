using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YA_TutorialScene : MonoBehaviour
{
    public GameObject FadeOut;
    bool FadeStart;

    private void Update()
    {
        if (FadeStart)
        {
            FadeOut.SetActive(true);
            FadeOut.GetComponent<YA_FadeOut>().FadeSceneCh(3);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="Player")
        {
            if (Input.GetKey(KeyCode.W) && Input.GetButton("Jump"))
                FadeStart = true;
        }
    }

}
