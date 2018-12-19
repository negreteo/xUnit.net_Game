using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
   public class PlayerCharacterShould : IDisposable
   {
      #region Properties and Constructor

      private readonly PlayerCharacter _sut;
      private readonly ITestOutputHelper _output;

      public PlayerCharacterShould (ITestOutputHelper output)
      {
         _output = output;
         _output.WriteLine ("Creating new PlayerCharacter");

         _sut = new PlayerCharacter ();
      }

      public void Dispose ()
      {
         _output.WriteLine ($"Disposing PlayerCharacter {_sut.FullName}");
         //_sut.Dispose ();
      }

      #endregion

      #region Boolean Assert

      [Fact]
      public void BeInexperienceWhenNew ()
      {
         Assert.True (_sut.IsNoob);
         // Assert.False (_sut.IsNoob);
      }

      #endregion

      #region Assert against String values

      [Fact]
      public void CalculateFullName ()
      {
         _sut.FirstName = "Matt";
         _sut.LastName = "Murdock";

         Assert.Equal ("Matt Murdock", _sut.FullName);
      }

      [Fact]
      public void HaveFullNameStartingWithFirstName ()
      {
         _sut.FirstName = "Matt";
         _sut.LastName = "Murdock";

         Assert.StartsWith ("Matt", _sut.FullName);
      }

      [Fact]
      public void HaveFullNameEndingWithLastName ()
      {
         _sut.FirstName = "Matt";
         _sut.LastName = "Murdock";

         Assert.EndsWith ("Murdock", _sut.FullName);
      }

      // Ignoring CASE
      [Fact]
      public void CalculateFullName_IgnoreCaseAssertExample ()
      {
         _sut.FirstName = "MATT";
         _sut.LastName = "MURDOCK";

         Assert.Equal ("Matt Murdock", _sut.FullName, ignoreCase : true);
      }

      [Fact]
      public void CalculateFullName_SubtringAssertExample ()
      {
         _sut.FirstName = "Matt";
         _sut.LastName = "Murdock";

         Assert.Contains ("tt Mu", _sut.FullName);
      }

      // Using an Regular Expression
      [Fact]
      public void CalculateFullNameWithTitleCase ()
      {
         _sut.FirstName = "Matt";
         _sut.LastName = "Murdock";

         Assert.Matches ("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
      }

      #endregion

      #region Asserting on Numeric values

      [Fact]
      public void StartWithDefaultHealth ()
      {
         Assert.Equal (100, _sut.Health);
      }

      [Fact]
      public void StartWithDefaultHealth_NotEqualExample ()
      {
         Assert.NotEqual (0, _sut.Health);
      }

      // Assert a Range Quantity after an Event occurs
      [Fact]
      public void IncreaseHealthAfterSleeping ()
      {
         _sut.Sleep (); // Expect increase between 1 to 100 inclusive

         //Assert.True (_sut.Health >= 101 && _sut.Health <= 200);
         Assert.InRange<int> (_sut.Health, 101, 200);
      }

      #endregion

      #region Asserting on Null values

      [Fact]
      public void NotHaveNickNameByDefault ()
      {
         Assert.Null (_sut.Nickname);
         //Assert.NotNull (_sut.Nickname);
      }

      #endregion

      #region Asserting with Collections

      [Fact]
      public void HaveALongBow ()
      {
         Assert.Contains ("Long Bow", _sut.Weapons);
      }

      [Fact]
      public void NotHaveAStaffOfWonder ()
      {
         Assert.DoesNotContain ("Staff of Wonder", _sut.Weapons);
      }

      // Using a Predicate that verifies the Collection matches an Element
      // Lambda Expression is used in the Predicate
      [Fact]
      public void HaveAtLeastOneKindOfSword ()
      {
         Assert.Contains (_sut.Weapons, weapon => weapon.Contains ("Sword"));
      }

      // Asserting a Full Collection matches
      [Fact]
      public void HaveAllExpectedWeapons ()
      {
         var expectedWeapons = new []
         {
            "Long Bow",
            "Short Bow",
            "Short Sword"
         };

         Assert.Equal (expectedWeapons, _sut.Weapons);
      }

      // Asserting against each one of the Items in the Collection
      [Fact]
      public void HaveNoEmptyDefaultWeapons ()
      {
         // Assert All - loops through the collection items
         Assert.All (_sut.Weapons, weapon => Assert.False (string.IsNullOrWhiteSpace (weapon)));
      }

      #endregion

      #region Asserting that Events are Raised

      [Fact]
      public void RaiseSleptEvent ()
      {
         // Event PlayerSlept is raised in method Sleep
         Assert.Raises<EventArgs> (
            handler => _sut.PlayerSlept += handler, // action that attaches to the event
            handler => _sut.PlayerSlept -= handler, // action that dettaches to the event
            () => _sut.Sleep ());
      }

      // Check a property (Health) changed in an event (TakeDamage)
      [Fact]
      public void RaisePropertyChangedEvent ()
      {
         Assert.PropertyChanged (_sut, "Health", () => _sut.TakeDamage (10));
      }

      #endregion

      #region Data-driven Tests

      [Theory] // Needs test data
      [InlineData (0, 100)] // Values come from inline data
      [InlineData (1, 99)]
      [InlineData (50, 50)]
      [InlineData (101, 1)]
      public void TakeDamage (int damage, int expectedHealth)
      {
         _sut.TakeDamage (damage);
         Assert.Equal (expectedHealth, _sut.Health);
      }

      #endregion

   }
}
