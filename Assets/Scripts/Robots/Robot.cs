using UnityEngine;
using UnityEngine.AI;

public abstract class Robot : MonoBehaviour
{
    public Transform _target;
    [Range(1, 20)]
    public float distance;
    internal NavMeshAgent _agent;
    private Animator _anim;
    private bool isClick = false;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Distance() > distance) isClick = false;
        if (Input.GetMouseButtonDown(0)) isClick = true;

        if (isClick is false)
            OnFollow();
        else
            OnClick();

        _anim.SetBool("Walk_Anim", Walked());

        if (RunRobot())
        {
            _agent.speed = AgentSpeed();
            if (DistanceTarget() > 5) _anim.SetBool("Roll_Anim", true);
            else _anim.SetBool("Roll_Anim", false);
        }
        else _anim.SetBool("Roll_Anim", false);
    }

    abstract protected void OnClick();
    abstract protected void OnFollow();

    #region Calculate Functions
    private float Distance() => Vector3.Distance(transform.position, _target.position);
    private bool RunRobot() => _agent.hasPath && isClick is true;
    private float DistanceTarget() => Vector3.Distance(_agent.transform.position, _agent.destination);
    private float AgentSpeed() => Mathf.Lerp(2, 10, DistanceTarget() / 10);
    #endregion

    #region Walk Value Get
    private bool Walked()
    {
        if (_agent.velocity.magnitude > 0)
        {
            return true;
        }
        return false;
    }
    #endregion
}
