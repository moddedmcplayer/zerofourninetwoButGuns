using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using InventorySystem.Items.Firearms.Ammo;
using UnityEngine;

namespace zerofourninetwoButGuns.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Pickup : ICommand
    {
        public string Command { get; } = "PickUp";
        public string[] Aliases { get; } = {"e"};
        public string Description { get; } = "Lets you pickup items as an Zombie";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var ply = Player.Get(sender);
            if (ply.Role.Type != RoleType.Scp0492)
            {
                response = "You must be a SCP-0492 to use this command";
                return false;
            }
            if (ply.Items.Count >= 8)
            {
                response = "Your inventory is full!";
                return false;
            }

            var transform = ply.ReferenceHub.transform;
            var items = Map.Pickups.OrderBy((d) => (d.Position - transform.position).sqrMagnitude).ToArray();

            for(int i = 0; i < Map.Pickups.Count; i++)
            {
                if (Map.Pickups.Count < i || (items[i].Position - transform.position).sqrMagnitude < 0.5f)
                    break;
                if (items[i].Type.IsAmmo() || Plugin.Singleton.Config.ammoPickup)
                {
                    if(items[i] is Exiled.API.Features.Items.Pickup pickup && pickup.Base is AmmoPickup ammo)
                    {
                        //AddAmmo or SetAmmo breaks game
                        ply.AddItem(pickup.Type, Plugin.coversion(pickup.Type, ammo.NetworkSavedAmmo));
                        items[i].Destroy();
                        response = $"Picked up {items[i].Type.ToString()}";
                        return true;
                    }
                }

                if (items[i].Type.IsWeapon(false))
                {
                    if (Plugin.Singleton.Config.blacklisteditems.Contains(items[i].Type))
                    {
                        response = $"This item is blacklisted! ({items[i].Type})";
                        return false;
                    }
                    ply.AddItem(items[i]);
                    items[i].Destroy();
                    response = $"Picked up {items[i].Type.ToString()}";
                    return true;
                }
            }
            
            response = "No items found!";
            return false;
        }
    }
}