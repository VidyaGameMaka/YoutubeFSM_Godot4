using Godot;

namespace FSM.KnightPlayer;

public partial class WalkState : State<StateTypes, KnightPlayer> {
	public WalkState() : base(StateTypes.walk) { }

	public override void EnterState(KnightPlayer actor) {
		base.EnterState(actor);
		//Actor.PlayAnimation(StateType.ToString().ToLower());
	}

	public override void PhysicsProcessState(double delta) {

	}

}
