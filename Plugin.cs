using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace zerofourninetwoButGuns
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "moddedmcplayer";
        public override string Name { get; } = "0492butGuns";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 1, 3);
        public EventHandlers EventHandler;
        public static Plugin Singleton;

        public static int coversion(ItemType input, float amount)
        {
            switch (input)
            {
                case ItemType.Ammo9x19:
                case ItemType.GunCrossvec:
                case ItemType.GunCOM15:
                case ItemType.GunCOM18:
                case ItemType.GunFSP9:
                    return (int)Math.Ceiling(amount/15f);
                case ItemType.GunE11SR:
                case ItemType.Ammo556x45:
                    return (int)Math.Ceiling(amount/40f);
                case ItemType.Ammo762x39:
                case ItemType.GunAK:
                case ItemType.GunLogicer:
                    return (int)Math.Ceiling(amount/30f);
                case ItemType.Ammo12gauge:
                case ItemType.GunShotgun:
                    return (int)Math.Ceiling(amount/14f);
                case ItemType.Ammo44cal:
                case ItemType.GunRevolver:
                    return (int)Math.Ceiling(amount/6f);
                default:
                    return 0;
            }
        }

        public override void OnEnabled()
        {
            Singleton = this;
            EventHandler = new EventHandlers();
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            EventHandler = null;
            Singleton = null;
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            Player.ChangingRole += EventHandler.OnChangingRole;
            Player.DroppingItem += EventHandler.OnDroppingItem;
            Player.Hurting += EventHandler.OnHurting;
        }

        public void UnRegisterEvents()
        {
            Player.ChangingRole -= EventHandler.OnChangingRole;
            Player.DroppingItem -= EventHandler.OnDroppingItem;
            Player.Hurting -= EventHandler.OnHurting;
        }
    }
}