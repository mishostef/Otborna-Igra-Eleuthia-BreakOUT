﻿

namespace OtbornaIgra.GameEngines
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
    public class GameEngine
    {
        public const int padWidht=150;
        public const int padHeight=15;
        private IRenderer renderer;
        private const double timerFramesIntervalInMiliSeconds=10;

        public GameObjects Pad { get; set; }

        public GameEngine(IRenderer Renderer)
        {
            this.renderer = Renderer;
            this.renderer.presingkey += HandleKeyPressed;
        }

        private void HandleKeyPressed(object sender, KeyDownEventArgs key)
        {
            if (key.Command == GameComand.MoveLeft)
            {
                var left = this.Pad.Position.Left - 15;
                var top = this.Pad.Position.Top;
                this.Pad.Position = new Position(left,top );
            }

            else if (key.Command == GameComand.MoveRight)
            {
                var right = this.Pad.Position.Left + 15;
                var top = this.Pad.Position.Top;
                this.Pad.Position = new Position(right, top);
            }

            else if (key.Command == GameComand.Fire)
            {
            }

               

        }

        internal void InitGame()
        {
            this.Pad = new PadGameObject() {
                Position =new Position((this.renderer.ScreenWidth)/2,((this.renderer.ScreenHeight)-padHeight*5))
                ,Bounds=new Size(padWidht,padHeight)};
        }

        internal void StartGame()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(timerFramesIntervalInMiliSeconds);
            timer.Tick += (sender, args) =>
             {
                 this.renderer.Clear();
                 this.renderer.Draw(this.Pad);

             };
            timer.Start();
    
        }
    }
}