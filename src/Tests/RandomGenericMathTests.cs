// Generic math (IBinaryInteger<T>, IBinaryFloatingPointIeee754<T>) only exists from net7, so these can't compile lower.
#if NET7_0_OR_GREATER

using System.Numerics;
using System.Linq;

// net11.0 runs the BCL, net7.0 to net10.0 the polyfill.
public class RandomGenericMathTests
{
    // Captured from the BCL on net11.0. The polyfill draws from the same Random members in the same order, so a
    // seeded Random has to produce exactly these values on every target.
    static string[] expectedSeeded =
    [
        "byte: 229",
        "byte: 108",
        "byte: 83",
        "sbyte: 12",
        "sbyte: 64",
        "sbyte: 61",
        "short: 18747",
        "short: 32452",
        "short: 30836",
        "ushort: 4507",
        "ushort: 33305",
        "ushort: 6700",
        "char: 23502",
        "char: 35667",
        "char: 25958",
        "int: 1594479323",
        "int: 1746989696",
        "int: 260588414",
        "uint: 1344523159",
        "uint: 2322783657",
        "uint: 148869001",
        "long: 6439962817402585491",
        "long: 5857314722532158982",
        "long: 7055885559419013981",
        "ulong: 5538261069194165750",
        "ulong: 2727279155707838963",
        "ulong: 5592403775672823524",
        "Int128: 56164621899614876416358013733755003028",
        "Int128: 86105197551338731869121937641790235922",
        "Int128: 164150719379323858137631861133440803808",
        "UInt128: 25292430011924215581803371997445906158",
        "UInt128: 143299931967942124728282084449276996569",
        "UInt128: 121421670909022461604525953271284332374",
        "int max: 80",
        "int max: 84",
        "int max: 23",
        "byte max 0: 0",
        "byte max 0: 0",
        "byte max 0: 0",
        "long max: 778096922091",
        "long max: 302177244721",
        "long max: 607756561064",
        "ulong max small: 972",
        "ulong max small: 812",
        "ulong max small: 26",
        "ulong max large: 4008725999450372517",
        "ulong max large: 2077751625385236023",
        "ulong max large: 13348629211850556547",
        "Int128 max: 6777329049328151643466524656335423981",
        "Int128 max: 98669818263507801792231528547522727872",
        "Int128 max: 83730840286217041029369179313738441338",
        "UInt128 max: 107938372455140285210313637687647124267",
        "UInt128 max: 20513106968727718826171549619304117876",
        "UInt128 max: 10423115217228611886679535463837587791",
        "BigInteger max: 421971615597286324720539831746",
        "BigInteger max: 501459283688883520321101045005",
        "BigInteger max: 343243262345880477934150684027",
        "BigInteger max 0: 0",
        "BigInteger max 0: 0",
        "BigInteger max 0: 0",
        "int range: 20",
        "int range: 28",
        "int range: -12",
        "int full range: 1577702958",
        "int full range: 833413667",
        "int full range: 1561321623",
        "sbyte full range: -40",
        "sbyte full range: 121",
        "sbyte full range: 42",
        "long full range: -1632131463859435979",
        "long full range: -5088893920429613252",
        "long full range: -9009939720609617658",
        "ulong range: 8773134041054601630",
        "ulong range: 9815492277766652307",
        "ulong range: 10717403823377083866",
        "Int128 full range: -65154430494137820101129616601745119689",
        "Int128 full range: -89704869536465725545635669486888113378",
        "Int128 full range: 42496444815205868583736703457602264741",
        "BigInteger range: 735930818143931154853",
        "BigInteger range: 1045744243566414221520",
        "BigInteger range: 406207401697947729108",
        "int empty range: 7",
        "int empty range: 7",
        "int empty range: 7",
        "float: 0.4240081",
        "float: 0.63560754",
        "float: 0.72613007",
        "double: 0.28571093235430817",
        "double: 0.23373615706047796",
        "double: 0.8012815363711125",
        "Half: 0.5215",
        "Half: 0.8525",
        "Half: 0.5786"
    ];

