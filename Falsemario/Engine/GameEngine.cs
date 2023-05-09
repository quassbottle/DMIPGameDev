using System;
using System.Collections.Generic;
using Falsemario.Engine.Graphics;
using Falsemario.Engine.Utils;
using System.Linq;
using System.Threading.Tasks;
using Falsemario.Engine;
using DMIPSpaceInvaders.Game.Utils;
using Falsemario.Engine.Basic;
using Falsemario.Game.Entites;

namespace DMIPSpaceInvaders.Game
{
    public class GameEngine
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private Blackboard _blackboard;

        private Timer _updater = new Timer();

        public int Score { get; set; } = 0;
        
        public GameEngine(int width, int height)
        {
            _blackboard = new Blackboard(width, height);
        }

        public Blackboard GetGraphics()
        {
            return _blackboard;
        }

        public List<GameObject> GetAllIf(Predicate<GameObject> pr)
        {
            var list = new List<GameObject>();
            foreach (var obj in _gameObjects.Copy())
            {
                if (pr.Invoke(obj))
                    list.Add(obj);
            }

            return list;
        }

        public void CreateInstance(GameObject obj)
        {
            obj.Init();
            _gameObjects.Add(obj);
        }

        public void DisposeObject(GameObject obj)
        {
            obj.Deinit();
            _gameObjects.Remove(obj);
        }

        public void Start()
        {
            _updater.Delay = 50;
            _updater.OnTick += _updater_OnTick;
            _updater.Start();
        }

        public void Stop()
        {
            _updater.Stop();
        }

        public bool IsRunning() => _updater.IsRunning;

        private async void _updater_OnTick(object invoker)
        {
            _blackboard.EraseAll();
            foreach (var gameObject in _gameObjects.Copy())
            {
                await Task.Run(() =>
                {
                    gameObject?.Update(this);
                });
            }
            foreach (var drawable in GetSortedDrawables())
            {
                if (drawable is ICollidable mov && drawable is IDrawable drw)
                {
                    _blackboard.DrawSprite(drw.GetSprite(), mov.GetCollider().Location);
                }
            }
            _blackboard.DrawAll();
        }

        public bool ObjectExists(GameObject obj) => _gameObjects.Contains(obj);

        public void DrawManually(GameObject obj)
        {
            if (obj is ICollidable mov && obj is IDrawable drw)
            {
                _blackboard.DrawSprite(drw.GetSprite(), mov.GetCollider().Location);
            }
            _blackboard.DrawAll();
        }

        public void UpdateManually(GameObject obj) => obj.Update(this);

        private List<GameObject> GetSortedDrawables()
        {
            return _gameObjects.FindAll(obj => obj is IDrawable)
                .OrderBy(obj => ((IDrawable) obj).GetSprite().Layer).ToList();
        }

        public GameObject GetByLocation(object caller, Point2D p)
        {
            var a =  _gameObjects.FindAll(obj => obj is ICollidable c && (
                c.GetCollider().X == p.X && c.GetCollider().Y == p.Y));
            foreach (var o in a)
            {
                if (o != caller)
                    return o;
            }
            return null;
        }
    }
}