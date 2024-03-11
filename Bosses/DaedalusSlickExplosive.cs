using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using UnityEngine.AddressableAssets;

namespace MoreBosses.Bosses
{
    public class DaedalusSlickExplosive
    {
        public static readonly string ID = "DaedalusSlickExplosive";
        public static readonly RegistrationPriority Priority = RegistrationPriority.LOW;

        public static void AutoRegister()
        {
            CharacterData baseCharacter = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.DaedalustheProfessorArmor);

            new CharacterDataBuilder
            {
                CharacterID = ID,
                Size = 6,
                NameKey = "CharacterData_nameKey-4158c157ee950db4-c8668ee740b3b5e4ca9cf5aa0f16920c-v2",
                Health = 250,
                IsOuterTrainBoss = true,
                DeathType = CharacterDeathVFX.Type.Boss,
                AttackDamage = 8,
                StatusEffectImmunities = new string[] { VanillaStatusEffectIDs.Rooted },
                SubtypeKeys =
                {
                    VanillaSubtypeIDs.BigBossT1
                },
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
                BypassPactCrystalsUpgradeDataList = baseCharacter.GetBypassPactCrystalsUpgradeDataList(),
                CharacterPrefabVariantRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(baseCharacter),
            }.BuildAndRegister();
        }
    }
}