// Include code libraries you need below (use the namespace).
using Raylib_cs;
using System;
using System.Drawing;
using System.Numerics;
using System.Threading;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    ///     im going to use this tag "!dont forget!" so at the end of my project i can control f to search through my project for this tag
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Vector2 mouseBrush = new Vector2(50, 10);
        //Debris deb = new Debris(); // will probably change into an array
        Color colBrown = new Color(180, 100, 90);
        Ice ice = new Ice();
        Color colUiBack = new Color(50, 20, 100);
        Debris[] obstacles = new Debris[20];
        Vector2 mousePosition = Vector2.Zero;
        Color colIceBlue = new Color(179, 240, 245);
        BackGroundTile[] backGround = new BackGroundTile[150];
        bool mainMenu = true; // in general, if main menu = true show main menu screen and nothing else.
        bool startGame = false; // otherwise if startGame = true the game screen will show, otherwise the score screen will show.
        float backgroundSpeed = -3;
        double distance = 0;
        int money = 0;
        string distanceUnits = "mm";

        int upgradeIceSize = 0;
        // function for hitboxes.
        public bool hitBoxDetect(Vector2 hitBoxPosition, Vector2 hitBoxSize, Vector2 hitboxPosition2, Vector2 hitboxSize2)
        {
            bool LeftOf;
            bool rightOf;
            //if (centered)
            //{
            //    LeftOf = hitBoxPosition.X - hitBoxSize.X  < hitboxPosition2.X - hitboxSize2.X && hitBoxPosition.X + hitBoxSize.X> hitboxPosition2.X + hitboxSize2.X;
            //    rightOf = hitBoxPosition.Y - hitBoxSize.Y < hitboxPosition2.Y - hitboxSize2.Y && hitBoxPosition.Y + hitBoxSize.Y > hitboxPosition2.Y + hitboxSize2.Y;
            //}
            //else
            //{
             LeftOf = hitBoxPosition.X < hitboxPosition2.X && hitBoxPosition.X + hitBoxSize.X > hitboxPosition2.X + hitboxSize2.X;
             rightOf = hitBoxPosition.Y < hitboxPosition2.Y && hitBoxPosition.Y + hitBoxSize.Y > hitboxPosition2.Y + hitboxSize2.Y;
            //}
            if (rightOf && LeftOf)
            {
                return (true);
            }
            return (false);
        }


        public void BoxMaker(float boxPositionX, float boxPositionY, float boxSizeX, float boxSizeY, string boxText)
        {

            Draw.Rectangle(boxPositionX, boxPositionY, boxSizeX, boxSizeY);
            
            Text.Draw(boxText, boxPositionX, boxPositionY+boxSizeY/3);
        }

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            
            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i] = new Debris();
            }

            
            for (int yPos = 0; yPos < 15; yPos++)
            { // this sets up the tile and goes through each row.
                for (int xPos = 0; xPos < 10; xPos++)
                {
                    backGround[xPos + yPos * 10] = new BackGroundTile();
                    int xOffset = 0;
                    if (yPos %2 == 0)
                    {
                        xOffset = -70; // this is how offset the tiles are.
                    }
                    backGround[xPos + yPos * 10].position = new Vector2(xPos * 140+xOffset, yPos * 40);

                }
            }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            //drawes ui
            //Draw.FillColor = colUiBack;
            //Draw.Rectangle(0, 0, 200, 600);


            mousePosition = Input.GetMousePosition();
            if (mainMenu)
            {
                Draw.FillColor = Color.OffWhite;
                Text.Size = 25;
                BoxMaker(300, 200, 200, 150, "Start The Game!");
                if (Input.IsMouseButtonPressed(MouseInput.Left))
                {

                    if (mousePosition.X > 350 && mousePosition.Y > 250 && mousePosition.X < 500 && mousePosition.Y < 350)
                    {
                        startGame = true;
                        mainMenu = false;
                    }

                }
            }

            else
            {


                if (startGame)
                {
                    // background tileing
                    foreach (BackGroundTile tile in backGround)
                    {

                        tile.DrawTile();
                        tile.OffScreen();
                        tile.Move(-backgroundSpeed);
                    }


                    ice.IceDraw(backgroundSpeed, colIceBlue);
                    // this will draw a brush on the mouse
                    Draw.LineSize = 2;
                    
                    Draw.LineColor = Color.OffWhite;
                    for (int i = -5; i< 5; i++)
                    {
                        Draw.Line(mousePosition.X - mouseBrush.X /3/ i, mousePosition.Y -mouseBrush.Y+3, mousePosition.X + mouseBrush.X / 3 / i, mousePosition.Y + mouseBrush.Y-3); // a mess
                    }
                    Draw.LineColor = Color.Black;
                    Draw.FillColor = colBrown;
                    Draw.Ellipse(mousePosition, mouseBrush);
                    
                    Draw.LineSize = 1;
                    
                    // this checks the debris collisions
                    foreach (Debris deb in obstacles)
                    { // thank you forEach!!!
                      if (deb.DebrisCollide(mousePosition, mouseBrush.X / 3)) // will need to be opomised later maybe
                        
                        {
                            deb.MouseCollide(mousePosition.Y);
                        }
                        if (deb.DebrisCollide(ice.position, ice.size)) // this will check if the brush is touching the debris
                        {
                            Console.WriteLine("damage"); // !dont forget! to comment out
                            ice.size -= (float)0.01 * deb.avgSize;
                            backgroundSpeed += (float)0.001 * deb.avgSize;
                            deb.DebrisDelete();

                        }
                        deb.DebrisMove(backgroundSpeed);
                    }

                    backgroundSpeed = ice.iceSlow(backgroundSpeed);


                    //scoring
                    distance = distance -= backgroundSpeed;



                    if (ice.size < 2 || backgroundSpeed > -1 || Input.IsKeyboardKeyDown(KeyboardInput.L))//!dont forget! to comment out the debug input
                    {
                        Console.WriteLine("You Lose!!");
                        startGame = false;;
                        money += (int)Math.Round(distance / 75.0, 0); // the money is equal to the distance /50
                        
                        if (distance < 1000)
                        {
                            distanceUnits = "mm";
                        }
                        else if (distance >= 1000)
                        {
                            distance = distance / 10;
                            distanceUnits = "cm";
                        }
                        else if (distance >= 10000)
                        {
                            distance = distance / 1000;
                            distanceUnits = "m";
                        }
                        distance = Math.Round(distance, 2);
                    }



                }

                else
                { // this will draw the scoreboard
                    


                    Text.Draw("Distance: "+distance.ToString()+ distanceUnits, 300, 100);
                    Text.Draw(money.ToString(), 300, 200);


                    Draw.FillColor = Color.White;
                    BoxMaker(600, 100, 150, 70, "Play Again");


                    // upgrades
                    Text.Draw("$" + (upgradeIceSize + 1 * 100), 25, 480);
                    BoxMaker(25, 500, 150, 70, "Size");

                    Text.Draw("$" + (upgradeIceSize + 1 * 100), 200, 480);
                    BoxMaker(200, 500, 150, 70, "Size");

                    Text.Draw("$" + (upgradeIceSize + 1 * 100), 425, 480);
                    BoxMaker(425, 500, 150, 70, "Size");

                    Text.Draw("$" + (upgradeIceSize + 1 * 100), 600, 480);
                    BoxMaker(600, 500, 150, 70, "Size");
                    if (Input.IsMouseButtonDown(MouseInput.Left))
                    {


                        if (hitBoxDetect(new Vector2(500, 200), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // restart button
                            startGame = true;
                            distance = 0;
                        }
                        if(hitBoxDetect(new Vector2(100, 500), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        {

                        }



                    }
                }
            }

        }
    }
}
