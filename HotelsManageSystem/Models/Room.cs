﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsManageSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
