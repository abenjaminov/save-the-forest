using _Scripts;
using ScriptableObjects.Channels;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    private StateMachine _StateMachine;

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

        var shouldIdle = new Func<bool>(() => false);
        var shouldPatrol = new Func<bool>(() => !_followState.TargetVisible(transform.position, 8));
        var shouldFollow = new Func<bool>(() => _followState.TargetVisible(transform.position, 8));

        _StateMachine.AddTransition(_patrolState, shouldPatrol, _followState, () =>
        {
            
        }, "From Follow to Patrol State");
        _StateMachine.AddTransition(_followState, shouldFollow, _patrolState, () =>
        {
            
        }, "From Patrol To Follow State");

        /*_StateMachine.AddTransition(_moveState, shouldMove, _idleState, transitionName: "From Idle To Moving State");
        _StateMachine.AddTransition(_moveState, shouldMove, _jumpState, transitionName: "From Jump To Moving State");
        _StateMachine.AddTransition(_moveState, shouldMove, _changeShapeState, transitionName: "From Jump To Moving State");

        _StateMachine.AddTransition(_idleState, shouldIdle, _moveState, transitionName: "From Move To Idle State");
        _StateMachine.AddTransition(_idleState, shouldIdle, _jumpState, transitionName: "From Jump To Idle State");
        _StateMachine.AddTransition(_idleState, shouldIdle, _changeShapeState, transitionName: "From Jump To Idle State");
        */
        _StateMachine.SetState(_patrolState);
    }

    private void Update()
    {
        _StateMachine.Tick();
    }

}
