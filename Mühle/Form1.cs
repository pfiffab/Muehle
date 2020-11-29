using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Mühle
{
    public partial class Form1 : Form
    {
        // Create Buttons which represent the playable stones
        PlayStone[,,] buttons = new PlayStone[3, 3, 3];
        // indicates that the game is in the movement phase
        bool midgame = false;
        // indicates a player has selected a stone to be moved
        bool selected = false;
        // indicates the game has just started and the players are allowed to place down stones
        bool start = true;
        // indicates which players turn it is
        bool turnp1 = true;
        bool turnp2 = false;
        // sets the colors for each player
        Color colorp1 = Color.SaddleBrown;
        Color colorp2 = Color.SandyBrown;
        // initialize variables which are needed globally throughout the program
        int delete = 0;
        // possible moves for each player
        int movesavailablep1;
        int movesavailablep2;
        // counts how many stones each player has placed already
        int placedstonesp1 = 0;
        int placedstonesp2 = 0;
        // identify a button by its xyz value
        int ibtnx;
        int ibtny;
        int ibtnz;
        // used to identify a second button in movement
        int sourcex;
        int sourcey;
        int sourcez;
        // counts how many stones each player has on the board
        int stonesp1;
        int stonesp2;
        public class PlayStone : Button
        {
            // XYZ represent the position of the button within the array and on the field
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            // indicates that the stone is part of a mill and prevents it from being deleted
            public bool Mill { get; set; }
        }
        public void ViableMoveCheck()
        {
            // resets the available moves for each player to zero
            movesavailablep1 = 0;
            movesavailablep2 = 0;
            // only counts the available moves if more than three stones remain since with three stones movement is global
            if (stonesp1 > 3)
            {
                foreach (PlayStone a in buttons)
                {
                    if (a.BackColor == colorp1)
                    {
                        if (a.X != 2)
                        {
                            if (buttons[a.X + 1, a.Y, a.Z].BackColor != Color.Khaki && buttons[a.X + 1, a.Y, a.Z].BackColor != colorp1 && buttons[a.X + 1, a.Y, a.Z].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                        if (a.X != 0)
                        {
                            if (buttons[a.X - 1, a.Y, a.Z].BackColor != Color.Khaki && buttons[a.X - 1, a.Y, a.Z].BackColor != colorp1 && buttons[a.X - 1, a.Y, a.Z].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                        if (a.Y != 2)
                        {
                            if (buttons[a.X, a.Y + 1, a.Z].BackColor != Color.Khaki && buttons[a.X, a.Y + 1, a.Z].BackColor != colorp1 && buttons[a.X, a.Y + 1, a.Z].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                        if (a.Y != 0)
                        {
                            if (buttons[a.X, a.Y - 1, a.Z].BackColor != Color.Khaki && buttons[a.X, a.Y - 1, a.Z].BackColor != colorp1 && buttons[a.X, a.Y - 1, a.Z].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                        if (a.Z != 2 && (a.Y == 1 || a.X == 1))
                        {
                            if (buttons[a.X, a.Y, a.Z + 1].BackColor != Color.Khaki && buttons[a.X, a.Y, a.Z + 1].BackColor != colorp1 && buttons[a.X, a.Y, a.Z + 1].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                        if (a.Z != 0 && (a.Y == 1 || a.X == 1))
                        {
                            if (buttons[a.X, a.Y, a.Z - 1].BackColor != Color.Khaki && buttons[a.X, a.Y, a.Z - 1].BackColor != colorp1 && buttons[a.X, a.Y, a.Z - 1].BackColor != colorp2)
                            {
                                movesavailablep1 += 1;
                            }
                        }
                    }
                }
                // if the player has no available moves he loses (not in the placing phase obviously)
                if (movesavailablep1 == 0 && !start)
                {
                    MessageBox.Show("Spieler 2 gewinnt!");
                    this.Close();
                }
            }
            // only counts the available moves if more than three stones remain since with three stones movement is global
            if (stonesp2 > 3)
            {
                                foreach (PlayStone a in buttons)
                {
                    if (a.BackColor == colorp2)
                    {
                        if (a.X != 2)
                        {
                            if (buttons[a.X + 1, a.Y, a.Z].BackColor != Color.Khaki && buttons[a.X + 1, a.Y, a.Z].BackColor != colorp1 && buttons[a.X + 1, a.Y, a.Z].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                        if (a.X != 0)
                        {
                            if (buttons[a.X - 1, a.Y, a.Z].BackColor != Color.Khaki && buttons[a.X - 1, a.Y, a.Z].BackColor != colorp1 && buttons[a.X - 1, a.Y, a.Z].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                        if (a.Y != 2)
                        {
                            if (buttons[a.X, a.Y + 1, a.Z].BackColor != Color.Khaki && buttons[a.X, a.Y + 1, a.Z].BackColor != colorp1 && buttons[a.X, a.Y + 1, a.Z].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                        if (a.Y != 0)
                        {
                            if (buttons[a.X, a.Y - 1, a.Z].BackColor != Color.Khaki && buttons[a.X, a.Y - 1, a.Z].BackColor != colorp1 && buttons[a.X, a.Y - 1, a.Z].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                        if (a.Z == 1)
                        {
                            if (buttons[a.X, a.Y, a.Z + 1].BackColor != Color.Khaki && buttons[a.X, a.Y, a.Z + 1].BackColor != colorp1 && buttons[a.X, a.Y, a.Z + 1].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                        if (a.Z == 1)
                        {
                            if (buttons[a.X, a.Y, a.Z - 1].BackColor != Color.Khaki && buttons[a.X, a.Y, a.Z - 1].BackColor != colorp1 && buttons[a.X, a.Y, a.Z - 1].BackColor != colorp2)
                            {
                                movesavailablep2 += 1;
                            }
                        }
                    }
                }
                // if the player has no available moves he loses (not in the placing phase obviously)
                if (movesavailablep2 == 0 && !start)
                {
                    MessageBox.Show("Spieler 1 gewinnt!");
                    this.Close();
                }
            }
        }
        public void StoneCounter()
        {
            // counts how many stones each player currently has
            // first resets the counter
            stonesp1 = 0;
            stonesp2 = 0;
            // counts each PlayStone by color
            foreach (PlayStone a in buttons)
            {
                if(a.BackColor == colorp1)
                {
                    stonesp1 += 1;
                }
                if (a.BackColor == colorp2)
                {
                    stonesp2 += 1;
                }
            }
            // displays the var
            txtp1.Text = Convert.ToString(stonesp1);
            txtp2.Text = Convert.ToString(stonesp2);
            // if a player has less than 3 stones they lose
            if (!start && stonesp1 < 3)
            {
                MessageBox.Show("Spieler 2 gewinnt!");
                this.Close();
            }
            else if(!start && stonesp2 < 3)
            {
                MessageBox.Show("Spieler 1 gewinnt!");
                this.Close();
            }
        }
        public void MillCounterP1()
        {
            // checks and counts which mills player 1 has
            int x = ibtnx;
            int y = ibtny;
            int z = ibtnz;
            // checks if the player created a mill with his last moved stone and allows him to delete one enemy stone
            if (buttons[x, 0, z].BackColor == colorp1 && buttons[x, 1, z].BackColor == colorp1 && buttons[x, 2, z].BackColor == colorp1)
            {
                delete = 1;
            }
            if (buttons[0, y, z].BackColor == colorp1 && buttons[1, y, z].BackColor == colorp1 && buttons[2, y, z].BackColor == colorp1)
            {
                delete = 1;
            }
            if (x == 1 ^ y == 1) // XOR = either X=1 or Y=1 but they can't be both 1 nor both not 1
            {
                if (buttons[x, y, 0].BackColor == colorp1 && buttons[x, y, 1].BackColor == colorp1 && buttons[x, y, 2].BackColor == colorp1)
                {
                    delete = 1;
                }
            }
            // keep the players turn so he can delete
            if (delete > 0)
            {
                turnp1 = true;
                turnp2 = false;
            }
        }
        public void MillCounterP2()
        {
            // checks and counts which mills player 2 has
            int x = ibtnx;
            int y = ibtny;
            int z = ibtnz;
            // checks if the player created a mill with his last moved stone and allows him to delete one enemy stone
            if (buttons[x, 0, z].BackColor == colorp2 && buttons[x, 1, z].BackColor == colorp2 && buttons[x, 2, z].BackColor == colorp2)
            {
                delete = 1;
            }
            if (buttons[0, y, z].BackColor == colorp2 && buttons[1, y, z].BackColor == colorp2 && buttons[2, y, z].BackColor == colorp2)
            {
                delete = 1;
            }
            if (x == 1 ^ y == 1) // XOR = either X=1 or Y=1 but they can't be both 1 nor both not 1
            {
                if (buttons[x, y, 0].BackColor == colorp2 && buttons[x, y, 1].BackColor == colorp2 && buttons[x, y, 2].BackColor == colorp2)
                {
                    delete = 1;
                }
            }
            // keep the players turn so he can delete
            if (delete > 0)
            {
                turnp1 = false;
                turnp2 = true;
            }
        }
        public void CheckAllMills()
        {
            // this simply checks for all the existing mills and locks them from being deleted
            // removes the mill status from all buttons
            foreach(PlayStone a in buttons)
            {
                a.Mill = false;
            }
            // player 2 mills
            // square crossing mills ("diagonal")
            for(int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (x == 1 ^ y == 1)
                    {
                        if (buttons[x, y, 0].BackColor == colorp2 && buttons[x, y, 1].BackColor == colorp2 && buttons[x, y, 2].BackColor == colorp2)
                        {
                            buttons[x, y, 0].Mill = true;
                            buttons[x, y, 1].Mill = true;
                            buttons[x, y, 2].Mill = true;
                        }
                    }
                }
            }
            for (int z = 0; z <= 2; z++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    // vertical mills
                    if (buttons[x, 0, z].BackColor == colorp2 && buttons[x, 1, z].BackColor == colorp2 && buttons[x, 2, z].BackColor == colorp2)
                    {
                        buttons[x, 0, z].Mill = true;
                        buttons[x, 1, z].Mill = true;
                        buttons[x, 2, z].Mill = true;
                    }
                }
                for (int y = 0; y <= 2; y++)
                {
                    // horizontal mills
                    if (buttons[0, y, z].BackColor == colorp2 && buttons[1, y, z].BackColor == colorp2 && buttons[2, y, z].BackColor == colorp2)
                    {
                        buttons[0, y, z].Mill = true;
                        buttons[1, y, z].Mill = true;
                        buttons[2, y, z].Mill = true;
                    }
                }
            }
            // player 1 mills
            // square crossing mills ("diagonal")
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (x == 1 ^ y == 1)
                    {
                        if (buttons[x, y, 0].BackColor == colorp1 && buttons[x, y, 1].BackColor == colorp1 && buttons[x, y, 2].BackColor == colorp1)
                        {
                            buttons[x, y, 0].Mill = true;
                            buttons[x, y, 1].Mill = true;
                            buttons[x, y, 2].Mill = true;
                        }
                    }
                }
            }
            for (int z = 0; z <= 2; z++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    // vertical mills
                    if (buttons[x, 0, z].BackColor == colorp1 && buttons[x, 1, z].BackColor == colorp1 && buttons[x, 2, z].BackColor == colorp1)
                    {
                        buttons[x, 0, z].Mill = true;
                        buttons[x, 1, z].Mill = true;
                        buttons[x, 2, z].Mill = true;
                    }
                }
                for (int y = 0; y <= 2; y++)
                {
                    // horizontal mills
                    if (buttons[0, y, z].BackColor == colorp1 && buttons[1, y, z].BackColor == colorp1 && buttons[2, y, z].BackColor == colorp1)
                    {
                        buttons[0, y, z].Mill = true;
                        buttons[1, y, z].Mill = true;
                        buttons[2, y, z].Mill = true;
                    }
                }
            }
        }
        public void RemoveStone()
        {
            // let's player 2 delete stones which do belong to player 1 and are not currently in a mill
            if (turnp1)
            {
                if (buttons[ibtnx, ibtny, ibtnz].BackColor == colorp2 && buttons[ibtnx, ibtny, ibtnz].Mill != true)
                {
                    buttons[ibtnx, ibtny, ibtnz].BackColor = Color.Transparent;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    delete--;
                    if(delete == 0)
                    {
                        turnp1 = false;
                        turnp2 = true;
                    }
                }
            }
            else if (turnp2)
            {
                // let's player 1 delete stones which do belong to player 2 and are not currently in a mill
                if (buttons[ibtnx,ibtny,ibtnz].BackColor == colorp1 && buttons[ibtnx, ibtny, ibtnz].Mill != true)
                {
                    buttons[ibtnx, ibtny, ibtnz].BackColor = Color.Transparent;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    delete--;
                    if (delete == 0)
                    {
                        turnp1 = true;
                        turnp2 = false;
                    }
                }
            }
        }
        public void PlacingPhase()
        {
            // game phase in which the players can each place down 9 stones
            // prevents players from placing stones on stones which are already taken by another player
            if (buttons[ibtnx, ibtny, ibtnz].BackColor != colorp1 && buttons[ibtnx, ibtny, ibtnz].BackColor != colorp2)
            {
                if (turnp1)
                {
                    buttons[ibtnx, ibtny, ibtnz].BackColor = colorp1;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = colorp1;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = colorp1;
                    // counts how many stones player 1 has
                    placedstonesp1++;
                    // swaps turns
                    turnp1 = false;
                    turnp2 = true;
                    MillCounterP1();
                    CheckAllMills();
                }
                else if (turnp2)
                {
                    buttons[ibtnx, ibtny, ibtnz].BackColor = colorp2;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = colorp2;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = colorp2;
                    // counts how many stones player 2 has
                    placedstonesp2++;
                    // swaps turns
                    turnp1 = true;
                    turnp2 = false;
                    MillCounterP2();
                    CheckAllMills();
                }
            }
            // if both players have placed down 9 stones the game state changes to the next phase
            if (placedstonesp1 == 9 && placedstonesp2 == 9)
            {
                start = false;
                midgame = true;
            }
        }
        public void StoneSelection()
        {
            // allows the player to select the PlayStone he would like to move and prevents him from moving the other players PlayStone
            if (turnp1 && buttons[ibtnx, ibtny, ibtnz].BackColor == colorp1)
            {
                // sets the source stones position
                sourcex = ibtnx;
                sourcey = ibtny;
                sourcez = ibtnz;
                // marks the source PlayStone so the players knows which one he is moving currently
                //buttons[ibtnx, ibtny, ibtnz].Text = "S";
                buttons[ibtnx, ibtny, ibtnz].FlatAppearance.BorderSize = 2;
                // allows the player to go to the next phase and move his PlayStone
                selected = true;
            }
            else if(turnp2 && buttons[ibtnx, ibtny, ibtnz].BackColor == colorp2)
            {
                // sets the source stones position
                sourcex = ibtnx;
                sourcey = ibtny;
                sourcez = ibtnz;
                // marks the source PlayStone so the players knows which one he is moving currently
                //buttons[ibtnx, ibtny, ibtnz].Text = "S";
                buttons[ibtnx, ibtny, ibtnz].FlatAppearance.BorderSize = 2;
                // allows the player to go to the next phase and move his PlayStone
                selected = true;
            }
        }
        public void StoneMovement()
        {
            // if the players clicks the selected PlayStone again he will reset the phase and can select another PlayStone
            if (sourcex == ibtnx && sourcey == ibtny && sourcez == ibtnz)
            {
                selected = false;
                //buttons[sourcex, sourcey, sourcez].Text = Convert.ToString(buttons[sourcex, sourcey, sourcez].X) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Y) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Z);
                buttons[sourcex, sourcey, sourcez].FlatAppearance.BorderSize = 0;
            }
            else
            {
                // this checks if the player is trying to do an illegal move
                bool legal = false;
                int sourceval = sourcex * 100 + sourcey * 10 + sourcez;
                int ibtnval = ibtnx * 100 + ibtny * 10 + ibtnz;
                int distance = sourceval - ibtnval;
                if ((distance == 1 || distance == -1) && (sourcex == 1 || sourcey == 1))
                {
                    legal = true;
                }
                if (distance == 10 || distance == -10 || distance == 100 || distance == -100)
                {
                    legal = true;
                }
                // if the players have 3 stones left they can move anywhere they would like
                if(turnp1 && stonesp1 == 3)
                {
                    legal = true;
                }
                if (turnp2 && stonesp2 == 3)
                {
                    legal = true;
                }
                // player 1 can move his preselected PlayStone if the PlayStone does belong to neither player and the move would be legal (no diagonals and only moving 1 position)
                if (turnp1 && buttons[ibtnx, ibtny, ibtnz].BackColor != colorp1 && buttons[ibtnx, ibtny, ibtnz].BackColor != colorp2 && legal)
                {
                    // resets the button color of the moved PlayStone
                    buttons[sourcex, sourcey, sourcez].BackColor = Color.Transparent;
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    //buttons[sourcex, sourcey, sourcez].Text = Convert.ToString(buttons[sourcex, sourcey, sourcez].X) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Y) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Z);
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.BorderSize = 0;
                    buttons[ibtnx, ibtny, ibtnz].BackColor = colorp1;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = colorp1;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = colorp1;
                    turnp1 = false;
                    turnp2 = true;
                    selected = false;
                    MillCounterP1();
                    CheckAllMills();
                }
                // player 2 can move his preselected PlayStone in the PlayStone does belong to neither player and the move would be legal (no diagonals and only moving 1 position)
                else if (turnp2 && buttons[ibtnx, ibtny, ibtnz].BackColor != colorp1 && buttons[ibtnx, ibtny, ibtnz].BackColor != colorp2 && legal)
                {
                    // resets the button color of the moved PlayStone
                    buttons[sourcex, sourcey, sourcez].BackColor = Color.Transparent;
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    //buttons[sourcex, sourcey, sourcez].Text = Convert.ToString(buttons[sourcex, sourcey, sourcez].X) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Y) + Convert.ToString(buttons[sourcex, sourcey, sourcez].Z);
                    buttons[sourcex, sourcey, sourcez].FlatAppearance.BorderSize = 0;
                    buttons[ibtnx, ibtny, ibtnz].BackColor = colorp2;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseOverBackColor = colorp2;
                    buttons[ibtnx, ibtny, ibtnz].FlatAppearance.MouseDownBackColor = colorp2;
                    turnp1 = true;
                    turnp2 = false;
                    selected = false;
                    MillCounterP2();
                    CheckAllMills();
                }
            }
        }
        public void GameProgression()
        {
            // game progression
            // before each round count how many stones each player has on the board
            StoneCounter();
            // in each turn the game checks if a player can delete something
            if (delete > 0)
            {
                RemoveStone();
                ViableMoveCheck();
            }
            // the game starts in the start phase of the game where players can place down their stones
            else if (start)
                {
                    PlacingPhase();
                }
            // the player can move his PlayStone if he selected one already
            else if (selected)
                {
                    StoneMovement();
                }
            // after start the game state is set to midgame where the player can select a PlayStone to move
            else if (midgame)
                {
                    StoneSelection();
                }
            // updates the info box every turn
            if (turnp1)
            {
                txtturn.Text = "Spieler 1";
            }
            else if (turnp2)
            {
                txtturn.Text = "Spieler 2";
            }
            StoneCounter();
            ViableMoveCheck();
        }
        public void BoardCreation()
        {
            // this sets up the lines and field
            Size = new Size(620, 620);
            Bitmap bm = new Bitmap(800, 800);
            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush blackBrush = new SolidBrush(Color.DarkRed))
            {
                // sets the thickness of the lines
                int linethick = 10;

                // distance from upper right corner to the outer square
                int ox = 50;
                int oy = 50;
                int ol = 500;

                // distance from outer square to middle square
                int mx = ox + 50;
                int my = oy + 50;
                int ml = ol - 100;

                // distance from middle square to inner square
                int ix = mx + 50;
                int iy = my + 50;
                int il = ml - 100;
                // outer square
                g.FillRectangle(blackBrush, ox, oy, ol, linethick); //top
                g.FillRectangle(blackBrush, ox, oy, linethick, ol); //left
                g.FillRectangle(blackBrush, ox + ol - linethick, oy, linethick, ol); //right
                g.FillRectangle(blackBrush, ox, oy + ol, ol, linethick); //bot
                // middle square
                g.FillRectangle(blackBrush, mx, my, ml, linethick);//top
                g.FillRectangle(blackBrush, mx, my, linethick, ml); //left
                g.FillRectangle(blackBrush, mx + ml - linethick, my, linethick, ml); //right
                g.FillRectangle(blackBrush, mx, my + ml, ml, linethick); //bot
                // inner square
                g.FillRectangle(blackBrush, ix, iy, il, linethick); //top
                g.FillRectangle(blackBrush, ix, iy, linethick, il); //left
                g.FillRectangle(blackBrush, ix + il - linethick, iy, linethick, il); //right
                g.FillRectangle(blackBrush, ix, iy + il, il, linethick); //bot
                // middle divider lines
                g.FillRectangle(blackBrush, ix + il, oy + ol / 2, ix - ox, linethick); //midhorizontalright
                g.FillRectangle(blackBrush, ox, oy + ol / 2, ix - ox, linethick); //midhorizontalleft
                g.FillRectangle(blackBrush, ox + ol / 2, oy, linethick, iy - oy); //midverticaltop
                g.FillRectangle(blackBrush, ox + ol / 2, ix + il, linethick, iy - oy); //midverticalleft
                // sets field as the background of the program
                BackgroundImage = bm;
                // prevents the field from being repeated
                BackgroundImageLayout = ImageLayout.None;
            }
        }
        public void StoneCreation()
        {
            // creates all of the buttons / stones
            // uses the outer square as an orientation point
            int ox = 50;
            int oy = 50;
            int ol = 500;
            // creates all of the buttons in the respective positions in the array
            for (int z = 0; z <= 2; z++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    for (int x = 0; x <= 2; x++)
                    {
                        if (start)
                        {
                            // if there would be an error the button would be created in the upper-right corner
                            int locx = 0;
                            int locy = 0;
                            // creates button
                            buttons[x, y, z] = new PlayStone();
                            // writes the coordinates in custom attributes
                            buttons[x, y, z].X = x;
                            buttons[x, y, z].Y = y;
                            buttons[x, y, z].Z = z;
                            // obviously the buttons start as not part of a mill
                            buttons[x, y, z].Mill = false;
                            // adds the on click event
                            buttons[x, y, z].Click += (s, e) =>
                            {
                                // will set the ibtn variables to the xyz position of the buttons and calls the game
                                PlayStone thisButton = s as PlayStone;
                                ibtnx = thisButton.X;
                                ibtny = thisButton.Y;
                                ibtnz = thisButton.Z;
                                GameProgression();
                            };
                            // sets the position of the buttons according to their location within the array
                            // left side
                            if (x == 0)
                            {
                                locx = ox - 15;
                            }
                            // middle
                            else if (x == 1)
                            {
                                locx = ox + ol / 2 - 15;
                            }
                            // right side
                            else if (x == 2)
                            {
                                locx = ox + ol - 25;
                            }
                            // top
                            if (y == 0)
                            {
                                locy = oy - 15;
                            }
                            // middle
                            else if (y == 1)
                            {
                                locy = oy + ol / 2 - 15;
                            }
                            // bottom
                            else if (y == 2)
                            {
                                locy = oy + ol - 15;
                            }
                            // sets button size, appearance and location (standard is transparent for all of them)
                            buttons[x, y, z].Size = new Size(40, 40);
                            buttons[x, y, z].Location = new Point(locx, locy);
                            buttons[x, y, z].FlatStyle = FlatStyle.Flat;
                            buttons[x, y, z].FlatAppearance.BorderSize = 0;
                            buttons[x, y, z].BackColor = Color.Transparent;
                            buttons[x, y, z].FlatAppearance.MouseOverBackColor = Color.Transparent;
                            buttons[x, y, z].FlatAppearance.MouseDownBackColor = Color.Transparent;
                            // sets the location of the button as a name... for no apparent reason
                            //buttons[x, y, z].Text = Convert.ToString(buttons[x, y, z].X) + Convert.ToString(buttons[x, y, z].Y) + Convert.ToString(buttons[x, y, z].Z);
                            // if the buttons are none of the inner buttons they will be placed on the board
                            if (!(x == 1 && y == 1))
                            {
                                this.Controls.Add(buttons[x, y, z]);
                            }
                            /*else
                            {
                                buttons[x, y, z].BackColor = Color.Khaki;
                            }*/
                        }
                    }
                }
                // after each square it will move closer to the middle
                ox += 50;
                oy += 50;
                ol -= 100;
            }
        }
        public void SaveGame()
        {
            // Create SaveGame XML File if it does not exist otherwise load path
            if (selected)
            {
                MessageBox.Show("Can not save during movement!");
            }
            else
            {
                // load SaveGame File path
                string directory = Directory.GetCurrentDirectory();
                string save = directory + @"\muehlesavegame.xml";
                var a =
                    new XElement("root",
                        new XElement("turn"),
                        new XElement("boardstate"),
                        new XElement("gamestate")
                    );
                // create file if it does not exist
                if (!File.Exists(save))
                {
                    a.Save(save);
                }
                XDocument savegame = XDocument.Load(save);
                // Clear previous turn save
                savegame.Elements("root").Elements("turn").Elements().Remove();
                // Save current turn
                savegame.Elements("root").Elements("turn").FirstOrDefault().Add(new XElement("turnp1", new XAttribute("value", turnp1)));
                savegame.Elements("root").Elements("turn").FirstOrDefault().Add(new XElement("turnp2", new XAttribute("value", turnp2)));
                // Save boardstate (which players currently controls which buttons)
                int i = 0;
                savegame.Elements("root").Elements("boardstate").Elements().Remove();
                // saves buttons and their corresponding variables
                foreach (PlayStone playStone in buttons)
                {
                    if (playStone.BackColor == colorp1)
                    {
                        savegame.Elements("root").Elements("boardstate").FirstOrDefault().Add(new XElement($"stone", new XAttribute("player", "colorp1"), new XAttribute("ibtnx", playStone.X), new XAttribute("ibtny", playStone.Y), new XAttribute("ibtnz", playStone.Z)));
                    }
                    if (playStone.BackColor == colorp2)
                    {
                        savegame.Elements("root").Elements("boardstate").FirstOrDefault().Add(new XElement($"stone", new XAttribute("player", "colorp2"), new XAttribute("ibtnx", playStone.X), new XAttribute("ibtny", playStone.Y), new XAttribute("ibtnz", playStone.Z)));
                    }
                    i++;
                }
                // Save gamestate
                savegame.Elements("root").Elements("gamestate").Elements().Remove();
                // saves gameprogression
                savegame.Elements("root").Elements("gamestate").FirstOrDefault().Add(new XElement("gamestate", new XAttribute("start", start), new XAttribute("midgame", midgame), new XAttribute("placedstonesp1", placedstonesp1), new XAttribute("placedstonesp2", placedstonesp2)));
                savegame.Save(save);
            }
        }
        public void LoadGame()
        {
            string directory = Directory.GetCurrentDirectory();
            string save = directory + @"\muehlesavegame.xml";
            XDocument savegame = XDocument.Load(save);
            // Load Turn Data
            turnp1 = Convert.ToBoolean(savegame.Elements("root").Elements("turn").Elements("turnp1").Select(c => c.Attribute("value").Value).FirstOrDefault());
            turnp2 = Convert.ToBoolean(savegame.Elements("root").Elements("turn").Elements("turnp2").Select(c => c.Attribute("value").Value).FirstOrDefault());
            // Load Gamestate
            start = Convert.ToBoolean(savegame.Elements("root").Elements("gamestate").Elements("gamestate").Select(c => c.Attribute("start").Value).FirstOrDefault());
            midgame = Convert.ToBoolean(savegame.Elements("root").Elements("gamestate").Elements("gamestate").Select(c => c.Attribute("midgame").Value).FirstOrDefault());
            // Load how many stones the players have already placed down
            placedstonesp1 = Convert.ToInt32(savegame.Elements("root").Elements("gamestate").Elements("gamestate").Select(c => c.Attribute("placedstonesp1").Value).FirstOrDefault());
            placedstonesp2 = Convert.ToInt32(savegame.Elements("root").Elements("gamestate").Elements("gamestate").Select(c => c.Attribute("placedstonesp2").Value).FirstOrDefault());
            // Load Boardstate
            var elements = from ele in savegame.Elements("root").Elements("boardstate").Elements("stone")
                           where ele != null
                           select ele;
            foreach(var e in elements)
            {
                // Set stones of player1 to colorp1
                if(e.Attribute("player").Value == "colorp1")
                {
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].BackColor = colorp1;
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].FlatAppearance.MouseOverBackColor = colorp1;
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].FlatAppearance.MouseDownBackColor = colorp1;
                }
                // Set stones of player2 to colorp2
                if (e.Attribute("player").Value == "colorp2")
                {
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].BackColor = colorp2;
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].FlatAppearance.MouseOverBackColor = colorp2;
                    buttons[Convert.ToInt32(e.Attribute("ibtnx").Value), Convert.ToInt32(e.Attribute("ibtny").Value), Convert.ToInt32(e.Attribute("ibtnz").Value)].FlatAppearance.MouseDownBackColor = colorp2;
                }
            }
        }
        public void ResetGame()
        {
            // resets all necessary variables to default values
            midgame = false;
            selected = false;
            start = true;
            turnp1 = true;
            turnp2 = false;
            delete = 0;
            placedstonesp1 = 0;
            placedstonesp2 = 0;
            // removes all buttons which exist on the field
            foreach(PlayStone playStone in buttons)
            {
                this.Controls.Remove(playStone);
            }
            // creates all buttons anew
            StoneCreation();
            StoneCounter();
            // updates the turn info
            if (turnp1)
            {
                txtturn.Text = "Spieler 1";
            }
            else if (turnp2)
            {
                txtturn.Text = "Spieler 2";
            }
        }
        public Form1()
        {
            // loads various components on the start of the program
            InitializeComponent();
            BoardCreation();
            StoneCreation();
            this.BackColor = Color.FromArgb(255,229,170);
            // default start message
            txtturn.Text = "Spieler 1 beginnt!";
        }

        private void btnsavegame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void btnloadgame_Click(object sender, EventArgs e)
        {
            // resets the game then loads the last saved game
            ResetGame();
            LoadGame();
            StoneCounter();
            if (turnp1)
            {
                txtturn.Text = "Spieler 1";
            }
            else if (turnp2)
            {
                txtturn.Text = "Spieler 2";
            }
        }

        private void btnresetgame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
    
