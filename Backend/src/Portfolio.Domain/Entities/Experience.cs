using Portfolio.Domain.Constants;

namespace Portfolio.Domain.Entities;

public class Experience
{
    public Guid Id { get; private set; }
    public string Company { get; private set; } = string.Empty;
    public string Position { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsCurrent { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Experience() { }

    public Experience(Guid id, string company, string position, string description, DateTime startDate, DateTime? endDate = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(string.Format(ErrorMessages.IdCannotBeEmpty, FieldNames.ExperienceId), nameof(id));
        }

        if (string.IsNullOrWhiteSpace(company))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.Company), nameof(company));
        }

        if (string.IsNullOrWhiteSpace(position))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.Position), nameof(position));
        }

        if (endDate.HasValue && endDate.Value < startDate)
        {
            throw new ArgumentException(ErrorMessages.EndDateBeforeStartDate, nameof(endDate));
        }

        Id = id;
        Company = company;
        Position = position;
        Description = description ?? string.Empty;
        StartDate = startDate;
        EndDate = endDate;
        IsCurrent = !endDate.HasValue;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateCompany(string newCompany)
    {
        if (string.IsNullOrWhiteSpace(newCompany))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.Company), nameof(newCompany));
        }

        Company = newCompany;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePosition(string newPosition)
    {
        if (string.IsNullOrWhiteSpace(newPosition))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.Position), nameof(newPosition));
        }

        Position = newPosition;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription ?? string.Empty;
        UpdatedAt = DateTime.UtcNow;
    }

    public void EndExperience(DateTime endDate)
    {
        if (endDate < StartDate)
        {
            throw new ArgumentException(ErrorMessages.EndDateBeforeStartDate, nameof(endDate));
        }

        EndDate = endDate;
        IsCurrent = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCurrent()
    {
        EndDate = null;
        IsCurrent = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

