# Development Workflow

This document describes the Scrum-based development workflow for this portfolio project.

## Workflow Overview

```
Issue (US/Bug) → Branch → Development → PR → Review → Merge → Deploy
```

## Step-by-Step Process

### 1. Create User Story or Issue

**For Features:**
- Use template: `.github/ISSUE_TEMPLATE/user_story.md`
- Format: `US-XX: [Title]`
- Label: `user-story`
- Assign story points and priority

**For Bugs:**
- Use template: `.github/ISSUE_TEMPLATE/bug_report.md` (create if needed)
- Format: `BUG-XX: [Title]`
- Label: `bug`

**For Tasks:**
- Use template: `.github/ISSUE_TEMPLATE/feature_request.md`
- Format: `TASK-XX: [Title]`
- Label: `task`

### 2. Create Branch

**Naming Convention:**
- Features: `features/US-XX-short-description`
- Bugs: `bugfix/US-XX-short-description`
- Tasks: `task/US-XX-short-description`

**Examples:**
- `features/US-01-create-ddd-module`
- `bugfix/US-05-fix-authentication-error`
- `task/US-10-update-documentation`

**Commands:**
```bash
# From main branch
git checkout main
git pull origin main

# Create and switch to new branch
git checkout -b features/US-01-create-ddd-module

# Push branch to remote
git push -u origin features/US-01-create-ddd-module
```

### 3. Development

**Coding Standards:**
- All code in English
- No inline comments (documentation in READMEs/tickets)
- Follow SOLID principles
- Write tests first (TDD when applicable)
- Use conventional commits

**Conventional Commits:**
```
feat: add DDD module with entities and value objects
fix: resolve authentication token expiration issue
docs: update architecture documentation
test: add unit tests for repository pattern
refactor: improve error handling in API controllers
```

### 4. Commit and Push

```bash
# Stage changes
git add .

# Commit with conventional commit message
git commit -m "feat: implement DDD module with entities and aggregates"

# Push to remote
git push origin features/US-01-create-ddd-module
```

### 5. Create Pull Request

**PR Title:**
- Format: `[US-XX] Title`
- Example: `[US-01] Create DDD Module`

**PR Description:**
- Use template: `.github/pull_request_template.md`
- Link to issue: `Closes #XX`
- Describe changes
- List acceptance criteria checked

**PR Checklist:**
- [ ] Code follows project standards
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] No compilation warnings
- [ ] All acceptance criteria met

### 6. Code Review

- Reviewer checks code quality
- Reviewer verifies tests pass
- Reviewer checks documentation
- Address review comments
- Update PR if needed

### 7. Merge

**Merge Strategy:**
- Squash and merge (recommended for clean history)
- Or merge commit (if preserving branch history)

**After Merge:**
- Close issue
- Delete branch (if not needed)
- Update project board (if using)

### 8. Update Documentation

- Update README if needed
- Update architecture docs if needed
- Update module documentation

## Branch Strategy

### Main Branches

- `main`: Production-ready code
- `develop`: Integration branch (optional)

### Feature Branches

- `features/US-XX-*`: New features
- `bugfix/US-XX-*`: Bug fixes
- `task/US-XX-*`: Tasks and chores

### Branch Lifecycle

1. Create from `main`
2. Develop and commit
3. Create PR to `main`
4. Review and merge
5. Delete branch

## Issue Management

### Labels

- `user-story`: User stories
- `bug`: Bug reports
- `task`: Tasks
- `enhancement`: Feature enhancements
- `documentation`: Documentation updates
- `priority-high`: High priority
- `priority-medium`: Medium priority
- `priority-low`: Low priority
- `complexity-low`: Low complexity
- `complexity-medium`: Medium complexity
- `complexity-high`: High complexity

### Milestones

- `MVP`: Minimum Viable Product
- `v1.0`: First release
- `v2.0`: Second release

## Definition of Done

A user story is considered "Done" when:

- [ ] All acceptance criteria met
- [ ] Code implemented and tested
- [ ] Unit tests written and passing 
- [ ] Integration tests written (if applicable)
- [ ] Code reviewed and approved
- [ ] Documentation updated
- [ ] No compilation warnings
- [ ] Follows coding standards
- [ ] PR merged to main
- [ ] Issue closed

## Sprint Planning

### Sprint Duration
- 1-2 weeks (adjust based on project needs)

### Sprint Activities
1. **Sprint Planning**: Select user stories for sprint
2. **Daily Standup**: Progress updates (if working in team)
3. **Sprint Review**: Demo completed work
4. **Sprint Retrospective**: Improve process

## Example Workflow

### US-01: Create DDD Module

1. **Create Issue**: Use user_story.md template
2. **Create Branch**: `features/US-01-create-ddd-module`
3. **Develop**: Implement DDD module
4. **Test**: Write unit tests
5. **Document**: Update module README
6. **Commit**: `feat: implement DDD module with entities and aggregates`
7. **PR**: Create PR linking to US-01
8. **Review**: Code review
9. **Merge**: Merge to main
10. **Close**: Close US-01 issue

---

**Last updated**: December 2025 
**Author**: Sevann Radhak Triztan

