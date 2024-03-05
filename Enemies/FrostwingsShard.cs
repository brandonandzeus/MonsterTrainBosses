using System.Collections.Generic;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using Trainworks.Utilities;

namespace MoreBosses.Enemies
{
    public class FrostwingsShard
    {
        public static readonly string ID = "FrostwingsShard";
        public static readonly string TriggerID = "Frostwings_Strike";
        // Shard Enhanced variant needs to be built before the normal enemy instance.
        public static readonly RegistrationPriority Priority = RegistrationPriority.MEDIUM;

        public static void AutoRegister()
        {
            var original = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Pyrewings);
            new CharacterDataBuilder
            {
                CharacterID = ID,
                Size = 2,
                Health = 180,
                AttackDamage = 3,
                StartingStatusEffects =
                {
                    new StatusEffectStackData { statusId = VanillaStatusEffectIDs.Sweep, count = 1 },
                    new StatusEffectStackData { statusId = VanillaStatusEffectIDs.Regen, count = 20},
                },
                SubtypeKeys = { VanillaSubtypeIDs.HeavyT3 },
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.LastAttackedCharacter,
                                TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 2 },
                                }
                            }
                        }
                    }
                },
                CharacterChatterData = original.GetCharacterChatterData(),
                AttackTeleportsToDefender = original.IsAttackTeleportsToDefender(),
                CharacterSoundData = original.GetCharacterSoundData(),
                DeathVFX = original.GetDeathVfx(),
                ImpactVFX = original.GetImpactVfx(),
                BossSpellCastVFX = original.GetBossSpellCastVfx(),
                BossRoomSpellCastVFX = original.GetBossSpellCastVfx(),
                ProjectilePrefab = original.GetProjectilePrefab(),
                BlockVisualSizeIncrease = original.BlockVisualSizeIncrease(),
                AscendsTrainAutomatically = original.GetAscendsTrainAutomatically(),
                BypassPactCrystalsUpgradeDataList = original.GetBypassPactCrystalsUpgradeDataList(),
                BundleLoadingInfo = new BundleAssetLoadingInfo
                {
                    BaseName = "ENM_FrostWings",
                    FilePath = "Assets/more_bosses_assetbundle",
                    SpriteName = "Assets/ENM_FrostWings.png",
                    SpineAnimationDict = new Dictionary<CharacterUI.Anim, string>
                    {
                        { CharacterUI.Anim.Idle, "Assets/ENM_FrostWings_Idle.prefab" },
                        { CharacterUI.Anim.Attack, "Assets/ENM_FrostWings_Attack.prefab" },
                        { CharacterUI.Anim.HitReact, "Assets/ENM_FrostWings_HitReact.prefab" },
                    },
                    AssetType = Trainworks.Builders.AssetRefBuilder.AssetTypeEnum.Character
                },
            }.BuildAndRegister();
        }
    }
}