    [Test]
    public async Task Seeded_MatchesBcl()
    {
        var actual = SeededValues();
        await Assert.That(string.Join("\n", actual)).IsEqualTo(string.Join("\n", expectedSeeded));
    }

    static List<string> SeededValues()
    {
        var random = new Random(1234);
        var values = new List<string>();

        void Add<T>(string name, Func<T> next)
        {
            for (var i = 0; i < 3; i++)
            {
                values.Add($"{name}: {Convert.ToString(next(), CultureInfo.InvariantCulture)}");
            }
        }

        Add("byte", () => random.NextInteger<byte>());
        Add("sbyte", () => random.NextInteger<sbyte>());
        Add("short", () => random.NextInteger<short>());
        Add("ushort", () => random.NextInteger<ushort>());
        Add("char", () => (int) random.NextInteger<char>());
        Add("int", () => random.NextInteger<int>());
        Add("uint", () => random.NextInteger<uint>());
        Add("long", () => random.NextInteger<long>());
        Add("ulong", () => random.NextInteger<ulong>());
        Add("Int128", () => random.NextInteger<Int128>());
        Add("UInt128", () => random.NextInteger<UInt128>());

        Add("int max", () => random.NextInteger(100));
        Add("byte max 0", () => random.NextInteger((byte) 0));
        Add("long max", () => random.NextInteger(1_000_000_000_000L));
        Add("ulong max small", () => random.NextInteger(1000UL));
        Add("ulong max large", () => random.NextInteger(ulong.MaxValue - 5));
        Add("Int128 max", () => random.NextInteger(Int128.MaxValue));
        Add("UInt128 max", () => random.NextInteger(UInt128.MaxValue / 3));
        Add("BigInteger max", () => random.NextInteger(BigInteger.Pow(2, 100) + 7));
        Add("BigInteger max 0", () => random.NextInteger(BigInteger.Zero));

        Add("int range", () => random.NextInteger(-50, 50));
        Add("int full range", () => random.NextInteger(int.MinValue, int.MaxValue));
        Add("sbyte full range", () => random.NextInteger(sbyte.MinValue, sbyte.MaxValue));
        Add("long full range", () => random.NextInteger(long.MinValue, long.MaxValue));
        Add("ulong range", () => random.NextInteger(5UL, ulong.MaxValue));
        Add("Int128 full range", () => random.NextInteger(Int128.MinValue, Int128.MaxValue));
        Add("BigInteger range", () => random.NextInteger(-BigInteger.Pow(2, 70), BigInteger.Pow(2, 70)));
        Add("int empty range", () => random.NextInteger(7, 7));

        Add("float", () => random.NextBinaryFloat<float>());
        Add("double", () => random.NextBinaryFloat<double>());
        Add("Half", () => random.NextBinaryFloat<Half>());

        return values;
    }

    [Test]
    public async Task NextInteger_CoversWholeRange()
    {
        var random = new Random(42);
        var bytes = new HashSet<byte>();
        var sbytes = new HashSet<sbyte>();
        for (var i = 0; i < 20_000; i++)
        {
            bytes.Add(random.NextInteger<byte>());
            sbytes.Add(random.NextInteger<sbyte>());
        }

        // unlike Next(), NextInteger<T>() is inclusive of T.MaxValue
        await Assert.That(bytes.Count).IsEqualTo(256);
        await Assert.That(sbytes.Count).IsEqualTo(128);
        await Assert.That(sbytes.Min()).IsEqualTo((sbyte) 0);
        await Assert.That(sbytes.Max()).IsEqualTo(sbyte.MaxValue);
    }

