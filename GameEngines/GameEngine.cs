﻿namespace OtbornaIgra.GameEngines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Renderers;
    using Interfaces;
    using GameObjects;
    using Misc;
    using System.Windows.Threading;
    using System.Threading;
    using Global;

    public class GameEngine
    {
        

        private IRenderer renderer;

        public IGameObject Pad { get; set; }

        public IMovable Ball { get; set; }

        public IList<IGameObject> Bricks { get; set; }

        public GameEngine(IRenderer Renderer)
        {  //zaka4am se za eventa presingkey i pri vsqko vikane na presingkey se vika handlekeypressed
            this.renderer = Renderer;
            this.renderer.presingkey += HandleKeyPressed; 
        }

        private void HandleKeyPressed(object sender, KeyDownEventArgs key)
            //metoda koito e zaka4en za eventa presingkey
        {
            if (key.Command == GameComand.MoveLeft)
            {
                var left = this.Pad.Position.Left - 15;
                var top = this.Pad.Position.Top;
                var newPositon = new Position(left, top);
                if (this.renderer.isInBounds(newPositon))
                 {
                    this.Pad.Position = newPositon;
                 }
            }

            else if (key.Command == GameComand.MoveRight)
            {
                var right = this.Pad.Position.Left + 15;
                var top = this.Pad.Position.Top;
                var newPositon = new Position(right, top);
                if (this.renderer.isInBounds(newPositon))
                {
                    this.Pad.Position = newPositon;
                }
                
            }

            else if (key.Command == GameComand.Fire)
            {
            }
        }

        internal void InitGame()
        {
            int initPadLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initPadTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight * GlobalConstants.distanceFromBottomRowPad);

            this.Pad = new PadGameObject()
            {
                Position = new Position(initPadLeftPosition, initPadTopPosition),   
                Bounds = new Size(GlobalConstants.padWidth, GlobalConstants.padHeight)
            };

            int initBallLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initBallTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight * (GlobalConstants.distanceFromBottomRowPad) - GlobalConstants.padHeight);

            this.Ball = new BallGameObject()
            {
                Position = new Position(initBallLeftPosition,
                (initBallTopPosition)),
                Bounds = new Size(GlobalConstants.ballSize, GlobalConstants.ballSize)
            };

            int initBrickLeftPosition = 50;
            int initBrickTopPosition = 50;
            this.Bricks = new List<IGameObject>();
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Bricks.Add(new BricksGameObject()
                    {
                        Position = new Position(initBrickLeftPosition + i * GlobalConstants.brickWidth * 2,
                    (initBrickTopPosition+j* GlobalConstants.brickHright * 2)),
                        Bounds = new Size(GlobalConstants.brickWidth, GlobalConstants.brickHright)
                    });
                }
            }

        }

        internal void StartGame()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GlobalConstants.timerFramesIntervalInMiliSeconds);
            timer.Tick += (sender, args) =>
             {
                 this.renderer.Clear();
                 int currentBallPositionLeft = this.Ball.Position.Left;
                 int currentBallPositionTop = this.Ball.Position.Top;
                 this.Ball.Move(currentBallPositionLeft - 2, currentBallPositionTop - 3);
                
                 this.renderer.Draw(this.Pad, this.Ball);
                 foreach (var brick in this.Bricks)
                 {   
                     this.renderer.Draw(brick);
                 }

             };
            timer.Start();

        }
    }
}
