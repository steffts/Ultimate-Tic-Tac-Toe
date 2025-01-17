using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using System.Collections.Generic;
using MauiApp1.Models;

namespace MauiApp1.Data
{
    public class GameDatabase
    {
        private readonly SQLiteConnection _database;

        public GameDatabase(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);

            _database.CreateTable<PlayerState>();
        }


        public void SavePlayerState(PlayerState player)
        {
            _database.Insert(player);
        }

        public List<PlayerState> GetPlayerStates()
        {
            return _database.Table<PlayerState>().ToList();
        }

    }

}
