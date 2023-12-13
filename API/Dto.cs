using CatNamespace;

namespace Dtos {
    public class CatDTO {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Race { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Color { get; set; } = String.Empty;

        public static implicit operator Cat(CatDTO cDTO) => new(
            cDTO.Id,
            cDTO.Name,
            cDTO.Race,
            cDTO.Age,
            cDTO.Color
        );

        public static implicit operator CatDTO(Cat c) => 
        new CatDTO() {
            Id = c.Id,
            Name = c.Name,
            Race = c.Race,
            Age = c.Age,
            Color = c.Color
        };
    }
}