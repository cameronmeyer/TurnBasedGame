using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardStatus : MonoBehaviour
{
    public static BoardStatus current;
    public List<Piece> TeamSquid;
    public List<Piece> TeamKid;

    [SerializeField] private Material TeamSquidMat;
    [SerializeField] private Material TeamKidMat;
    [SerializeField] private GameObject Shooter;
    [SerializeField] private GameObject Roller;
    [SerializeField] private GameObject Bomb;
    [SerializeField] [Range(0.0f, 1.0f)] private float percentRollerTeammates = 0.2f;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
    }

    void Awake()
    {
        current = this;
        ChooseTeamColors();
    }

    void ChooseTeamColors()
    {
        ColorPair teamColors = gameObject.GetComponent<Colors>().GetColorPair();

        // 50% chance to swap team colors
        int swapColors = (int) Random.Range(0, 2); // generates either 0 or 1

        if (swapColors < 1)
        {
            TeamSquidMat.color = teamColors.color1;
            TeamKidMat.color = teamColors.color2;
        }
        else
        {
            TeamSquidMat.color = teamColors.color2;
            TeamKidMat.color = teamColors.color1;
        }
    }

    public void InitTeams(List<Vector2> squidTeamRespawns, List<Vector2> kidTeamRespawns)
    {
        // function accepts list of GridSpace locations for each team's respawns

        if(squidTeamRespawns.Count != kidTeamRespawns.Count)
        {
            Debug.LogError("Unequal team sizes");
            return;
        }

        int totalTeammates = squidTeamRespawns.Count;
        for(int teamSize = 0; teamSize < totalTeammates; teamSize++)
        {
            int squidRespawn = Random.Range(0, squidTeamRespawns.Count);
            int kidRespawn = Random.Range(0, kidTeamRespawns.Count);
            
            if (teamSize % 3 == 2) // spawn 1 roller for every 2 shooters
            {
                // spawn rollers

                // squid team
                GameObject squidRoller = Instantiate(Roller, board[(int) squidTeamRespawns[squidRespawn].x, (int) squidTeamRespawns[squidRespawn].y].tile.transform.position, Quaternion.LookRotation(Vector3.right));
                TeamSquid.Add(squidRoller.GetComponent<Piece>());

                // kid team
                GameObject kidRoller = Instantiate(Roller, board[(int)kidTeamRespawns[kidRespawn].x, (int)kidTeamRespawns[kidRespawn].y].tile.transform.position, Quaternion.LookRotation(Vector3.left));
                TeamKid.Add(kidRoller.GetComponent<Piece>());
            }
            else
            {
                // spawn shooters

                // squid team
                GameObject squidShooter = Instantiate(Shooter, board[(int)squidTeamRespawns[squidRespawn].x, (int)squidTeamRespawns[squidRespawn].y].tile.transform.position, Quaternion.LookRotation(Vector3.right));
                TeamSquid.Add(squidShooter.GetComponent<Piece>());

                // kid team
                GameObject kidShooter = Instantiate(Shooter, board[(int)kidTeamRespawns[kidRespawn].x, (int)kidTeamRespawns[kidRespawn].y].tile.transform.position, Quaternion.LookRotation(Vector3.left));
                TeamKid.Add(kidShooter.GetComponent<Piece>());
            }
            squidTeamRespawns.RemoveAt(squidRespawn);
            kidTeamRespawns.RemoveAt(kidRespawn);
        }
    }
}

public struct GridSpace
{
    public Piece piece;
    public Tile tile;

    public GridSpace(Piece piece, Tile tile)
    {
        this.piece = piece;
        this.tile = tile;
    }
}