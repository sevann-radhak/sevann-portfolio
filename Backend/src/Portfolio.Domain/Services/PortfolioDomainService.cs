using Portfolio.Domain.Aggregates;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Services;

public class PortfolioDomainService
{
    public static bool CanFeatureProject(PortfolioProjectAggregate project)
    {
        ArgumentNullException.ThrowIfNull(project);

        return project.Project.Status == ProjectStatus.Active;
    }

    public static bool CanArchiveProject(PortfolioProjectAggregate project)
    {
        ArgumentNullException.ThrowIfNull(project);

        return project.Project.Status != ProjectStatus.Archived;
    }

    public static int CalculateTotalYearsOfExperience(IEnumerable<Experience> experiences)
    {
        ArgumentNullException.ThrowIfNull(experiences);

        List<Experience> experienceList = experiences.ToList();

        if (experienceList.Count == 0)
        {
            return 0;
        }

        int totalMonths = 0;

        foreach (Experience? experience in experienceList)
        {
            DateTime endDate = experience.EndDate ?? DateTime.UtcNow;
            int months = ((endDate.Year - experience.StartDate.Year) * 12) + (endDate.Month - experience.StartDate.Month);
            totalMonths += months;
        }

        return totalMonths / 12;
    }

    public static int CountActiveSkills(IEnumerable<Skill> skills)
    {
        return skills is null ? throw new ArgumentNullException(nameof(skills)) : skills.Count(s => s.IsActive);
    }

    public static int CountFeaturedProjects(IEnumerable<PortfolioProject> projects)
    {
        return projects is null
            ? throw new ArgumentNullException(nameof(projects))
            : projects.Count(p => p.IsFeatured && p.Status == ProjectStatus.Active);
    }
}

