using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Models
{
    public class PlayerState
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Wins { get; set; }
    }
}


