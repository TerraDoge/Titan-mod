using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace ExampleMod
{
    public class SuperBouncyBullets : PassiveItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Super Bouncy Bullets";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "ExampleMod/Resources/super_bouncy_bullets";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<SuperBouncyBullets>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Bouncier Than Before";
            string longDesc = "Makes bullets really bouncy and piercing.\n\n" +
                "A crazy man once thought about Bouncy Bullets that were bouncier. The very next day he died.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "tm");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.RangeMultiplier, 2f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalShotBounces, 5f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalShotPiercing, 3f, StatModifier.ModifyMethod.ADDITIVE);


            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.A;
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