using _Scripts;
using _Scripts.ScriptableObjects.Channels;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    private StateMachine _StateMachine;

    [SerializeField] 
    private CombatChannel _CombatChannel;

    private EnemyIdleState _idleState;
    private EnemyPatrolState _patrolState;
    private EnemyFollowState _followState;

    private EnemyMovement _EnemyMovement;
    private AnimatorController _animator;

    public Transform Player;

    private void Awake()
    {
        _StateMachine = new StateMachine();
        _EnemyMovement = GetComponent<EnemyMovement>();
        _animator = GetComponent<AnimatorController>();
    }

    private void Start()
    {
        _idleState = new EnemyIdleState(_animator, _EnemyMovement);
        _patrolState = new EnemyPatrolState(_animator, _EnemyMovement);
        _followState = new EnemyFollowState(_animator, _EnemyMovement);

        Player = GameObject.Find("Player").transform;

        var shouldIdle = new Func<bool>(() => !_followState.TargetVisible(transform.position, 6) && _idleState.IdleTime < 3f);
        var shouldPatrol = new Func<bool>(() => _idleState.IdleTime >= 3f);
        var shouldFollow = new Func<bool>(() => _followState.TargetVisible(transform.position, 6));

        _StateMachine.AddTransition(_idleState, shouldIdle, _followState, () =>
        {
            
        }, "From Follow to Idle State");
        _StateMachine.AddTransition(_patrolState, shouldPatrol, _idleState, () =>
        {

        }, "From Idle to Patrol State");
        _StateMachine.AddTransition(_followState, shouldFollow, _patrolState, () =>
        {
            
        }, "From Patrol To Follow State");
        _StateMachine.AddTransition(_followState, shouldFollow, _idleState, () =>
        {

        }, "From Patrol To Follow State");

        _StateMachine.SetState(_patrolState);
    }

    private void Update()
    {
        _StateMachine.Tick();
    }

}