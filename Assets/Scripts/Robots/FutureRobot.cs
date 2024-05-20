using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FutureRobot : MonoBehaviour
{
    public Vector3[] destinations;
    private NavMeshAgent _agent;
    private int _currentDestination;
    public GameObject player;
    public float maxDistance;
    public int warningType;
    public TMP_Text warningText;
    public TMP_Text dialogueText;
    public string[] messages;
    public int currentMessage;
    private string _targetScene="SciFi_Warehouse";
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == _targetScene)
        {
            gameObject.GetComponent<RobotControll>().enabled = false;
            gameObject.GetComponent<RobotManager>().enabled = false;
        }
        _agent = GetComponent<NavMeshAgent>();
        _currentDestination = 0;
        currentMessage = 0;
        StartCoroutine(MessageCoroutine());
    }

    public IEnumerator MessageCoroutine()
    {
        dialogueText.text = messages[currentMessage];
        currentMessage += 1;
        yield return new WaitForSeconds(3);
        dialogueText.text = messages[currentMessage];
        _agent.SetDestination(destinations[_currentDestination]);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= maxDistance)
        {
            _agent.isStopped = true;
            DisplayWarningMessage();
        }
        else
        {
            _agent.isStopped = false;
            warningText.gameObject.SetActive(false);
        }


        if (_currentDestination > destinations.Length - 1)
        {
            _agent.SetDestination(player.transform.position);
        }
    }

    public void NextDestination()
    {
        _currentDestination++;
        if (_currentDestination < destinations.Length)
        {
            _agent.SetDestination(destinations[_currentDestination]);
        }
    }


    private void DisplayWarningMessage()
    {
        switch (warningType)
        {
            case 1:
                warningText.text = "Hey! Follow me to the oxygen supply!";
                break;
            case 2:
                warningText.text = "I said follow me! You're going to get lost!";
                break;
            case 3:
                warningText.text = "This is your last chance! If you don't follow me, you'll run out of oxygen!";
                break;
            default:
                warningText.text = "If you don't follow me, you'll die out here!";
                break;
        }

        warningText.gameObject.SetActive(true);
    }
}