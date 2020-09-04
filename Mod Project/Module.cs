using ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExampleMod
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "Titan's Mod";
        public static readonly string VERSION = "0.0.1";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            ItemBuilder.Init();
            BoringHeart.Register();
            MinusOneBullets.Register();
            RingOfFinancialThing.Register();
            SuperBouncyBullets.Register();
            MysterySeed.Register();
            LeakingBullets.Register();
            CheeseToast.Register();
            Fakey.Register();
            BudgetShelletonKey.Register();
            TheBulletBullets.Register();
            Log($"{MOD_NAME} v{VERSION} started successfully. Thank you for downloading!", TEXT_COLOR);
        }

        public static void Log(string text, string color="#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
