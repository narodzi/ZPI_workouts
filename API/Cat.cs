using System.ComponentModel.DataAnnotations;

namespace CatNamespace {
    public class Cat {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Race { get; set; } = String.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Color { get; set; } = String.Empty;

        private Cat() {}

        public Cat(int id, string name, string race, int age, string color) {
            Id = id;
            Name = name;
            Race = race;
            Age = age;
            Color = color;
        }
    }
}