namespace Portfolio.Domain.Constants;

public static class ErrorMessages
{
    public const string IdCannotBeEmpty = "{0} ID cannot be empty";
    public const string CannotBeNullOrEmpty = "{0} cannot be null or empty";
    public const string CannotBeNegative = "{0} cannot be negative";
    public const string InvalidFormat = "Invalid {0} format";
    public const string MustBeAtLeastCharacters = "{0} must be at least {1} characters";
    public const string CannotExceedCharacters = "{0} cannot exceed {1} characters";
    public const string EndDateBeforeStartDate = "End date cannot be before start date";
    public const string AlreadyCompleted = "{0} is already completed";
    public const string AlreadyActive = "{0} is already active";
    public const string OnlyCompletedCanBeReopened = "Only completed items can be reopened";
    public const string ProjectNotActive = "Project is not active";
    public const string ProjectAlreadyArchived = "Project is already archived";
}

public static class FieldNames
{
    public const string ProjectId = "Project ID";
    public const string SkillId = "Skill ID";
    public const string ExperienceId = "Experience ID";
    public const string ProjectName = "Project name";
    public const string SkillName = "Skill name";
    public const string Company = "Company name";
    public const string Position = "Position";
    public const string Description = "Description";
    public const string RepositoryUrl = "Repository URL";
    public const string EmailAddress = "Email address";
    public const string Url = "URL";
    public const string YearsOfExperience = "Years of experience";
    public const string Task = "Task";
    public const string Project = "Project";
}

