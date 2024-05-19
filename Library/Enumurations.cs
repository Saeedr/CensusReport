using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class Enumurations
    {
        public enum Status
        {
            Disabled = 0, Enabled = 1
        }

        public enum BaseInfoField
        {
            FamilyType = 1,
            PopulationType = 2,
            RegionStatus = 3,
            OwnerShipStatus = 4,
            Nationality = 5,
            Gender = 6,
            RelationShip = 7,
            InhabitancyStatus = 8,
            MaritalStatus = 9,
            EducationStatus = 10,
            ActivityStatus = 11,
            JobType = 12,
            InsuranceFirst = 13,
            InsuranceSecond = 13
        }
    }
}