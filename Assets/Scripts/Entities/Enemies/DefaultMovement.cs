using UnityEngine;
using UnityEngine.AI;

public class DefaultMovement : MonoBehaviour, IMovable
{
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Move(float speed)
    {
        if (_playerTransform != null)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.SetDestination(_playerTransform.position);
        }
    }
}