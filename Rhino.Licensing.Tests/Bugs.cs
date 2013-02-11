using System;
using System.IO;
using Xunit;

namespace Rhino.Licensing.Tests
{
    public class Bug : BaseLicenseTest
    {
        [Fact]
        public void Bug_for_year_over_8000_was_marked_as_expired()
        {
            var guid = Guid.NewGuid();
            var generator = new LicenseGenerator(public_and_private);
            var expiration = new DateTime(9999, 10, 10, 10, 10, 10);
            var key = generator.Generate("User name", guid, expiration, LicenseType.Standard);

            var path = Path.GetTempFileName();
            File.WriteAllText(path, key);

            var validator = new LicenseValidator(public_only, path);
            validator.AssertValidLicense();

            Assert.Equal(guid, validator.UserId);
            Assert.Equal(expiration, validator.ExpirationDate);
            Assert.Equal("User name", validator.Name);
            Assert.Equal(LicenseType.Standard, validator.LicenseType);


        }
    }
}