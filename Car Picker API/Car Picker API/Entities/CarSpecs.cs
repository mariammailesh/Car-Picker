using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class CarSpecs : ParentEntity
    {
        //can be joined with the Car entity using CarId
        public string EngineType { get; set; } // Engine type (e.g., "Petrol V6", "Electric", "Diesel")
        public float EngineCapacity { get; set; } // Engine capacity in liters (e.g., 2.0, 3.5)
        public short HorsePower { get; set; } // int not null
        public TransmissionType TransmissionType { get; set; } 
        public float performanceScore { get; set; } // Performance rating (e.g., 0-100 scale)
        public FuelType FuelType { get; set; } // nvarchar(max) not null
        public short SeatingCapacity { get; set; } // int not null
        public int CarId { get; set; } // Foreign Key to Car entity
    }
    
}
