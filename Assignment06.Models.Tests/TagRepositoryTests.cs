using System;
using System.Linq;
using Assignment06.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Assignment06.Entities.State;
using static Assignment06.Models.Response;

namespace Assignment06.Models.Tests
{
    public class TagRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly KanbanContext _context;
        private readonly TagRepository _repository;

        public TagRepositoryTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>().UseSqlite(_connection);
            _context = new KanbanTestContext(builder.Options);
            _context.Database.EnsureCreated();
            _repository = new TagRepository(_context);
        }

        [Fact]
        public void Create_returns_Created_with_id()
        {
            var tag = new TagCreateDTO
            {
                Name = "tag"
            };

            var (response, id) = _repository.Create(tag);

            var created = _context.Tags.Find(id);

            Assert.Equal(Created, response);
            Assert.Equal(id, created.Id);
        }

        [Fact]
        public void Create_creates_a_tag_with_name_and_description_and_state_New()
        {
            var tag = new TagCreateDTO
            {
                Name = "tag"
            };

            var (_, id) = _repository.Create(tag);

            var created = _context.Tags.Find(id);

            Assert.Equal("tag", created.Name);
        }

        [Fact]
        public void Create_given_existing_tag_returns_Conflict()
        {
            var tag = new TagCreateDTO
            {
                Name = "tag2"
            };

            var (response, _) = _repository.Create(tag);

            Assert.Equal(Conflict, response);
        }

        [Fact]
        public void Read_given_id_returns_tag_with_mapped_properties()
        {
            var tag = _repository.Read(2);

            Assert.Equal("tag2", tag.Name);
            Assert.Equal(0, tag.New);
            Assert.Equal(1, tag.Active);
            Assert.Equal(0, tag.Resolved);
            Assert.Equal(1, tag.Closed);
            Assert.Equal(0, tag.Removed);
        }

        [Fact]
        public void Read_given_returns_all_tags_with_state_count()
        {
            var tags = _repository.Read();

            Assert.Equal(3, tags.Count());

            var tag = tags.FirstOrDefault(t => t.Id == 2);

            Assert.Equal("tag2", tag.Name);
            Assert.Equal(0, tag.New);
            Assert.Equal(1, tag.Active);
            Assert.Equal(0, tag.Resolved);
            Assert.Equal(1, tag.Closed);
            Assert.Equal(0, tag.Removed);
        }

        [Fact]
        public void Update_given_non_existing_tag_returns_NotFound()
        {
            var tag = new TagUpdateDTO
            {
                Id = 42,
                Name = "tag",
            };

            var response = _repository.Update(tag);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public void Update_updates_name_returns_updated()
        {
            var tag = new TagUpdateDTO
            {
                Id = 2,
                Name = "newtag"
            };

            var response = _repository.Update(tag);

            var updated = _context.Tags.Find(2);

            Assert.Equal(Updated, response);
            Assert.Equal("newtag", updated.Name);
        }

        [Fact]
        public void Update_given_existing_name_in_other_tag_returns_Conflict()
        {
            var tag = new TagUpdateDTO
            {
                Id = 2,
                Name = "tag1"
            };

            var response = _repository.Update(tag);

            Assert.Equal(Conflict, response);
        }

        [Fact]
        public void Update_given_no_change_in_name_returns_Updated()
        {
            var tag = new TagUpdateDTO
            {
                Id = 2,
                Name = "tag2"
            };

            var response = _repository.Update(tag);

            var updated = _context.Tags.Find(2);

            Assert.Equal(Updated, response);
            Assert.Equal("tag2", updated.Name);
        }

        [Fact]
        public void Delete_given_non_existing_tag_returns_NotFound()
        {
            var response = _repository.Delete(42);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public void Delete_given_existing_tag_returns_Deleted()
        {
            var response = _repository.Delete(3);

            Assert.Equal(Deleted, response);
        }

        [Fact]
        public void Delete_given_existing_tag_deletes_tag()
        {
            _repository.Delete(3);

            var entity = _context.Tags.Find(3);

            Assert.Null(entity);
        }

        [Fact]
        public void Delete_given_existing_tag_with_tasks_returns_Conflict()
        {
            var response = _repository.Delete(2);

            Assert.Equal(Conflict, response);
        }

        [Fact]
        public void Delete_given_existing_tag_with_tasks_using_force_returns_Deleted()
        {
            var response = _repository.Delete(2, true);

            Assert.Equal(Deleted, response);
        }

        [Fact]
        public void Delete_given_existing_tag_with_tasks_using_force_deletes_tag()
        {
            _repository.Delete(2, true);

            var entity = _context.Tags.Find(2);

            Assert.Null(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }
    }
}
