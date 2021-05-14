using MangaBooksProject.Models;
using System;
using Xunit;

namespace MangaBooksProject.tests
{
    public class MangaTest
    {
        [Fact]
        public void AuthorInput()
        {
            //Arrange
            var manga1 = new MangaModel();
            var authorinput = manga1.Author = "Kubo Tite";

            //Act
            var result = "Kubo Tite";


            //Assert
            Assert.Equal(authorinput, result);
        }
    }
}
