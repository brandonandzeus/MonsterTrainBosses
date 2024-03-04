using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using Trainworks.Interfaces;
using Trainworks.Managers;

namespace MoreBosses
{
    [BepInPlugin(MODGUID, MODNAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks")]
    public class Plugin : BaseUnityPlugin, IInitializable
    {
        public const string MODGUID = "more.bosses";
        public const string MODNAME = "More Bosses";
        public const string VERSION = "1.0";
        public static string BasePath;

        private void Awake()
        {
            var harmony = new Harmony(MODGUID);
            harmony.PatchAll();
        }

        public void Initialize()
        {
            // Automatically find types in this assembly with a AutoRegister static method and call it.
            // Additionally find Custoim CardTraits and if they require a tooltip whitelist it to show a tooltip in-game.
            var assembly = Assembly.GetExecutingAssembly();
            BasePath = PluginManager.PluginGUIDToPath[PluginManager.AssemblyNameToPluginGUID[assembly.FullName]];

            CustomLocalizationManager.ImportCSV("Assets/localization.csv");

            List<RegisterableType> registerableTypes = new List<RegisterableType>();
            List<Type> cardTraitTypes = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                // CardTraits that have a tooltip.
                if (type.IsSubclassOf(typeof(CardTraitState)))
                {
                    bool needsATooltip = type.GetMethod("GetCardTooltipText").DeclaringType == type;
                    Trainworks.Trainworks.Log("Discovered Trait " + type.Name + " Tooltip: " + needsATooltip);

                    if (needsATooltip)
                        cardTraitTypes.Add(type);
                }

                // Registering all GameData objects.
                var method = type.GetMethod("AutoRegister", BindingFlags.Static | BindingFlags.Public);
                if (method != null)
                {
                    var postMethod = type.GetMethod("PostRegister", BindingFlags.Static | BindingFlags.Public);
                    var priorityField = type.GetField("Priority", BindingFlags.Static | BindingFlags.Public);
                    int priority = (int)RegistrationPriority.LOW;
                    if (priorityField != null)
                    {
                        priority = (int)priorityField.GetValue(type);
                    }

                    RegisterableType registerableType = new RegisterableType
                    {
                        type = type,
                        priority = priority,
                        registerMethod = method,
                        postRegisterMethod = postMethod
                    };
                    Trainworks.Trainworks.Log("Discovered Registerable Item " + type.ToString());
                    registerableTypes.Add(registerableType);
                }
            }

            // Registering GameData objects
            registerableTypes.Sort((x, y) => x.priority.CompareTo(y.priority));
            foreach (var obj in registerableTypes)
            {
                Trainworks.Trainworks.Log("Registering " + obj.type.Name);
                obj.registerMethod.Invoke(null, null);
                if (obj.postRegisterMethod != null)
                {
                    obj.postRegisterMethod.Invoke(null, null);
                }
            }

            // Custom Trait Tooltips.
            foreach (var type in cardTraitTypes)
            {
                CustomLocalizationManager.AllowCustomCardTraitTooltips(type);
            }

        }
    }
}
