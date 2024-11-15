using Raylib_cs;
using System;
using System.Drawing;
using System.Numerics;
using System.Threading;

namespace Game10003
{
    /// <summary>
    /// 
    ///     im going to use this tag "!dont forget!" so at the end of my project i can control f to search through my project for this tag
    /// </summary>
    public class Game
    {
        Vector2 mouseBrush = new Vector2(50, 10);
        Vector2 mousePosition = Vector2.Zero;
        Color colBrown = new Color(180, 100, 90);
        Color colUiBack = new Color(50, 20, 100);
        Color colIceBlue = new Color(179, 240, 245);
        Ice ice = new Ice();
        Debris[] obstacles = new Debris[20];
        BackGroundTile[] backGround = new BackGroundTile[150];
        bool mainMenu = true; // in general, if main menu = true show main menu screen and nothing else.
        bool startGame = false; // otherwise if startGame = true the game screen will show, otherwise the score screen will show.
        float backgroundSpeed = -3; // this is the speed the ice moves
        double distance = 0;
        string distanceUnits = ""; // distance unit (mm, cm, m, km);
        int money = 0;
        int upgradeIceSize = 1;
        int upgradeIceSpeed = 1;
        int upgradeBrushSize = 1;
        int upgradeBrushPower = 1;
       



        public void ReSetup()
        {
            distance = 0;
            backgroundSpeed = (float)-3.0  - ((float)upgradeIceSpeed- (float)1.0)/ (float)2.0;
            ice.size = 19 + upgradeIceSize*2;
            foreach (Debris deb in obstacles)
            {
                deb.DebrisDelete();
                deb.max = 20;
            }
            distanceUnits = "";
        }

        // function for hitboxes.
        public bool hitBoxDetect(Vector2 hitBoxPosition, Vector2 hitBoxSize, Vector2 hitboxPosition2, Vector2 hitboxSize2)
        {
            bool LeftOf;
            bool rightOf;
             LeftOf = hitBoxPosition.X < hitboxPosition2.X && hitBoxPosition.X + hitBoxSize.X > hitboxPosition2.X + hitboxSize2.X;
             rightOf = hitBoxPosition.Y < hitboxPosition2.Y && hitBoxPosition.Y + hitBoxSize.Y > hitboxPosition2.Y + hitboxSize2.Y;
            //}
            if (rightOf && LeftOf)
            {
                return (true);
            }
            return (false);
        }

        public double distanceCalculator(double newDist)
        {
            if (distance < 26.4583)
            { // with this the measurements are actually acurate.
                newDist = distance * 0.264583;
                distanceUnits = "mm";
                newDist = Math.Round(newDist, 0);
            }
            else if (distance <= 3779) // how many pixels are in a meter
            {
                newDist = distance * 0.264583;
                newDist = newDist / 10;
                distanceUnits = "cm";
                newDist = Math.Round(newDist, 0);
            }
            else if (distance <= 3779528) //how many pixels are in a km
            {
                newDist = distance * 0.264583;
                newDist = newDist / 10;
                newDist = newDist / 100;
                distanceUnits = "m";
                newDist = Math.Round(newDist, 2);
            }
            else
            {
                newDist = distance * 0.264583;
                newDist = newDist / 10;
                newDist = newDist / 100;
                newDist = newDist / 1000;
                distanceUnits = "km";
                newDist = Math.Round(newDist, 2);
            }
            return newDist;
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
                {  // the game will start
                    
                    foreach (BackGroundTile tile in backGround) // background tileing
                    {
                        tile.DrawTile();
                        tile.OffScreen();
                        tile.Move(-backgroundSpeed);
                    }

                    double distance2 = distanceCalculator(distance);
                    Text.Color = Color.OffWhite;
                    Text.Draw("$" + money.ToString(), 20, 20);
                    Text.Draw(distance2.ToString() + distanceUnits.ToString(), 20, 60);
                    Text.Color = Color.Black;
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
                        if(deb.DebrisCollide(mousePosition, mouseBrush.X / 3))
                        {
                            deb.MouseCollide(mousePosition.Y, upgradeBrushPower);
                        }
                        if (deb.DebrisCollide(ice.position, ice.size)) // this will check if the brush is touching the debris
                        {
                            ice.size -= (float)0.012 * deb.avgSize;
                            backgroundSpeed += (float)0.01 * deb.avgSize/ice.size;
                            deb.DebrisDelete();

                        }
                        deb.DebrisMove(backgroundSpeed);
                        Draw.LineColor = Color.Black;
                    }

                    backgroundSpeed = ice.iceSlow(backgroundSpeed);


                    //scoring
                    distance = distance -= backgroundSpeed;



                    if (ice.size < 3 || backgroundSpeed > -1.2-0.1*upgradeIceSpeed) // this checks when the game will stop. it slowly scales with your ice speed so the game dosen't feel slow.
                    {
                        startGame = false;; 
                        money += (int)Math.Round(distance / 21.0, 0); // the money is equal to the distance /50
                        distance = distanceCalculator(distance);
                        if (distanceUnits == "cm")
                        {
                            money += (int)Math.Round(distance / 21.0, 0);
                        }
                        if (distanceUnits == "m")
                        {
                            money += 20 * (int)Math.Round(distance / 21.0, 0);
                        }
                        else if (distanceUnits == "km")
                        {
                            money += 99999;
                            Console.WriteLine("I have no clue how youve done this but congrats!");
                        }
                        else
                        {
                            money += (int)Math.Round(distance / 21.0, 0); // the money is equal to the distance /50
                        }
                        
                    }



                }

