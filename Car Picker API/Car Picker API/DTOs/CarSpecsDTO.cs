using Car_Picker_API.Entities;

namespace Car_Picker_API.DTOs
{
    public class CarSpecsDTO
    {
        public string EngineType { get; set; }
        public float EngineCapacity { get; set; }
        public short HorsePower { get; set; }
        public string TransmissionType { get; set; }
        public float PerformanceScore { get; set; }
        public string FuelType { get; set; }
        public short SeatingCapacity { get; set; }
      

    }
}
