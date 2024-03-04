using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using Trainworks.Utilities;

namespace MoreBosses.Bosses
{
    /// <summary>
    /// Variant used in Covenant 0.
    /// </summary>
    public class SeraphTheColdhearted
    {
        public static readonly string ID = "SeraphTheColdhearted";
        public static readonly RegistrationPriority Priority = RegistrationPriority.MEDIUM;

        public static void AutoRegister()
        {
            CharacterData baseSeraph = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.SeraphtheChaste);
            CharacterData lightwings = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Lightwings);

            CharacterData SeraphTheColdhearted = new CharacterDataBuilder
            {
                CharacterID = ID,

                Size = 6,
                Health = 999,
                IsOuterTrainBoss = true,
                DeathType = CharacterDeathVFX.Type.Boss,
                AttackDamage = 6,
                StatusEffectImmunities = new string[] { VanillaStatusEffectIDs.Rooted },
                StartingStatusEffects =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Sweep, count = 1 }
                },
                SubtypeKeys =
                {
                    VanillaSubtypeIDs.BigBossT3
                },

                // Controls the bosses behaviour and effects they perform each turn.
                BossActionGroupBuilders =
                {
                    new ActionGroupDataBuilder
                    {
                        ActionBuilders =
                        {
                            new BossActionDataBuilder
                            {
                                // Goto a floor with monsters on it, On this turn.
                                ActionBehaviorType = typeof(RandomOccupiedBossActionBehavior),
                                ParamTeamType = Team.Type.Monsters,
                                ActionEffectBuilders =
                                {
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectAddStatusEffect),
                                        TargetTeamType = Team.Type.Monsters,
                                        TargetMode = TargetMode.Room,
                                        ParamStatusEffects =
                                        {
                                            new StatusEffectStackData {count = 2, statusId = VanillaStatusEffectIDs.Frostbite},
                                        }
                                    },
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectSpawnHero),
                                        ParamCharacterData = lightwings,
                                    }
                                }
                            },
                        }
                    }
                },

                CharacterLoreTooltipKeys = baseSeraph.GetCharacterLoreTooltipKeys(),
                CharacterChatterData = baseSeraph.GetCharacterChatterData(),
                AttackTeleportsToDefender = baseSeraph.IsAttackTeleportsToDefender(),
                CharacterSoundData = baseSeraph.GetCharacterSoundData(),
                DeathVFX = baseSeraph.GetDeathVfx(),
                ImpactVFX = baseSeraph.GetImpactVfx(),
                BossSpellCastVFX = baseSeraph.GetBossSpellCastVfx(),
                BossRoomSpellCastVFX = baseSeraph.GetBossSpellCastVfx(),
                ProjectilePrefab = baseSeraph.GetProjectilePrefab(),
                BlockVisualSizeIncrease = baseSeraph.BlockVisualSizeIncrease(),
                AscendsTrainAutomatically = baseSeraph.GetAscendsTrainAutomatically(),
                BypassPactCrystalsUpgradeDataList = baseSeraph.GetBypassPactCrystalsUpgradeDataList(),
                BundleLoadingInfo = new BundleAssetLoadingInfo
                {
                    BaseName = "ENM_SeraphTheTraitor_Frost",
                    FilePath = "Assets/seraph_frost_test",
                    SpriteName = "Assets/ENM_SeraphTheTraitor_Frost.png",
                    SpineAnimationDict = new Dictionary<CharacterUI.Anim, string>
                    {
                        { CharacterUI.Anim.Idle, "Assets/ENM_SeraphTheTraitor_Frost_Idle.prefab" },
                        { CharacterUI.Anim.Attack, "Assets/ENM_SeraphTheTraitor_Frost_Attack.prefab" },
                        { CharacterUI.Anim.HitReact, "Assets/ENM_SeraphTheTraitor_Frost_HitReact.prefab" },
                        { CharacterUI.Anim.Idle_Relentless, "Assets/ENM_SeraphTheTraitor_Frost_Idle_Relentless.prefab" },
                        { CharacterUI.Anim.Attack_Spell, "Assets/ENM_SeraphTheTraitor_Frost_Spell.prefab" },
                    },
                    AssetType = Trainworks.Builders.AssetRefBuilder.AssetTypeEnum.Character
                },
            }.BuildAndRegister();
        }
    }
}
