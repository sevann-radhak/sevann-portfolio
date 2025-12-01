using Portfolio.Domain.Constants;

namespace Portfolio.Domain.Entities;

public class Skill
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public SkillCategory Category { get; private set; }
    public SkillLevel Level { get; private set; }
    public int YearsOfExperience { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private Skill() { }

    public Skill(Guid id, string name, SkillCategory category, SkillLevel level, int yearsOfExperience)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(string.Format(ErrorMessages.IdCannotBeEmpty, FieldNames.SkillId), nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.SkillName), nameof(name));

        if (yearsOfExperience < 0)
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNegative, FieldNames.YearsOfExperience), nameof(yearsOfExperience));

        Id = id;
        Name = name;
        Category = category;
        Level = level;
        YearsOfExperience = yearsOfExperience;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.SkillName), nameof(newName));

        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLevel(SkillLevel newLevel)
    {
        Level = newLevel;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateYearsOfExperience(int years)
    {
        if (years < 0)
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNegative, FieldNames.YearsOfExperience), nameof(years));

        YearsOfExperience = years;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum SkillCategory
{
    ProgrammingLanguage = 0,
    Framework = 1,
    Database = 2,
    Cloud = 3,
    DevOps = 4,
    Frontend = 5,
    Backend = 6,
    Other = 7
}

public enum SkillLevel
{
    Beginner = 0,
    Intermediate = 1,
    Advanced = 2,
    Expert = 3
}

