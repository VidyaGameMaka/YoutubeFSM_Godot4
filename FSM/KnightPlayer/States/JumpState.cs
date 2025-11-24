using Godot;

namespace FSM.KnightPlayer;

public partial class JumpState : State<StateTypes, KnightPlayer> {
	public JumpState() : base(StateTypes.jump) { }

	public override void EnterState(KnightPlayer actor) {
		base.EnterState(actor);
		//Actor.PlayAnimation(StateType.ToString().ToLower());
	}

	public override void PhysicsProcessState(double delta) {
	}

}
