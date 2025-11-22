using Godot;

namespace FSM.KnightPlayer;

public partial class IdleState : FSM.StateMachineBase.State<StateTypes, KnightPlayer> {
	public IdleState() : base(StateTypes.idle) { }

	public override void EnterState(KnightPlayer actor) {
		base.EnterState(actor);
		Actor.PlayAnimation(StateType.ToString().ToLower());
	}

	public override void PhysicsProcessState(double delta) {
	}

}
