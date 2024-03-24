﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog
    {
            public DateTime Date { get; set; }
            public float Duration { get; set; }
            public float Distance { get; set; }
            public string Comment { get; set; }
            public int Difficulty { get; set; }
            public int Rating { get; set; }

            public TourLog(DateTime date, float duration, float distance)
            {
                Date = date;
                Duration = duration;
                Distance = distance;
            }
    }
}