﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using HSPA_TEST.DAL.Models.Authentication;

namespace HSPA_TEST.DAL.Models
{
    public class Property
    {
        public int SellRent { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public Guid FurnishingTypeId { get; set; }
        public FurnishingType FurnishingType { get; set; }
        public int Price { get; set; }
        public int BHK { get; set; }
        public int BuiltArea { get; set; }

        public bool ReadyToMove { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloors { get; set; }
        public string MainEntrance { get; set; }
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
