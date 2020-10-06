# Assignment #6

## C&#35; - Kanban Board part quatre

[![Simple-kanban-board-](https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Simple-kanban-board-.jpg/512px-Simple-kanban-board-.jpg)](https://commons.wikimedia.org/wiki/File:Simple-kanban-board-.jpg "Jeff.lasovski [CC BY-SA 3.0 (https://creativecommons.org/licenses/by-sa/3.0)], via Wikimedia Commons")

Fork this repository and implement the code required for the assignments below.

### Prequel

Inspect the code for `TagRepository`, `TagRepositoryTests`, `TaskRepository`, and `TaskRepositoryTests`. They contain a sample solution for last weeks assignment.

### Exercise 1

Implement and test the `TasksController` class such that it exposes the `ITasksRepository` interface using *REST*.
Tests should use `Moq` and not a live database.

### Exercise 2

Implement and test the `TagsController` class such that it exposes the `ITagsRepository` interface using *REST*.
Tests should use `Moq` and not a live database.

### Exercise 3

Implement and test the `UsersController` class such that it exposes the `IUsersRepository` interface using *REST*.
Tests should use `Moq` and not a live database.

**Note**: You do not need an implementation of `IUsersRepository` for this.

### Exercise 4

Configure the `Startup` class with dependencies such that the system can run and targets either a *SQLite* or a *SQL Server* database.
