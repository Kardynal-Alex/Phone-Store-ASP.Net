using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.DataModel
{
    public class ProductInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter ScreenResolution Information")]
        public string ScreenResolution { get; set; }
        [Required(ErrorMessage = "Enter Camera Information")]
        public string Camera { get; set; }
        [Required(ErrorMessage = "Enter CPU information")]
        public string CPU { get; set; }
        [Required(ErrorMessage = "Enter Number of cores")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int NumberOfCores { get; set; }
        [Required(ErrorMessage = "Enter RAM Size information")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int RAMSize { get; set; }
        [Required(ErrorMessage = "Enter Built-In Memory")]
        [Range(1, 1001, ErrorMessage = "Out of range")]
        public int BuiltInMemory { get; set; }
        [Required(ErrorMessage = "Enter OS")]
        public string OS { get; set; }
        [Required(ErrorMessage = "Enter BatteryCapacity")]
        [Range(1, 10000, ErrorMessage = "Out of range")]
        public int BatteryCapacity { get; set; }

        public Product Product { get; set; }
    }
}
