using System;
using Xunit;

namespace GameEngine.Tests
{
   [Trait ("Category", "Enemy")]
   public class EnemyFactoryShould
   {
      #region Asserts against Object Types

      [Fact]
      public void CreateNormalEnemyByDefault ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy = sut.Create ("Zombie");

         Assert.IsType<NormalEnemy> (enemy);
      }

      [Fact (Skip = "Don't need to run this")]
      public void CreateNormalEnemyByDefault_NotTypeExample ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy = sut.Create ("Zombie");

         Assert.IsNotType<DateTime> (enemy);
      }

      [Fact]
      public void CreateBossEnemy ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy = sut.Create ("Zombie King", true); // true specifies it is a boss enemy

         // Specify in <T> the expected Type 
         Assert.IsType<BossEnemy> (enemy);
      }

      [Fact]
      public void CreateBossEnemy_CastReturnedTypeExample ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy = sut.Create ("Zombie King", true); // true specifies it is a boss enemy

         // Assert and get cast result if test passes
         BossEnemy boss = Assert.IsType<BossEnemy> (enemy);

         // Additional asserts on typed object
         // Checking the name is equal to "Zombie King"
         Assert.Equal ("Zombie King", boss.Name);
      }

      // Checks Type Equality from a Derived or Inherited Class
      [Fact]
      public void CreateBossEnemy_AssertAssignableTypes ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy = sut.Create ("Zombie King", true); // true specifies it is a boss enemy

         //Assert.IsType<BossEnemy> (enemy);
         Assert.IsAssignableFrom<Enemy> (enemy);
      }

      #endregion

      #region Asserting on Object Instances

      [Fact]
      public void CreateSeparateInstances ()
      {
         EnemyFactory sut = new EnemyFactory ();

         Enemy enemy1 = sut.Create ("Zombie");
         Enemy enemy2 = sut.Create ("Zombie");

         Assert.NotSame (enemy1, enemy2);
         //Assert.Same (enemy1, enemy2);
      }

      #endregion

      #region Asserting that code throws an exception

      [Fact]
      public void NotAllowedNullName ()
      {
         EnemyFactory sut = new EnemyFactory ();

         //Assert.Throws<ArgumentNullException> (() => sut.Create (null));

         // Validating against an argument (name) that is expected to cause the Exception
         Assert.Throws<ArgumentNullException> ("name", () => sut.Create (null));
      }

      [Fact]
      public void OnlyAllowKingOrQueenBossEnemies ()
      {
         EnemyFactory sut = new EnemyFactory ();

         // Not a Queen or a Boss, Zombie throws an Exception and the test passes
         EnemyCreationException ex = Assert.Throws<EnemyCreationException> (
            () => sut.Create ("Zombie", true));

         Assert.Equal ("Zombie", ex.RequestedEnemyName);
      }

      #endregion

   }
}
