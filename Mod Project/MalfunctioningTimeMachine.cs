using ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MultiplayerBasicExample;
using Dungeonator;

namespace TitansMod
{
    class MalfunctioningTimeMachine : PlayerItem
    {
        public static void Init()
        {
            //The name of the item
            string itemName = "Malfunctioning Time Machine";

            //Refers to an embedded png in the project. Make sure to embed your resources!
            string resourceName = "TitansMod/Resources/malfunctioning_time_machine";

            //Create new GameObject
            GameObject obj = new GameObject();

            //Add a ActiveItem component to the object
            var item = obj.AddComponent<MalfunctioningTimeMachine>();

            //Adds a tk2dSprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "20000BC";
            string longDesc = "Turns all enemies to Arrowkin on use.\n\n" +
                "Fixing it might do something great! Too bad you don't know how to repair time machines.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //"example_pool" here is the item pool. In the console you'd type "give example_pool:sweating_bullets"
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "tm");

            //Set the cooldown type and duration of the cooldown
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 350f);

            //Adds a passive modifier, like curse, coolness, damage, etc. to the item. Works for passives and actives.
            //ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 1);

            //Set some other fields
            item.consumable = false;
            item.quality = ItemQuality.B;
        }

        //Turns enemies into arrowkin or something
        protected override void DoEffect(PlayerController user)
        {
            foreach (AIActor aiactor in user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
            {
                if (!aiactor.healthHaver.IsBoss)
                {
                    aiactor.Transmogrify(EnemyDatabase.GetOrLoadByGuid("05891b158cd542b1a5f3df30fb67a7ff"), (GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
                }

            }

        }

        //Disables the item out of combat
        public override bool CanBeUsed(PlayerController user)
        {
            return user.IsInCombat && base.CanBeUsed(user);
        }
    }
}
