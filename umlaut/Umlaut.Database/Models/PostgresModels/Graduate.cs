using System.ComponentModel.DataAnnotations;

namespace Umlaut.Database.Models.PostgresModels
{
    public class Graduate
    {
        //Id выпускника
        public int Id { get; set; }

        //Внешний ключ на город выпусника
        [Required]
        public Location Location { get; set; }

        //Внешний ключ на факультет выпусника
        [Required]
        public Faculty Faculty { get; set; }

        //Внешний ключ на специализации выпусника
        public List<Specialization> Specializations { get; set; }

        //Пол
        public string? Gender { get; set; }

        //Возраст
        public int Age { get; set; }

        //Вакансия
        public string? Vacation { get; set; }

        //Ожидаемая зарплата
        public int ExpectedSalary { get; set; }

        //Год окончания обучения
        public int YearGraduation { get; set; }

        //Стаж
        public int Experience { get; set; }

        //Ссылка на резюме
        public string? ResumeLink { get; set; }

    }
}