using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace zerofourninetwoButGuns
{
    public class Config : IConfig
    {
        [Description("Enable plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description("How much hp do you want zombies to have")]
        public byte zombieHP { get; set; } = 250;

        [Description("Whether or not to make zombies go fast without a gun")] 
        public bool speedyboi { get; set; } = true;

        [Description("The speed multiplier to give (max. 4)")]
        public int speed { get; set; } = 1;

        [Description("Guns zombies arent allowed to use")]
        public List<ItemType> blacklisteditems { get; set; } = new List<ItemType>()
        {
            ItemType.GunLogicer,
        };

        [Description("Whether or not to allow zombies to pick up ammo (and reload weapons with it)")]
        public bool ammoPickup { get; set; } = false;
    }
}