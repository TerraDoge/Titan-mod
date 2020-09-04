using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace ExampleMod
{
    public class BoringHeart : PassiveItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Boring Heart";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "ExampleMod/Resources/boring_heart";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<BoringHeart>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "How Boring...";
            string longDesc = "Grants a heart container.\n\n" +
                "Just another boring heart item. Nothing special.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "tm");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Health, 1, StatModifier.ModifyMethod.ADDITIVE);


            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.D;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}