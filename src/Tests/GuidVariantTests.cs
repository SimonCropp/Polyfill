// Runs on every target, so net9.0 and later validate the BCL and everything below the polyfill.
public class GuidVariantTests
{
    // Both fields are exhaustively determined by two bytes, so every combination is checked
    // rather than sampled.
    [Test]
    public async Task ExhaustiveOverBothBytes()
    {
        var failures = 0;
        var visited = 0;
        for (var seven = 0; seven < 256; seven++)
        for (var eight = 0; eight < 256; eight++)
        {
            var bytes = new byte[16];
            bytes[7] = (byte) seven;
            bytes[8] = (byte) eight;
            var guid = new Guid(bytes);

            visited++;
            if (guid.Version != seven >> 4 ||
                guid.Variant != eight >> 4)
            {
                failures++;
            }
        }

        await Assert.That(failures).IsEqualTo(0);
        await Assert.That(visited).IsEqualTo(65536);
    }

    // the other fourteen bytes must not affect either value
    [Test]
    public async Task OtherBytesAreIgnored()
    {
        var random = new Random(42);
        var failures = 0;
        for (var i = 0; i < 20000; i++)
        {
            var bytes = new byte[16];
            random.NextBytes(bytes);
            bytes[7] = 0x74;
            bytes[8] = 0xb9;
            var guid = new Guid(bytes);

            if (guid.Version != 7 ||
                guid.Variant != 11)
            {
                failures++;
            }
        }

        await Assert.That(failures).IsEqualTo(0);
    }

    [Test]
    public async Task KnownUuids()
    {
        // the DNS namespace uuid from RFC 4122, a version 1 uuid
        await Assert.That(Guid.Parse("6ba7b810-9dad-11d1-80b4-00c04fd430c8").Version).IsEqualTo(1);
        await Assert.That(Guid.Parse("6ba7b810-9dad-11d1-80b4-00c04fd430c8").Variant).IsEqualTo(8);

        // a version 3, name based
        await Assert.That(Guid.Parse("3d813cbb-47fb-32ba-91df-831e1593ac29").Version).IsEqualTo(3);
        await Assert.That(Guid.Parse("3d813cbb-47fb-32ba-91df-831e1593ac29").Variant).IsEqualTo(9);

        // a version 7, time ordered
        await Assert.That(Guid.Parse("01890a5d-ac96-774b-bcce-b302099a8057").Version).IsEqualTo(7);
        await Assert.That(Guid.Parse("01890a5d-ac96-774b-bcce-b302099a8057").Variant).IsEqualTo(11);

        await Assert.That(Guid.Empty.Version).IsEqualTo(0);
        await Assert.That(Guid.Empty.Variant).IsEqualTo(0);

        var allBitsSet = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        await Assert.That(allBitsSet.Version).IsEqualTo(15);
        await Assert.That(allBitsSet.Variant).IsEqualTo(15);
    }

    // Variant is the raw high nibble of byte 8, not the RFC 4122 variant field, which is only the
    // top two bits. So an RFC 4122 uuid reports 8, 9, 10 or 11 rather than one fixed number.
    [Test]
    public async Task GeneratedGuids()
    {
        for (var i = 0; i < 2000; i++)
        {
            var random = Guid.NewGuid();
            await Assert.That(random.Version).IsEqualTo(4);
            await Assert.That(random.Variant & 0b1100).IsEqualTo(0b1000);

            var seven = Guid.CreateVersion7();
            await Assert.That(seven.Version).IsEqualTo(7);
            await Assert.That(seven.Variant & 0b1100).IsEqualTo(0b1000);
        }
    }

    // the value comes from the same byte the string form shows, which is the check that would
    // catch reading the wrong index or the wrong endianness of the third group
    [Test]
    public async Task AgreesWithTheStringForm()
    {
        var random = new Random(7);
        var failures = 0;
        for (var i = 0; i < 20000; i++)
        {
            var bytes = new byte[16];
            random.NextBytes(bytes);
            var guid = new Guid(bytes);
            var text = guid.ToString("D");

            // "xxxxxxxx-xxxx-Vxxx-Nxxx-xxxxxxxxxxxx"
            var version = Convert.ToInt32(text.Substring(14, 1), 16);
            var variant = Convert.ToInt32(text.Substring(19, 1), 16);

            if (guid.Version != version ||
                guid.Variant != variant)
            {
                failures++;
            }
        }

        await Assert.That(failures).IsEqualTo(0);
    }
}
