using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Wargame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Soldiers Player1;
        Soldier Player2;//skapa två spelare/tanks
        GameObj Bana; //Skapa ett GameObj som ska användas som bana
        SpriteFont font; //Spritefont för utskrift
        Meter  Player1LifeMeter, Player2LifeMeter; //Mätare för liv och kraft

        List<Shot> allShots = new List<Shot>(); //Ny lista för alla skott(-objekt)
        Texture2D shot1Gfx; //Grafik till skotten

        List<Bloodsplat> allExplosions = new List<Bloodsplat>(); //Ny lista för alla explosioner
        Texture2D explosionGfx; //Grafik till explosionen
        string displayMessage = ""; //Text som skall skrivas ut

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Ladar in grafiken
            Texture2D greenGrafik = Content.Load<Texture2D>("Sanic");
            Texture2D pinkGrafik = Content.Load<Texture2D>("Sanic2");
            Texture2D level1Gfx = Content.Load<Texture2D>("level");
            Texture2D redMeterGfx = Content.Load<Texture2D>("meter");
            Texture2D lifeMeterGfx = Content.Load<Texture2D>("life_meter");
            shot1Gfx = Content.Load<Texture2D>("shot");
            explosionGfx = Content.Load<Texture2D>("16_sunburn_spritesheet");

            //Skapa Objekt
            //Tilldelning av properties direkt.. slipper skriva konstruktorer
            Player1 = new Soldiers()
            {
                Grafik = greenGrafik,
                Position = new Vector2(400, 300),
                Speed = 0,
                Direction = new Vector2(0, -1),
                Enemy = false
            };
            Player2 = new Soldier()
            {
                Grafik = pinkGrafik,
                Position = new Vector2(500, 400),
                Speed = 0,
                Direction = new Vector2(0, -1),
                Enemy = true
            };
            Bana = new GameObj() { Grafik = level1Gfx, Position = new Vector2(0, 0), Angle = 0 };
           
            Player1LifeMeter = new Meter() { Grafik = lifeMeterGfx, Position = new Vector2(50, 50) };
            Player2LifeMeter = new Meter() { Grafik = lifeMeterGfx, Position = new Vector2(250, 50) };
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Player1.Update(gameTime);   //Uppdatera spelare1 
            Player2.Update(gameTime);
            //Sätter Kraftmätarens värde till vår spelares "skottkraft"
           
            //Sätter Livmätarnas värde till resp. spelares liv-värde
            Player1LifeMeter.Value = Player1.Life;
            Player2LifeMeter.Value = Player2.Life;

            if (Player1.ShotFired) //Ifall ett skott avfyrats
            {
                //Lägger till ett nytt skott i listan
                allShots.Add(new Shot()
                {
                    Grafik = shot1Gfx,
                    Position = Player1.Position + (Player1.Direction * (Player1.Grafik.Height) / 2) +
                                (Player1.Direction * shot1Gfx.Height / 2),
                    Angle = Player1.Angle,
                    Speed = 5.0F + Player1.Speed,
                    Power = Player1.ShotPower,
                    Direction = Player1.Direction
                });
                Player1.ShotPower = 0;
                Player1.ShotFired = false;
            }
            if (Player2.ShotFired) //Ifall ett skott avfyrats
            {
                //Lägger till ett nytt skott i listan
                allShots.Add(new Shot()
                {
                    Grafik = shot1Gfx,
                    Position = Player2.Position + (Player2.Direction * (Player2.Grafik.Height) / 2) +
                                (Player2.Direction * shot1Gfx.Height / 2),
                    Angle = Player2.Angle,
                    Speed = 5.0F + Player2.Speed,
                    Power = Player2.ShotPower,
                    Direction = Player2.Direction
                });
                Player2.ShotPower = 0;
                Player2.ShotFired = false;
            }
            for (int i = 0; i < allShots.Count; i++) //Loopar igenom alla skott
            {
                allShots[i].Update(gameTime); //uppdaterar skott
                if (allShots[i].Power < 0) //Är skottets "kraft" slut?
                {
                    //Lägg till en ny Explosion
                    allExplosions.Add(new Bloodsplat()
                    { Grafik = explosionGfx, Position = allShots[i].Position });
                    allShots.RemoveAt(i); //Tar bort skottet
                }
            }
            for (int i = 0; i < allExplosions.Count; i++) //Loopa igenom alla explosioner
            {
                if (Player2.CheckCollision(allExplosions[i]))
                {
                    Player2.Life -= 1;

                }
                if (Player1.CheckCollision(allExplosions[i]))
                {
                    Player1.Life -= 1;

                }
                allExplosions[i].Update(gameTime); //Uppdatera explosion
                //Ta bort "färdiga" explosioner
                if (allExplosions[i].Active == false) allExplosions.RemoveAt(i);
            }

            if (Player1.CheckCollision(Player2))
            {
                if (Player1.Speed < 1.0 && Player1.Speed < 1.0)
                {
                    Player1.Speed = 0;
                    Player2.Speed = 0;
                    Player1.Position += Player2.Direction * Player2.Speed;
                    Player2.Position += Player1.Direction * Player1.Speed;
                }
                else
                {
                    if (Player1.Speed > Player2.Speed)
                    {
                        Player2.Life -= 5;
                        Player1.Speed = 0F;
                        Player2.Position = Player2.Position + Player1.Direction * 10F;
                    }
                    else
                    {
                        if (Player1.Speed == Player2.Speed)
                        {
                            Player2.Life -= 5;
                            Player1.Speed = 0F;
                            Player2.Position = Player2.Position + Player1.Direction * 10F;
                            Player1.Life -= 5;
                            Player2.Speed = 0F;
                            Player1.Position = Player1.Position + Player2.Direction * 10F;
                        }
                        else
                        {
                            Player1.Life -= 5;
                            Player2.Speed = 0F;
                            Player1.Position = Player1.Position + Player2.Direction * 10F;
                        }
                    }
                }
            }
            
            if (Player2.Life < 0)
            {
                Player1.Kills++;
                Player2.Respawn();
            }
            if (Player1.Life < 0)
            {
                Player2.Kills++;
                Player1.Respawn();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //Ritar ut bana, spelare och mätare
            Bana.Draw(spriteBatch, Player1.Position);
            Player1.Draw(spriteBatch, Player1.Position);
           
            
            Player2.Draw(spriteBatch, Player1.Position);
            


            //Loopar igenom alla skott och ritar ut dem
            for (int i = 0; i < allShots.Count; i++)
            {
                allShots[i].Draw(spriteBatch, Player1.Position);
            }
            //Loopar igenom alla explosioner och ritar ut dem
            for (int i = 0; i < allExplosions.Count; i++)
            {
                allExplosions[i].Draw(spriteBatch, Player1.Position);
            }
            //Skriver ut spelarnas namn och liv mm..
            string nameFormat = "{0}\nLife: {1}%\n\nKills: {2}";
            displayMessage = string.Format(nameFormat, "Player1", Player1.Life, Player1.Kills);
            spriteBatch.DrawString(font, displayMessage, new Vector2(51, 4), Color.Black);
            spriteBatch.DrawString(font, displayMessage, new Vector2(50, 5), Color.White);
            displayMessage = string.Format(nameFormat, "Player2", Player2.Life, Player2.Kills);
            spriteBatch.DrawString(font, displayMessage, new Vector2(251, 4), Color.Black);
            spriteBatch.DrawString(font, displayMessage, new Vector2(250, 5), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
