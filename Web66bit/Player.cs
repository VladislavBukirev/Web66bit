using System.ComponentModel.DataAnnotations;

public class Player
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Имя")]
    public string Name { get; set; }
    [Display(Name = "Фамилия")]
    public string Surname { get; set; }
    [Display(Name = "Пол")]
    public string Gender { get; set; }
    [Display(Name = "Дата рождения")]
    public string BirthDate { get; set; }
    [Display(Name = "Название команды")]
    public string TeamName { get; set; }
    [Display(Name = "Страна")]
    public string Country { get; set; }

    public Player(string name, string surname, string gender, string birthDate, string teamName, string country)
    {
        this.Name = name;
        this.Surname = surname;
        this.Gender = gender;
        this.BirthDate = birthDate;
        this.TeamName = teamName;
        this.Country = country;
    }

    public Player() { }
}