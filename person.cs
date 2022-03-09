﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace App39
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Subscribed { get; set; }
    }
}
