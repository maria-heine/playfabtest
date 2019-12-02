handlers.grantXp = function (args, context)
{
    let killCount = null;
    if (args && args["killCount"])
    {
        killCount = args["killCount"]
    }
    else
    {
        log.error("Null data of 'killCount' necessary to grant player xp.");
        return;
    }    

    let getTitleData : PlayFabServerModels.GetTitleDataRequest = {
        Keys: ["XpPerEnemy"]
    }
    let titleDataResult = server.GetTitleData(getTitleData);
    let xpMultiplier : number = Number(titleDataResult.Data["XpPerEnemy"]);
    log.info(" xpMUltiplier", xpMultiplier);

    let getPlayerStatistics : PlayFabServerModels.GetPlayerStatisticsRequest = {
        PlayFabId: currentPlayerId,
        StatisticNames: ["playerXp"]
    }
    let statisticsResult = server.GetPlayerStatistics(getPlayerStatistics);

    let playerStatistic = statisticsResult.Statistics.find(x => x.StatisticName == "playerXp");

    let playerTotalXp : number = 0;

    if(playerStatistic != undefined)
    {
        playerTotalXp = playerStatistic.Value
    }

    // log.info("playerTotalXp", playerTotalXp);
    let finalPlayerXp : number = 0;

    finalPlayerXp = playerTotalXp + killCount * xpMultiplier;

    log.info(finalPlayerXp.toString());
    log.info(playerTotalXp.toString());
    log.info(killCount.toString());
    
    let request : PlayFabServerModels.UpdatePlayerStatisticsRequest = {
        PlayFabId: currentPlayerId, Statistics: [{
                StatisticName: "playerXp",
                Value: finalPlayerXp
            }]
    };

    var playerStatResult = server.UpdatePlayerStatistics(request);
}