                else
                { // this will draw the scoreboard
                    Text.Draw("Distance: "+distance.ToString()+ distanceUnits, 300, 100);
                    Text.Draw("$"+money.ToString(), 300, 200);


                    Draw.FillColor = Color.White;
                    BoxMaker(600, 100, 150, 70, "Play Again");


                    // upgrades
                    Text.Draw("Ice", 160, 430);


                    Text.Draw("Brush", 560, 430);


                    Text.Draw("Upgrades", 350, 390);


                    Text.Draw("$" + (upgradeIceSize* 100), 25, 480);
                    BoxMaker(25, 500, 150, 70, "Size");

                    Text.Draw("$" + (upgradeIceSpeed* 100), 200, 480);
                    BoxMaker(200, 500, 150, 70, "Speed");

                    Draw.Line(400, 450, 400, 600);

                    Text.Draw("$" + (upgradeBrushSize * 100), 425, 480);
                    BoxMaker(425, 500, 150, 70, "Size");

                    Text.Draw("$" + (upgradeBrushPower * 100), 600, 480);
                    BoxMaker(600, 500, 150, 70, "Power");
                    if (Input.IsMouseButtonPressed(MouseInput.Left))
                    {


                        if (hitBoxDetect(new Vector2(600, 100), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // restart button
                            startGame = true;
                            ReSetup();
                        }
                        else if(hitBoxDetect(new Vector2(25, 500), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // size
                            if (money >= 100 * upgradeIceSize)
                            {
                                money -= 100 * upgradeIceSize;
                                upgradeIceSize++;
                            }
                        }
                        if (hitBoxDetect(new Vector2(200, 500), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // speed
                            if (money >= 100 * upgradeIceSpeed)
                            {
                                money -= 100 * upgradeIceSpeed;
                                upgradeIceSpeed++;
                            }
                        }
                        if (hitBoxDetect(new Vector2(425, 500), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // size
                            if (money >= 100 * upgradeBrushSize)
                            {
                                money -= 100 * upgradeBrushSize;
                                upgradeBrushSize++;
                                mouseBrush = new Vector2(50 + upgradeBrushSize*5, 10 + upgradeBrushSize);
                            }
                        }
                        if (hitBoxDetect(new Vector2(600, 500), new Vector2(150, 70), mousePosition, Vector2.Zero))
                        { // power
                            if (money >= 100 * upgradeBrushPower)
                            {
                                money -= 100 * upgradeBrushPower;
                                upgradeBrushPower++;
                            }
                        }



                    }
                }
            }

        }
    }
}
