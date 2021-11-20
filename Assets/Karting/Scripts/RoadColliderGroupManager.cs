using KartGame.KartSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayersCurrentRaceInfo
{
    public GameObject Player;

    public int Rank;

    public int CircleCount;

    public int Pos;

    public float Distance;

    public PlayersCurrentRaceInfo(GameObject Player)
    {
        this.Player = Player;
        Rank = 0;
        CircleCount = 0;
        Pos = 0;
        Distance = 0;
    }
}
public class RoadColliderGroupManager : MonoBehaviour
{
    public PlayDataFactoty dataFactoty;
    public List<GameObject> RoadColliderGroup;
    public List<GameObject> AI;
    public List<GameObject> AllPlayer;
    public GameObject Player;
    public int currentPlayerPos;
    public int[] currentAIPos; 
    private ArcadeKart[] karts;
    public Dictionary<GameObject, PlayersCurrentRaceInfo> allPlayersRank = null;
    public List<GameObject> AllPlayersRankResult = new List<GameObject>();
    public ObjectiveCompleteLaps objectiveCompleteLaps;
    public int[] CurrentEnemyPlayersCircleCount;
    public int raceRankCount;
    private int frameCount;
    void Start()
    {
        Player = AllPlayer[dataFactoty.playerData.skin];
        currentAIPos = new int[3] { -1, -1, -1};
        CurrentEnemyPlayersCircleCount = new int[3]{ 0, 0, 0};
        currentPlayerPos = -1;
        allPlayersRank = new Dictionary<GameObject, PlayersCurrentRaceInfo> {
            {Player, new PlayersCurrentRaceInfo(Player) },
            {AI[0], new PlayersCurrentRaceInfo(AI[0]) },
            {AI[1], new PlayersCurrentRaceInfo(AI[1]) },
            {AI[2], new PlayersCurrentRaceInfo(AI[2]) }
        };
        RankAllPlayers();
    }
    void FixedUpdate()
    {
        UpdatePlayersDistanceFromLastCollider();

        if (frameCount == 100)
        {
            RankAllPlayers();
            frameCount = 0;
        }
        else
        {
            frameCount++;
        }

    }

    private void UpdatePlayersDistanceFromLastCollider()
    {
        for (int i = 0; i < AI.Count; i++)
        {
            if (currentAIPos[i]>=0)
            {
                allPlayersRank[AI[i]].Distance = Vector3.Distance(RoadColliderGroup[currentAIPos[i]].transform.position, AI[i].transform.position);
            }          
        }
        if (Player)
        {
            if (currentPlayerPos>=0)
            {
                allPlayersRank[Player].Distance = Vector3.Distance(RoadColliderGroup[currentPlayerPos].transform.position, Player.transform.position);
            }           
        }
       
    }

    public void UpdateTargetState(GameObject collider, GameObject target)
    {
        if (!UpdatePosInfo(collider, target))
        {
            return;
        }
        UpdateCicleNumInfo(collider, target);
    }

    private bool UpdatePosInfo(GameObject collider, GameObject target)
    {
        if (target == Player)
        {
            if (GetThePlayerNextCollider(currentPlayerPos) != collider)
            {
                return false;
            }
            currentPlayerPos = RoadColliderGroup.IndexOf(collider);

            if (allPlayersRank.ContainsKey(target))
            {
                allPlayersRank[target].Pos = currentPlayerPos;
            }

            return true;
        }
        for (int i = 0; i < AI.Count; i++)
        {
            if (AI[i] == target)
            {
                if (GetThePlayerNextCollider(currentAIPos[i]) != collider)
                {
                    return false;
                }

                currentAIPos[i] = RoadColliderGroup.IndexOf(collider);
                if (allPlayersRank.ContainsKey(target))
                {
                    allPlayersRank[target].Pos = currentAIPos[i];
                }
            }
        }


        return true;
    }

    private GameObject GetThePlayerNextCollider(int currentPos)
    {
        if (currentPos < RoadColliderGroup.Count - 1)
        {
            return RoadColliderGroup[currentPos + 1];
        }
        else
        {
            return RoadColliderGroup[0];
        }
        return null;
    }
    public void UpdateCicleNumInfo(GameObject collider, GameObject target)
    {
        if (collider.tag != "StartTag")
        {
            return;
        }
        if (target == Player)
        {
            if (objectiveCompleteLaps.currentLap == objectiveCompleteLaps.lapsToComplete)
            {
                raceRankCount++;
                int muilt = 1;
                if (dataFactoty.playerData.gameMode == 0)
                {
                    muilt = 1;
                }
                else if (dataFactoty.playerData.gameMode == 1)
                {
                    muilt = 2;
                }
                else if (dataFactoty.playerData.gameMode == 2)
                {
                    muilt = 4;
                }
                if (AllPlayersRankResult.IndexOf(Player) == 0)
                {
                    dataFactoty.playerData.coin += (20 * muilt);
                }
                else if(AllPlayersRankResult.IndexOf(Player) == 1)
                {
                    dataFactoty.playerData.coin += 10 * muilt;
                }
                dataFactoty.SavePlayerData();
            }

            if (allPlayersRank.ContainsKey(target))
            {
                allPlayersRank[target].CircleCount = objectiveCompleteLaps.currentLap + 1;
                allPlayersRank[target].Rank = raceRankCount;
            }
        }
        for (int i = 0; i < AI.Count; i++)
        {
            if (AI[i] == target)
            {
                if (CurrentEnemyPlayersCircleCount[i] - 1 == objectiveCompleteLaps.lapsToComplete)
                {
                    raceRankCount++;                 
                }
                else
                {
                    CurrentEnemyPlayersCircleCount[i]++;
                }
                if (allPlayersRank.ContainsKey(target))
                {
                    allPlayersRank[target].CircleCount = CurrentEnemyPlayersCircleCount[i];
                    allPlayersRank[target].Rank = raceRankCount;
                }
            }
        }
    }

    void RankAllPlayers()
    {
        List<KeyValuePair<GameObject, PlayersCurrentRaceInfo>> list = new List<KeyValuePair<GameObject, PlayersCurrentRaceInfo>>(allPlayersRank);
        list.Sort(Comparion);
        AllPlayersRankResult.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            AllPlayersRankResult.Add(list[i].Value.Player);
        }
    }

    int Comparion(KeyValuePair<GameObject, PlayersCurrentRaceInfo> x, KeyValuePair<GameObject, PlayersCurrentRaceInfo> y)
    {
        int compareValue = 0;
        compareValue = x.Value.Rank.CompareTo(y.Value.Rank);
        if (compareValue != 0)
        {
            return (0 - compareValue);
        }
        compareValue = x.Value.CircleCount.CompareTo(y.Value.CircleCount);
        if (compareValue != 0)
        {
            return (0 - compareValue);
        }
        compareValue = x.Value.Pos.CompareTo(y.Value.Pos);
        if (compareValue != 0)
        {
            return (0 - compareValue);
        }
        compareValue = x.Value.Distance.CompareTo(y.Value.Distance);
        if (compareValue != 0)
        {
            return (0 - compareValue);
        }
        return (0 - compareValue);
    }
}
