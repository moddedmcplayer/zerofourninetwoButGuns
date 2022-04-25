using System;
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
            Player.SearchingPickup += EventHandler.OnSearchingForPickup;
            Player.Hurting += EventHandler.OnHurting;
        }

        public void UnRegisterEvents()
        {
            Player.ChangingRole -= EventHandler.OnChangingRole;
            Player.DroppingItem -= EventHandler.OnDroppingItem;
            Player.SearchingPickup -= EventHandler.OnSearchingForPickup;
            Player.Hurting -= EventHandler.OnHurting;
        }
    }
}