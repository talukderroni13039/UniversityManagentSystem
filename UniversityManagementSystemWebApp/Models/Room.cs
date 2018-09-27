using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNo { get; set; }

        public List<ClassRoomAllocation> ClassRoomAllocations { get; set; }
    }
}