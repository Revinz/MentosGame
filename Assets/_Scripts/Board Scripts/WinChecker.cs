using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour {

    int totalRedPiece = 0;
    BoardManager manager;
    Tile[,] board;
        
    public WinChecker(BoardManager _manager)
    {
        manager = _manager;
        board = manager.BoardArray;
        //Count how many red pieces there are on the board
        
        foreach (Tile tile in board)
        {

            if (tile.piece == null)
            {
                Debug.Log(tile.piece);
                continue;
            }
                
            else
            {
                Debug.Log(tile.piece.type);
            }
           

            if (tile.piece.type == Piece.Piece_Types.RED)
                totalRedPiece++;

        } 

        Debug.Log("Total Red Pieces:" + totalRedPiece);
    }
        
    public void CheckForWin(Piece piece)
    {

        // If the player didn't move a red piece, no need to check for a win -- because it is impossible
        if (piece.type != Piece.Piece_Types.RED)
            return;

        //The lists
        List<Tile> UncheckedList = new List<Tile>();
        List<Tile> CheckedList = new List<Tile>();

        Tile startTile = Piece.GetTileBelow(piece);
        UncheckedList.Add(startTile);

        int RedTilesFound = 1;

        // Check if all the red pieces are connected
        while (UncheckedList.Count > 0 && RedTilesFound < totalRedPiece)
        {

            Tile CheckingTile = null;
            int row = 0;
            int column = 0;
            //Find the position of the tile
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {                 
                    if (manager.BoardArray[i, j] == UncheckedList[0])
                    {
                        
                        CheckingTile = board[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            Debug.Log("row: " + row + "column: " + column);

            // Make sure there is a tile to check
            if (CheckingTile == null)
            {
                Debug.Log("No tile to check");
                return;
            }

            //Add the adjecent neighbouring tiles to the unchecked list, but only if they are red tiles  
            Debug.Log("check 1");
            CheckForRedPiece(row- 1,column, UncheckedList, CheckedList, ref RedTilesFound);
            Debug.Log("check 2");
            CheckForRedPiece(row + 1, column, UncheckedList, CheckedList, ref RedTilesFound);
            Debug.Log("check 3");
            CheckForRedPiece(row, column -1, UncheckedList, CheckedList, ref RedTilesFound);
            Debug.Log("check 4");
            CheckForRedPiece(row, column + 1, UncheckedList, CheckedList, ref RedTilesFound);


            //Add the tile that was just checked to the checked list
            CheckedList.Add(UncheckedList[0]);
            //Remove the tile from the unchecked list
            UncheckedList.RemoveAt(0);

            Debug.Log("Finished checking an unchecked red piece");
        }

        if (RedTilesFound >= totalRedPiece)
            Debug.Log("A WIN HAS OCCURED!");
        else
            Debug.Log("No Win :(");

    }
    
    private void CheckForRedPiece(int row, int column, List<Tile> uncheckedList, List<Tile> checkedList, ref int counter)
    {
        if (manager.BoardArray[row, column].piece == null)
            return;

        if (manager.BoardArray[row, column].piece.type == Piece.Piece_Types.RED)
        {
            Tile TileWithRedPiece = board[row, column];

            //Check if the tile is already in the checked list
            foreach (Tile tile in checkedList)
            {
                if (tile == TileWithRedPiece)
                    return;
            }

            //Check if the tile is already in the unchecked list
            foreach (Tile tile in uncheckedList)
            {
                if (tile == TileWithRedPiece)
                    return;
            }

            //Else add it to the unchecked list
            uncheckedList.Add(board[row, column]);
            counter++;
        }
    }

}
