
using BookStore1.Controllers;
using BookStore1.Interfaces;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookStore1.Tests
{
    public class BookControllerTests
    {
        private readonly BookController _controller;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<IPhotoService> _mockPhotoService;

        public BookControllerTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockPhotoService = new Mock<IPhotoService>();
            _controller = new BookController(null, _mockBookRepository.Object, _mockPhotoService.Object);
        }

        [Fact]
        public async Task Index_ReturnsCorrectView()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { ID = 1, Title = "Book 1", Author = "Author 1", Price = 10 },
                new Book { ID = 2, Title = "Book 2", Author = "Author 2", Price = 20 }
            };

            _mockBookRepository.Setup(x => x.GetAll()).ReturnsAsync(books);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Detail_ReturnsCorrectView()
        {
            // Arrange
            var book = new Book { ID = 1, Title = "Book 1", Author = "Author 1", Price = 10 };
            _mockBookRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _controller.Detail(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ID);
        }

        
    }
}
