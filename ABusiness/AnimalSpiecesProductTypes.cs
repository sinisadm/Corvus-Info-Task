//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABusiness
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnimalSpiecesProductTypes
    {
        public int Id { get; set; }
        public int AnimalSpiecesId { get; set; }
        public int AnimalProductTypesId { get; set; }
        public string Description { get; set; }
        public Nullable<int> ProductionIntervalsId { get; set; }
        public Nullable<int> FirstPossibleAbuseInDays { get; set; }
        public Nullable<int> NextAbusePeriodInDays { get; set; }
        public Nullable<int> MissAbusePenality { get; set; }
        public Nullable<int> MaxPeriodProduction { get; set; }
        public Nullable<decimal> DailyProductionDecreaseIndex { get; set; }
        public int Status { get; set; }
    
        public virtual AnimalProductTypes AnimalProductTypes { get; set; }
        public virtual AnimalSpecies AnimalSpecies { get; set; }
        public virtual EntityStatuses EntityStatuses { get; set; }
        public virtual ProductionIntervals ProductionIntervals { get; set; }
    }
}
