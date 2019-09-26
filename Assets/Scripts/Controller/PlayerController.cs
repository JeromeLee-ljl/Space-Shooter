using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
//    public float aimRangeAngle = 30;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        //玩家移动
        _player.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
//        AimAtMouse();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _player.shootSkill.SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _player.shootSkill.SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _player.shootSkill.SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _player.shootSkill.SwitchWeapon(3);

        //开火  
        if (Input.GetButton("Fire1"))
            _player.shootSkill.Launch();
        if (Input.GetKeyDown(KeyCode.E))
            _player.shieldSkill.Launch();
        if (Input.GetButtonDown("Jump"))
            _player.jumpSkill.Launch();
        if (Input.GetKeyDown(KeyCode.Q))
            _player.bombSkill.Launch();
    }
}