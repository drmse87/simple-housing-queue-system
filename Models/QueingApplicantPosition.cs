using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{   
    public class QueingApplicantPosition
    {
        public Int64? PlaceInQueue { get; set; }
    }
}