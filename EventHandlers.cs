
using System.Linq;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using MEC;

namespace zerofourninetwoButGuns
{
    public class EventHandlers
    {
        private Config cfg = Plugin.Singleton.Config;
        
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            if(ev.NewRole == RoleType.Scp0492)
            {
                Timing.CallDelayed(1f, () =>
                    {
                        ev.Player.Health = cfg.zombieHP;
                        ev.Player.MaxHealth = cfg.zombieHP;

                        if (cfg.speedyboi)
                        {
                            ev.Player.EnableEffect(EffectType.Scp207);
                            if(cfg.speed > 1)
                                ev.Player.GetEffect(EffectType.Scp207).Intensity = (byte)cfg.speed;
                        }
                    }
                );
            }
        }

        public void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleType.Scp0492 && !ev.Player.Items.Any(x => x.IsWeapon) && cfg.speedyboi)
            {
                ev.Player.EnableEffect(EffectType.Scp207);
                if(cfg.speed > 1)
                    ev.Player.GetEffect(EffectType.Scp207).Intensity = (byte)cfg.speed;
            }
        }
        
        public void OnHurting(HurtingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Target.Role.Type != RoleType.Scp0492)
                return;
            if (ev.Handler.Type == DamageType.Scp207)
                ev.IsAllowed = false;
        }
    }
}