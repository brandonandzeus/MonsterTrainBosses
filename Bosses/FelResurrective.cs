using HarmonyLib;
using MoreBosses.BossActionBehaviours;
using MoreBosses.CardEffects;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using UnityEngine.AddressableAssets;

namespace MoreBosses.Bosses
{
    public class FelResurrective
    {
        public static readonly string ID = "FelResurrective";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            CharacterData baseCharacter = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.FeltheWingsofLightArmor);
            CharacterData alabasterGuardian = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.AlabasterGuardian);

            new CharacterDataBuilder
            {
                CharacterID = ID,
                Size = 6,
                NameKey = "CharacterData_nameKey-c7cf311674b459c9-ca231f4af8d21424185f8e3764689499-v2",
                Health = 700,
                IsOuterTrainBoss = true,
                DeathType = CharacterDeathVFX.Type.Boss,
                AttackDamage = 9,
                StartingStatusEffects =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Multistrike, count = 1},
                },
                StatusEffectImmunities = new string[] { VanillaStatusEffectIDs.Rooted },
                SubtypeKeys =
                {
                    VanillaSubtypeIDs.BigBoss2
                },
                BossActionGroupBuilders =
                {
                    new ActionGroupDataBuilder
                    {
                        ActionBuilders =
                        {
                            new BossActionDataBuilder
                            {
                                // Try to goto a floor without a Alabaster Guardian.
                                // If not then she will move to a random floor.
                                ActionBehaviorType = typeof(RandomUnoccupiedStatueBossActionBehavior),
                                ActionEffectBuilders =
                                {
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectSpawnAlabasterGuardianIfNotPresent),
                                        ParamCharacterData = alabasterGuardian,
                                        ParamInt = 50,
                                    }
                                }
                            },
                            new BossActionDataBuilder
                            {
                                ActionBehaviorType = typeof(RandomBossActionBehavior),
                                ActionEffectBuilders =
                                {
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateType = typeof(CardEffectAddStatusEffect),
                                        TargetMode = TargetMode.Room,
                                        TargetTeamType = Team.Type.Heroes,
                                        ParamStatusEffects =
                                        {
                                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 2},
                                        }
                                    }
                                }
                            },
                        }
                    }
                },
                CharacterLoreTooltipKeys = baseCharacter.GetCharacterLoreTooltipKeys(),
                CharacterChatterData = baseCharacter.GetCharacterChatterData(),
                AttackTeleportsToDefender = baseCharacter.IsAttackTeleportsToDefender(),
                CharacterSoundData = baseCharacter.GetCharacterSoundData(),
                DeathVFX = baseCharacter.GetDeathVfx(),
                ImpactVFX = baseCharacter.GetImpactVfx(),
                BossSpellCastVFX = baseCharacter.GetBossSpellCastVfx(),
                BossRoomSpellCastVFX = baseCharacter.GetBossSpellCastVfx(),
                ProjectilePrefab = baseCharacter.GetProjectilePrefab(),
                BlockVisualSizeIncrease = baseCharacter.BlockVisualSizeIncrease(),
                AscendsTrainAutomatically = baseCharacter.GetAscendsTrainAutomatically(),
                BypassPactCrystalsUpgradeDataList = baseCharacter.GetBypassPactCrystalsUpgradeDataList(),
                CharacterPrefabVariantRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(baseCharacter),
            }.BuildAndRegister();
        }
    }
}