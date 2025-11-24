using Godot;

namespace FSM.KnightPlayer;

public partial class KnightPlayer : CharacterBody2D {

	private StateMachine<StateTypes, KnightPlayer> _fsm = new StateMachine<StateTypes, KnightPlayer>();
	[Export] private AnimationPlayer _animPlayer;
	[Export] private Sprite2D _characterSprite;

	public override void _Ready() {
		// Initialize FSM
		_fsm = new StateMachine<StateTypes, KnightPlayer>();
		AddChild(_fsm);
		ConfigureStates(_fsm);

		// Set first state
		_fsm.Init(StateTypes.idle, this);
	}

	// Add all states to the FSM here
	private void ConfigureStates(StateMachine<StateTypes, KnightPlayer> fsm) {
		_fsm.AddState(new IdleState());
	}

	public override void _Process(double delta) => _fsm.Process(delta);
	public override void _PhysicsProcess(double delta) => _fsm.PhysicsProcess(delta);

	//----- Shared helpers and utilities -----//

	// State Machine Accessor
	public StateMachine<StateTypes, KnightPlayer> GetStateMachine() => _fsm;

	// Animation and Sprite Accessors
	public Sprite2D CharacterSprite => _characterSprite;
	public void PlayAnimation(string animName) => _animPlayer.Play(animName);

	// Input Helpers
	public float GetHorizontalDirection() {
		return Input.GetAxis(InputAxisNames.ui_left.ToString(), InputAxisNames.ui_right.ToString());
	}

	public bool IsJumpJustPressed() {
		return Input.IsActionJustPressed(InputButtonNames.ui_accept.ToString());
	}

	public bool IsJumpPressed() {
		return Input.IsActionPressed(InputButtonNames.ui_accept.ToString());
	}

	public bool IsJumpJustReleased() {
		return Input.IsActionJustReleased(InputButtonNames.ui_accept.ToString());
	}

}
