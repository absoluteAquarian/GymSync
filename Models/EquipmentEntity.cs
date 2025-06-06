﻿using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class EquipmentEntity : IQueryKeyable<EquipmentEntity, int>
    {
        [Key]
        public int equipment_id { get; set; }
        public bool in_use { get; set; }
        public string location_name { get; set; }
		public DateTime last_maintenance { get; set; }

        static Expression<Func<EquipmentEntity, int>> IQueryKeyable<EquipmentEntity, int>.GetPrimaryKey() {
            return e => e.equipment_id;
        }
    }
}
