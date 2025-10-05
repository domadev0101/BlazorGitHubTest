using Bunit;
using Xunit;
using BlazorGitHubTest.Pages;

namespace BlazorGitHubTest.Tests
{
    public class CounterTests : TestContext
    {
        [Fact]
        public void Counter_Increments_When_Button_Clicked()
        {
            // Arrange
            var cut = RenderComponent<Counter>();

            // Act
            cut.Find("button").Click();

            // Assert
            cut.Find("p[role='status']").MarkupMatches("<p role=\"status\">Current count: 1</p>");
        }
    }
}


