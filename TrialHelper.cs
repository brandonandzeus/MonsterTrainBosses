using HarmonyLib;
using System;
using System.Collections.Generic;
using Trainworks.Managers;

public static class TrialHelper
{
    // Note TrialDataList objects are shared across Scenarios of the same areas.
    // T1: Rings 1-2
    // T2: Rings 4-5
    // T3: Ring 7
    public static int[] LevelToClosestDistance = new int[3] { 0, 3, 6 };
    // TODO move to Trainworks.
    public static void AddTrialToLevels(int level, TrialData trial)
    {
        AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
        // Add scenario to appear at distance.
        BalanceData balanceData = allGameData.GetBalanceData();
        RunData runData = balanceData.GetRunData(false);
        NodeDistanceData distanceData = runData.GetDistanceData(LevelToClosestDistance[level]);
        // References are shared across scenarios.
        var scenario = distanceData.GetScenarioData(0);

        var trialDataList = scenario.GetTrialDataList();
        var trialsArray = (TrialData[]) AccessTools.Field(typeof(TrialDataList), "trialDatas").GetValue(trialDataList);
        var newTrials = trialsArray.AddToArray(trial);

        AccessTools.Field(typeof(TrialDataList), "trialDatas").SetValue(trialDataList, newTrials);
    }
}
