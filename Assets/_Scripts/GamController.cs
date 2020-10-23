using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamController : MonoBehaviour
{
    public int whosTurn; //0=x, 1=o
    public int turnCount;// count the number of turns played
    public GameObject[] turnIcons; //displays whos turn it is
    public GameObject[] playerIcons;// 0 = x and 1 = o icon
    public GameObject[] tictactoeSpaces; //playable spaces for our game
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
        //for (int i = 0; i < tictactoeSpaces.Length; i++)
        //{
        //    tictactoeSpaces[i]. = true;
        //    tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        //}

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    //fills up the spaces
    public void TicTacToeButton(int whichNumber)
    {
        //tictactoeSpaces[whichNumber].image.sprite = playerIcons[whosTurn];

        //tictactoeSpaces[whichNumber].interactable = false;

        //1 = X filled
        //2 = O filled
        markedSpaces[whichNumber] = whosTurn + 1;
        turnCount++;

        if (turnCount > 4)
        {
            int isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == 0)
            {
                TieGame();

            }
        }

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

        int[] solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
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
        return 0; //if no one wins return 0
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
    int PossibleWins(int[] board)
    {
        int s1 = board[0] + board[1] + board[2];
        int s2 = board[3] + board[4] + board[5];
        int s3 = board[6] + board[7] + board[8];
        int s4 = board[0] + board[3] + board[6];
        int s5 = board[1] + board[4] + board[7];
        int s6 = board[2] + board[5] + board[8];
        int s7 = board[0] + board[4] + board[8];
        int s8 = board[2] + board[4] + board[6];

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
                if(turnCount == 9)
                {
                    return 3;
                }

            }
        }
        return 0; //if no one wins return 0
    }

    int minimax(int[] board, int depth, bool isMax)
    {
        int score = PossibleWins(board);

        if (score == 10)
        {
            return score;
        }
        if (score == -10)
        {
            return score;
        }
        if (turnCount == 9 && score == 0)
        {
            return 0;
        }
        //maximizer move (ai)
        if (isMax)
        {
            int best = -1000;

            //go through all the cells
            for (int i = 0; i < board.Length; i++)
            { //check to see if board is empty
                if (board[i] == -100)
                {
                    //make move
                    board[i] = 1;

                    //recursive
                    best = Mathf.Max(best, minimax(board, depth + 1, !isMax));

                    //undo
                    board[i] = -100;
                }

            }
            return best;
        }

        //minimizer move (human)
        else
        {
            int best = -1000;

            //traverse all cells
            for (int i = 0; i < board.Length; i++)
            {
                //make move
                board[i] = 2;

                best = Mathf.Min(best, minimax(board, depth + 1, !isMax));

                //undo
                board[i] = -100;
            }
            return best;
        }
    }

    //return the best possible move
    int findBestMove(int[] board)
    {
        int bestVal = -1000;
        int bestMove = -1;
        //traverse all cells and evaluate with mnimax
        //for all empty cells and return the best cell
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == -100)
            {
                //make move
                board[i] = 1;

                //computer evaluate func for this move
                int moveVal = minimax(board, 0, false);
                //undo
                board[i] = -100;

                //if value of current move is more than the best value than update the best
                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }

            }
        }
        Debug.Log(bestMove);
        return bestMove;
    }


}