    [Test]
    public async Task NextInteger_StaysInRange()
    {
        var random = new Random(42);
        var failures = new List<string>();

        void Check<T>(string name, T minValue, T maxValue, Func<T> next)
            where T : IBinaryInteger<T>
        {
            for (var i = 0; i < 2_000; i++)
            {
                var value = next();
                if (value < minValue || value >= maxValue)
                {
                    failures.Add($"{name}: {value} outside [{minValue}, {maxValue})");
                    return;
                }
            }
        }

        Check("int max", 0, 10, () => random.NextInteger(10));
        Check("ulong max", 0UL, ulong.MaxValue, () => random.NextInteger(ulong.MaxValue));
        Check("UInt128 max", UInt128.Zero, UInt128.MaxValue, () => random.NextInteger(UInt128.MaxValue));
        Check("BigInteger max", BigInteger.Zero, BigInteger.Pow(10, 40), () => random.NextInteger(BigInteger.Pow(10, 40)));
        Check("short range", (short) -3, (short) 3, () => random.NextInteger((short) -3, (short) 3));
        Check("long full range", long.MinValue, long.MaxValue, () => random.NextInteger(long.MinValue, long.MaxValue));
        Check("Int128 full range", Int128.MinValue, Int128.MaxValue, () => random.NextInteger(Int128.MinValue, Int128.MaxValue));
        Check("BigInteger range", -BigInteger.Pow(10, 30), BigInteger.Pow(10, 30), () => random.NextInteger(-BigInteger.Pow(10, 30), BigInteger.Pow(10, 30)));

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task NextInteger_EmptyRangesReturnTheBound()
    {
        var random = new Random(42);
        await Assert.That(random.NextInteger(0)).IsEqualTo(0);
        await Assert.That(random.NextInteger(UInt128.Zero)).IsEqualTo(UInt128.Zero);
        await Assert.That(random.NextInteger(BigInteger.Zero)).IsEqualTo(BigInteger.Zero);
        await Assert.That(random.NextInteger(-5, -5)).IsEqualTo(-5);
        await Assert.That(random.NextInteger(BigInteger.MinusOne, BigInteger.MinusOne)).IsEqualTo(BigInteger.MinusOne);
    }

    [Test]
    public async Task NextInteger_InvalidArguments()
    {
        var random = new Random(42);
        await Assert.That(Describe(() => random.NextInteger(-1))).IsEqualTo("ArgumentOutOfRangeException:maxValue");
        await Assert.That(Describe(() => random.NextInteger(BigInteger.MinusOne))).IsEqualTo("ArgumentOutOfRangeException:maxValue");
        await Assert.That(Describe(() => random.NextInteger(5, 4))).IsEqualTo("ArgumentOutOfRangeException:minValue");
        await Assert.That(Describe(() => random.NextInteger(ulong.MaxValue, 0UL))).IsEqualTo("ArgumentOutOfRangeException:minValue");
    }

    [Test]
    public async Task NextBinaryFloat_StaysInRange()
    {
        var random = new Random(42);
        var failures = new List<string>();

        void Check<T>(string name)
            where T : IBinaryFloatingPointIeee754<T>
        {
            for (var i = 0; i < 20_000; i++)
            {
                var value = random.NextBinaryFloat<T>();
                if (value < T.Zero || value >= T.One)
                {
                    failures.Add($"{name}: {value} outside [0, 1)");
                    return;
                }
            }
        }

        Check<float>("float");
        Check<double>("double");
        // converting NextSingle() to Half can round to 1.0, which is why Half is built from its significand
        Check<Half>("Half");
        Check<NFloat>("NFloat");

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    public async Task NextBinaryFloat_UsesDerivedRandom()
    {
        var random = new FixedRandom();
        await Assert.That(random.NextBinaryFloat<double>()).IsEqualTo(0.25);
        await Assert.That(random.NextBinaryFloat<float>()).IsEqualTo(0.5f);
        await Assert.That(random.NextInteger(1000)).IsEqualTo(999);
    }

    class FixedRandom : Random
    {
        public override double NextDouble() => 0.25;
        public override float NextSingle() => 0.5f;
        public override int Next(int maxValue) => maxValue - 1;
    }

    static string Describe(Action action)
    {
        try
        {
            action();
            return "no exception";
        }
        catch (ArgumentException exception)
        {
            return $"{exception.GetType().Name}:{exception.ParamName}";
        }
    }
}

#endif
