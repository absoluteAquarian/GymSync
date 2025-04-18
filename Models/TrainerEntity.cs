﻿using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class TrainerEntity : IQueryKeyable<TrainerEntity, int>
    {
        [Key]
        public int trainer_id { get; set; }

        static Expression<Func<TrainerEntity, int>> IQueryKeyable<TrainerEntity, int>.GetPrimaryKey() {
            return t => t.trainer_id;
        }
    }
}
