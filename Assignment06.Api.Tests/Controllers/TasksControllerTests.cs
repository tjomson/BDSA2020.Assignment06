using System;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Assignment06.Api.Controllers;
using Assignment06.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment06.Api.Tests
{
    public class TasksControllerTests
    {
        [Fact]
        public async Task Get_given_existing_returns_task()
        {
            var task = new TaskDetailsDTO();

            var repository = new Mock<ITaskRepository>();
            repository.Setup(s => s.Read(1)).ReturnsAsync(task);

            var controller = new TasksController(repository.Object);

            var actual = await controller.Get(1);

            Assert.Equal(task, actual.Value);
        }

        [Fact]
        public async Task Get_given_non_existing_returns_404()
        {
            var repository = new Mock<ITaskRepository>();

            TaskDetailsDTO task = null;

            var controller = new TasksController(repository.Object);
            repository.Setup(s => s.Read(42)).ReturnsAsync(task);

            var actual = await controller.Get(42);

            Assert.IsType<NotFoundResult>(actual.Result);
        }
        
        [Fact]
        public async Task Put_given_existing_returns_task()
        {
            var task = new TaskUpdateDTO()
            {
                Id = 1
            };

            var expected = new StatusCodeResult((int) Status.Updated);

            var repository = new Mock<ITaskRepository>();
            repository.Setup(s => s.Update(task)).ReturnsAsync(Status.Updated);

            var controller = new TasksController(repository.Object);

            var actual = await controller.Put(task.Id, task);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Put_given_non_existing_returns_404()
        {
            var task = new TaskUpdateDTO()
            {
                Id = 1
            };

            var expected = new StatusCodeResult((int) Status.NotFound);

            var repository = new Mock<ITaskRepository>();
            repository.Setup(s => s.Update(task)).ReturnsAsync(Status.NotFound);

            var controller = new TasksController(repository.Object);

            var actual = await controller.Put(task.Id, task);

            Assert.Equal(expected, actual);
        }
        

        [Fact]
        public async Task Put_given_conflicting_returns_conflict()
        {
            var repository = new Mock<ITaskRepository>();

            TaskDetailsDTO task = null;

            var controller = new TasksController(repository.Object);
            repository.Setup(s => s.Read(42)).ReturnsAsync(task);

            var actual = await controller.Get(42);

            Assert.IsType<NotFoundResult>(actual.Result);
        }
    }
}
