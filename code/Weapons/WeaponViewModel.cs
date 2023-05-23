using Sandbox;

namespace SWRP.Weapons;
/// <summary>
/// Template used from https://github.com/sboxgame/templates/tree/main/game.shooter
/// </summary>
public partial class WeaponViewModel : BaseViewModel
{
	protected Weapon Weapon { get; init; }

	public WeaponViewModel( Weapon weapon )
	{
		Weapon = weapon;
		EnableShadowCasting = false;
		EnableViewmodelRendering = true;
	}

	public override void PlaceViewmodel()
	{
		base.PlaceViewmodel();

		Camera.Main.SetViewModelCamera( 80f, 1, 500 );
	}
}
