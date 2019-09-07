using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : ScriptableObject
{

    public Board board;
    public GameObject TilePrefab;
    public GameObject RedPiecePrefab;
    public GameObject YellowPiecePrefab;
    private float tileSize;
    private float pieceOffsetY;

    public void Setup(Board _board)
    {
        board = _board;
    }

    public void OnEnable()
    {
        TilePrefab = (GameObject)Resources.Load("BoardTilePrototype");
        RedPiecePrefab = (GameObject)Resources.Load("PieceSecondPrototype");
        YellowPiecePrefab = (GameObject)Resources.Load("PiecePrototype");
        tileSize = TilePrefab.gameObject.transform.localScale.x;
        pieceOffsetY = TilePrefab.gameObject.transform.localScale.y - 0.5f;
    }
    //Incl. a list of all gameobjects spawned from the board, for easy despawning

    public void Spawn()
    {
        Debug.Log("SPAWN BOARD");

        board.BoardPieces = new Piece[board.BoardPiecesID.GetLength(0), board.BoardPiecesID.GetLength(1)];

        for (int i = 0; i < board.BoardPiecesID.GetLength(0); i++)
        {
            for (int j = 0; j < board.BoardPiecesID.GetLength(1); j++)
            {

                Vector3 TileSpawnPos = new Vector3((-board.columns / 2 * tileSize) + j * tileSize, 0, (board.rows / 2  * tileSize) - i * tileSize);

                SpawnTile(TileSpawnPos, i, j);

                Piece.Piece_Types type = LevelDataConverter.ToPieceTypeFromID(board.BoardPiecesID[i, j]);
                Vector3 PieceSpawnPos = new Vector3(TileSpawnPos.x, TileSpawnPos.y + pieceOffsetY, TileSpawnPos.z);

                SpawnPiece(type, PieceSpawnPos, i, j);


            }
        }

    }

    public void Despawn()
    {

    }

    private void SpawnPiece(Piece.Piece_Types type, Vector3 position, int boardIndexX, int boardIndexY)
    {
        GameObject PieceObject = null;

        switch (type)
        {
            case 0:
                break;

            case Piece.Piece_Types.RED:
                PieceObject = Instantiate(RedPiecePrefab, position, Quaternion.identity);
                break;

            case Piece.Piece_Types.YELLOW:
                PieceObject = Instantiate(YellowPiecePrefab, position, Quaternion.identity);
                break;


        }

        if (PieceObject != null)
        {
            board.BoardPieces[boardIndexX, boardIndexY] = PieceObject.GetComponent<Piece>();
            board.BoardTiles[boardIndexX, boardIndexY].piece = board.BoardPieces[boardIndexX, boardIndexY];
            board.BoardPieces[boardIndexX, boardIndexY].Setup(type);
        }
    }

    private void SpawnTile(Vector3 position, int boardIndexX, int boardIndexY)
    {
        GameObject TileObject = Instantiate(TilePrefab, position, Quaternion.identity);
        board.BoardTiles[boardIndexX, boardIndexY] = TileObject.GetComponent<Tile>();
    }

}
