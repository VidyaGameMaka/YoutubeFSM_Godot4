using Godot;
using FSM.StateMachineBase;

namespace FSM.KnightPlayer;

public partial class KnightPlayer : CharacterBody2D {

	private StateMachine<StateTypes, KnightPlayer> _fsm = new StateMachine<StateTypes, KnightPlayer>();
	private AnimationPlayer _animPlayer;
	private Sprite2D _characterSprite;

	public override void _Ready() {
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_characterSprite = GetNode<Sprite2D>("SpriteContainer/characterSprite2D");

		// Initialize FSM
		_fsm = new StateMachine<StateTypes, KnightPlayer>();
		AddChild(_fsm);
		ConfigureStates(_fsm);

		// Set first state
		_fsm.Init(this, StateTypes.idle);
	}

	private void ConfigureStates(StateMachine<StateTypes, KnightPlayer> fsm) {
		_fsm.AddState(new IdleState());
	}

	public override void _Process(double delta) => _fsm.Process(delta);
	public override void _PhysicsProcess(double delta) => _fsm.PhysicsProcess(delta);

	//----- Shared helpers and utilities -----//
	public Sprite2D CharacterSprite => _characterSprite;
	public void PlayAnimation(string animName) => _animPlayer.Play(animName);
	public StateMachine<StateTypes, KnightPlayer> GetStateMachine() => _fsm;

}
