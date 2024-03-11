using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using Trainworks.Utilities;
using UnityEngine.AddressableAssets;

namespace MoreBosses.Bosses
{
    /// <summary>
    /// Variant for covenant 1 and above.
    /// </summary>
    public class DaedalusSlickExplosivePact
    {
        public static readonly string ID = "DaedalusSlickExplosiveHard";
        public static readonly RegistrationPriority Priority = RegistrationPriority.MEDIUM;

        public static void AutoRegister()
        {
            CharacterData baseCharacter = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.DaedalustheProfessorArmor);

            new CharacterDataBuilder
            {
                CharacterID = ID,
                NameKey = "CharacterData_nameKey-4158c157ee950db4-c8668ee740b3b5e4ca9cf5aa0f16920c-v2",
                Size = 6,
                Health = 450,
                IsOuterTrainBoss = true,
                DeathType = CharacterDeathVFX.Type.Boss,
                AttackDamage = 9,
                StatusEffectImmunities = new string[] { VanillaStatusEffectIDs.Rooted },
                SubtypeKeys =
                {
                    VanillaSubtypeIDs.BigBossT1
                },

                // Controls the bosses behaviour and effects they perform each turn.
                BossActionGroups = baseCharacter.GetBossActionData(),
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
                BypassPactCrystalsUpgradeDataBuilders =
                {
                    /// +8 HP for every 5 pact shards.
                    new CharacterShardUpgradeDataBuilder
                    {
                        CrystalsCount = 5,
                        UpgradeDataBuilder = new CardUpgradeDataBuilder
                        {
                            UpgradeID = "DaedalusHpUpShard",
                            UseUpgradeHighlightTextTags = false,
                            BonusHP = 8,
                        }
                    },
                    // +1 Attack for every 20 pact shards.
                    new CharacterShardUpgradeDataBuilder
                    {
                        CrystalsCount = 20,
                        UpgradeDataBuilder = new CardUpgradeDataBuilder
                        {
                            UpgradeID = "DaedalusAttackUpShard",
                            UseUpgradeHighlightTextTags = false,
                            BonusDamage = 1
                        }
                    },
                    // +5 Regen every 25 (after covenant 10).
                    new CharacterShardUpgradeDataBuilder
                    {
                        CrystalsCount = 25,
                        CovenantUpgrade = true,
                        RequiredCovenantLevel = 10,
                        UpgradeDataBuilder = new CardUpgradeDataBuilder
                        {
                            UpgradeID = "DaedalusRegenShard",
                            UseUpgradeHighlightTextTags = false,
                            StatusEffectUpgrades =
                            {
                                new StatusEffectStackData { statusId = VanillaStatusEffectIDs.Regen, count = 5 },
                            }
                        }
                    },
                },
                CharacterPrefabVariantRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(baseCharacter),
            }.BuildAndRegister();
        }
    }
}