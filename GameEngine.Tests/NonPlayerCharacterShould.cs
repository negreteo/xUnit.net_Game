using Xunit;

namespace GameEngine.Tests
{
   public class NonPlayerCharacterShould
   {
      // Takes data from an internal class object 
      [Theory]
      [MemberData (nameof (InternalHealthDamageTestData.TestData),
         MemberType = typeof (InternalHealthDamageTestData))]
      public void TakeDamage_InternalTestData (int damage, int expectedHealth)
      {
         NonPlayerCharacter sut = new NonPlayerCharacter ();

         sut.TakeDamage (damage);

         Assert.Equal (expectedHealth, sut.Health);
      }

      // Takes data from an external file
      [Theory]
      [MemberData (nameof (EnternalHealthDamageTestData.TestData),
         MemberType = typeof (EnternalHealthDamageTestData))]
      public void TakeDamage_ExternalTestData (int damage, int expectedHealth)
      {
         NonPlayerCharacter sut = new NonPlayerCharacter ();

         sut.TakeDamage (damage);

         Assert.Equal (expectedHealth, sut.Health);
      }

      // Takes data from custom data attribute
      [Theory]
      [HealthDamageData]
      public void TakeDamage_CustomDataAttribute (int damage, int expectedHealth)
      {
         NonPlayerCharacter sut = new NonPlayerCharacter ();

         sut.TakeDamage (damage);

         Assert.Equal (expectedHealth, sut.Health);
      }

   }
}
