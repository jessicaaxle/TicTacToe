﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamController : MonoBehaviour
{
    public int whosTurn; //0=x, 1=o
    public int turnCount;// count the number of turns played
    public GameObject[] turnIcons; //displays whos turn it is
    public Sprite[] playerIcons;// 0 = x and 1 = y icon
    public Button[] tictactoeSpaces; //playable spaces for our game
    public int[] markedSpaces; //IDs whoch space was marked by which player
    public Text winnerText;
    public GameObject[] winningLines;
    public GameObject winnerPanel;
    public int xPlayScore;
    public int oPlayScore;
    public Text xScoreText;
    public Text oScoreText;
    public GameObject TieImage;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whosTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(whosTurn == 1)
        {
          

            TicTacToeButton(AiMove());
            whosTurn = 0;
        }
    }

    public void TicTacToeButton(int whichNumber)
    {
        tictactoeSpaces[whichNumber].image.sprite = playerIcons[whosTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        //1 = X filled
        //2 = O filled
        markedSpaces[whichNumber] = whosTurn + 1;
        turnCount++;
        canWin();
        //if (turnCount > 4)
        //{
        //    int isWinner = WinnerCheck();
        //    if (turnCount == 9 && isWinner == 0)
        //    {
        //        TieGame();
        //
        //    }
        //}

        if (whosTurn == 0)
        {
            whosTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whosTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    int WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whosTurn + 1))
            {

                winnerPanel.gameObject.SetActive(true);
                if (whosTurn == 0)
                {
                    xPlayScore++;
                    xScoreText.text = xPlayScore.ToString();
                    winnerText.text = "Player X Wins!";
                    winningLines[i].SetActive(true);


                    return 10; //if player 1 win return 10
                }
                if (whosTurn == 1)
                {
                    oPlayScore++;
                    oScoreText.text = oPlayScore.ToString();
                    winnerText.text = "Player O Wins!";
                    winningLines[i].SetActive(true);

                    
                    return -10; //if player 2 wins return -10
                }

            }
         
        }
       //for (int j = 0; j < 9; j++)
       //{
       //    if (markedSpaces[j] == 0)
       //    {
       //        Debug.Log("3 Return");
       //        return 3;
       //
       //    }
       //    else if (j == 8)
       //    {
       //        Debug.Log("0 Return");
       //        return 0;
       //    }
       //}
        Debug.Log("3 end Return");
        return 3; //if no one wins return 0
    }

    void DisplayWinner(int indexIn)
    {
        
        
        
        
        
        
        
        
        
        
        
        
        

        

    }
    
    int canWin()
    {//check the possibility of winning
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whosTurn + 1))
            {

                if (whosTurn == 0)
                {
                    return 10; //if player 1 win return 10
                }
                if (whosTurn == 1)
                {
                    return -10; //if player 2 wins return -10
                }


            }
           
        }
        ////check for a tie
        //for (int j = 0; j < 9; j++)
        //{
        //    if (markedSpaces[j] == 0)
        //    {
        //        Debug.Log("3 Return");
        //        return 3;
        //
        //    }
        //    else if (j == 8)
        //    {
        //        Debug.Log("0 Return");
        //        return 0;
        //    }
        //}
        return 0; //if no one wins return 0
    }
    bool movesLeft(int[] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if(board[i] == -100)
            {
                return true;
            }
        }
        return false;
    }
    int minimax(int[] board, int depth, bool isMax)
    {
        int score = canWin();
        

        if(score ==10)
        {
            return score;
        }
        if(score == -10)
        {
            return score;
        }    
        if(movesLeft(board) == false)
        {
            return 0;
        }

        if(isMax)
        {
            int best = -60;

            for(int i=0; i<9; i++)
            {
                //check if the cell is empty
                if(board[i] == -100)
                {
                    board[i] = 2;
                    int theScore = minimax(board, depth + 1, false);
                    //undo
                    board[i] = -100;
                    best = Mathf.Max(best, theScore);

                }
            }
            return best;
        }

        else
        {
            int best = 60;

            for (int i = 0; i < 9; i++)
            {
                if(board[i]==-100)
                {
                    board[i] = 1;
                    int theScore = minimax(board, depth + 1, true);
                    //undo
                    board[i] = -100;
                    best = Mathf.Min(best, theScore);
                }
            }
            return best;
        }
        
    }

    int AiMove()
    {
        int bestVal = -1999;
        int bestMove = 0;
        for(int i = 0; i <9; i++)
        {
            if (markedSpaces[i] == -100)
            {
                //make move
                markedSpaces[i] = 2;

                //find move
                int moveVal = minimax(markedSpaces, 0, false);

                //undo
                markedSpaces[i] = -100;

                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }
        return bestMove;
    }
    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        TieImage.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xPlayScore = 0;
        oPlayScore = 0;

        xScoreText.text = "0";
        oScoreText.text = "0";
    }

    void TieGame()
    {
        winnerPanel.SetActive(true);
        TieImage.SetActive(true);
        winnerText.text = "TIE";
    }
}
