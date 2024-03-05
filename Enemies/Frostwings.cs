using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using Trainworks.Utilities;

namespace MoreBosses.Enemies
{
    public class Frostwings
    {
        public static readonly string ID = "Frostwings";
        public static readonly string TriggerID = "Frostwings_Strike";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            var sharded = CustomCharacterManager.GetCharacterDataByID(FrostwingsShard.ID);

            new CharacterDataBuilder
            {
                CharacterID = ID,
                PactCrystalsRequiredCount = 20,
                PactCrystalsVariantData = sharded,
                Size = 2,
                Health = 90,
                AttackDamage = 2,
                StartingStatusEffects =
                {
                    new StatusEffectStackData { statusId = VanillaStatusEffectIDs.Sweep, count = 1 },
                },
                SubtypeKeys = { VanillaSubtypeIDs.HeavyT3 },
                CharacterLoreTooltipKeys = { "Frostwings_CharacterData_DescriptionKey" },
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
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 1 },
                                }
                            }
                        }
                    }
                },
                CharacterChatterData = sharded.GetCharacterChatterData(),
                AttackTeleportsToDefender = sharded.IsAttackTeleportsToDefender(),
                CharacterSoundData = sharded.GetCharacterSoundData(),
                DeathVFX = sharded.GetDeathVfx(),
                ImpactVFX = sharded.GetImpactVfx(),
                BossSpellCastVFX = sharded.GetBossSpellCastVfx(),
                BossRoomSpellCastVFX = sharded.GetBossSpellCastVfx(),
                ProjectilePrefab = sharded.GetProjectilePrefab(),
                BlockVisualSizeIncrease = sharded.BlockVisualSizeIncrease(),
                AscendsTrainAutomatically = sharded.GetAscendsTrainAutomatically(),
                BypassPactCrystalsUpgradeDataList = sharded.GetBypassPactCrystalsUpgradeDataList(),
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
