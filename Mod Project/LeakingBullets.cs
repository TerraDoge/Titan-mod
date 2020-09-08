using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace TitansMod
{
    public class LeakingBullets : PassiveItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Leaking Bullets";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "TitansMod/Resources/leaking_bullets";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<LeakingBullets>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Missing The Letter B";
            string longDesc = "Bullets leave a trail of water behind them.\n\n" +
                "Originally they were Water Bullets, but someone wasn't being careful causing them to crack.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "tm");

            //Adds the actual passive effect to the item
            


            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.D;

            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            string text = "assets/data/goops/water goop.asset";
            GoopDefinition goopDefinition;
            try
            {
                GameObject gameObject = assetBundle.LoadAsset(text) as GameObject;
                goopDefinition = gameObject.GetComponent<GoopDefinition>();
            }
            catch
            {
                goopDefinition = (assetBundle.LoadAsset(text) as GoopDefinition);
            }
            goopDefinition.name = text.Replace("assets/data/goops/", "").Replace(".asset", "");
            LeakingBullets.DefaultWaterGoop = goopDefinition;
        }
        public static GoopDefinition DefaultWaterGoop;

        private void AddGoop(Projectile proj)
        {
            DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(LeakingBullets.DefaultWaterGoop).TimedAddGoopCircle(proj.specRigidbody.UnitCenter, 1.5f, 0.5f, false);
        }
        public void PostProcessProjectile(Projectile projectile, float f) 
        {
            projectile.OnPostUpdate += this.AddGoop;
           
        }
           
            


    public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.PostProcessProjectile += this.PostProcessProjectile;
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            player.PostProcessProjectile -= this.PostProcessProjectile;
            return base.Drop(player);
            
        } 
    }
}