using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOxygenGather : MonoBehaviour
{
    public FutureRobot robotManager;
    public int oxygenAmount = 100;
    public Image oxygenImage;
    public static Action lights;
    public GameObject[] fans;
    public GameObject fanFixUI;
    public Image fanFixImage;
    public float fanFixAmount;
    public int fixedFans = 0;
    public GameObject oxygenUI;
    public bool setLights = false;
    public BoxCollider doorCollider;
  
    

    private void Start()
    {
        
        StartCoroutine(OxygenCoroutine());
    }

    private IEnumerator OxygenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            oxygenAmount -= 5;
            oxygenImage.fillAmount = ((float)oxygenAmount / 100);
        }
        yield break;
    }

    private void Update()
    {
        CheckDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oxygen"))
        {
            oxygenAmount += 20;
            Destroy(other.gameObject);
            robotManager.NextDestination();
            StartCoroutine(robotManager.MessageCoroutine());
        }
        else if (other.CompareTag("FinalDoor"))
        {
            if (setLights)
            {
                //playiri istedigin scene gonderebilirsin burda
            }
            //final
        }
    }

    private void CheckDistance()
    {
        foreach (var fan in fans)
        {
            if (Vector3.Distance(transform.position, fan.transform.position) < 6)
            {
                fanFixUI.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    fanFixAmount += Time.deltaTime;
                    fanFixImage.fillAmount = fanFixAmount / 5;
                    if (fanFixImage.fillAmount >= 1)
                    {
                        fan.transform.position = new Vector3(1000, 1000, 1000);
                        fanFixUI.SetActive(false);
                        fanFixAmount = 0;
                        fixedFans += 1;
                        fanFixImage.fillAmount = 0;
                        robotManager.NextDestination();
                        StartCoroutine(robotManager.MessageCoroutine());
                        
                    }
                }
            }
        }

        if (!setLights)
        {
            if (fixedFans == 3)
            {
                oxygenUI.SetActive(false);
                lights?.Invoke();
                doorCollider.enabled = true;
                setLights = true;
            }
        }
    }
}