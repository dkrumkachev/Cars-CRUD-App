using lab2;
using lab2.vehicles;
using lab2.Serializers;
using System.Linq.Expressions;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        private TextSerializer CreateTextSerializer()
        {
            return new TextSerializer();
        }

        private string CreateTextFile(string contents = "")
        {
            const string Filename = "testfile.txt";
            File.WriteAllText(Filename, contents);
            return Filename;
        }

        [TestMethod]
        public void Serialize_ListOfVehicles_CreatesFileWithSerializedVehicles()
        {
            var vehicles = new List<Vehicle>() {
                new Motorcycle(Motorcycle.MotorcycleTypes.OffRoad, "qqq", "abc999", 
                    "Qwe", 19, 150, Engine.FuelTypes.Gasoline),
                new Bicycle(Bicycle.BikeTypes.Racing, 15, "iiii", "d4443", "Lpfe", 30)
            };
            const string Expected = @"[MotorcycleSerializableModel{""Type"":""OffRoad"";" +
                @"""Engine"":EngineSerializableModel{""FuelType"":""Gasoline"";""Horsepower"":""150"";};" +
                @"""Manufacturer"":""qqq"";""Model"":""abc999"";""Driver"":PersonSerializableModel" + 
                @"{""Name"":""Qwe"";""Age"":""19"";};}BicycleSerializableModel{""Type"":""Racing"";" + 
                @"""WheelsDiameter"":""15"";""Manufacturer"":""iiii"";""Model"":""d4443"";""Driver"":" + 
                @"PersonSerializableModel{""Name"":""Lpfe"";""Age"":""30"";};}]";
            string filename = CreateTextFile();
            ISerializer serializer = CreateTextSerializer();

            serializer.Serialize(filename, vehicles);
            string actual = File.ReadAllText(filename);
            
            Assert.AreEqual(actual, Expected);
        }

        [TestMethod]
        public void Deserialize_FileWithOneVehicle_ReturnsOneVehicle()
        {
            string filename = CreateTextFile(string.Empty);
            var expected = new List<Vehicle>() {
                new Motorcycle(Motorcycle.MotorcycleTypes.OffRoad,
                "qqq", "abc999", "Qwe", 19, 150, Engine.FuelTypes.Gasoline)
            };
            ISerializer serializer = CreateTextSerializer();
            serializer.Serialize(filename, expected);

            List<Vehicle> actual = serializer.Deserialize(filename);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_FileWithListOfVehicles_ReturnsCorrectListOfVehicles()
        {
            var expected = new List<Vehicle>() {
                new Truck(9, false, "aaaa", "1234", "Ann", 43, 200, Engine.FuelTypes.Diesel),
                new Motorcycle(Motorcycle.MotorcycleTypes.OffRoad, "qqq", "abc999", "Qwe", 19, 150,
                    Engine.FuelTypes.Gasoline),
                new Bicycle(Bicycle.BikeTypes.Racing, 15, "iiii", "d4443", "Lpfe", 30)
            };
            string filename = CreateTextFile();
            ISerializer serializer = CreateTextSerializer();
            serializer.Serialize(filename, expected);

            List<Vehicle> actual = serializer.Deserialize(filename);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_FileWithEmptyList_ReturnsEmptyListOfVehicles()
        {
            var expected = new List<Vehicle>();
            string filename = CreateTextFile();
            ISerializer serializer = CreateTextSerializer();
            serializer.Serialize(filename, expected);

            List<Vehicle> actual = serializer.Deserialize(filename);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_EscapedQuotesInStrings_ReturnsListOfVehiclesWithQuotesInStringFields()
        {
            var expected = new List<Vehicle>() {
                new Motorcycle(Motorcycle.MotorcycleTypes.OffRoad,
                "q\"q\"q", "ab\"\"c99\"9", "Q\"w\"e", 19, 150, Engine.FuelTypes.Gasoline)
            };
            string filename = CreateTextFile();
            ISerializer serializer = CreateTextSerializer();
            serializer.Serialize(filename, expected);

            List<Vehicle> actual = serializer.Deserialize(filename);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_ExtraCharactersAtTheBegging_ThrowsFileFormatException()
        {
            const string FileContents = @"adlkfja[BicycleSerializableModel{""Type"":""Racing"";" +
                @"""WheelsDiameter"":""15"";""Manufacturer"":""abc"";""Model"":""d4443"";""Driver"":" +
                @"PersonSerializableModel{""Name"":""wwww"";""Age"":""30"";};}]";
            string filename = CreateTextFile(FileContents);
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }
        
        [TestMethod]
        public void Deserialize_NotQuotedPropertyName_ThrowsFileFormatException()
        {
            const string FileContents = @"[BicycleSerializableModel{""Type"":""Racing"";" +
                @"""WheelsDiameter"":""15"";Manufacturer:""abc"";""Model"":""d4443"";""Driver"":" +
                @"PersonSerializableModel{""Name"":""wwww"";""Age"":""30"";};}]";
            string filename = CreateTextFile(FileContents);
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }

        [TestMethod]
        public void Deserialize_QuotedTypeName_ThrowsFileFormatException()
        {
            const string FileContents = @"[""BicycleSerializableModel""{""Type"":""Racing"";" +
                @"""WheelsDiameter"":""15"";""Manufacturer"":""abc"";""Model"":""d4443"";""Driver"":" +
                @"PersonSerializableModel{""Name"":""wwww"";""Age"":""30"";};}]";
            string filename = CreateTextFile(FileContents);
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }

        [TestMethod]
        public void Deserialize_CommasInsteadOfSemicolonsAfterPropertyValue_ThrowsFileFormatException()
        {
            const string FileContents = @"[BicycleSerializableModel{""Type"":""Racing""," +
                @"""WheelsDiameter"":""15"",""Manufacturer"":""abc"",""Model"":""d4443"",""Driver"":" +
                @"PersonSerializableModel{""Name"":""wwww"",""Age"":""30"",},}]";
            string filename = CreateTextFile(FileContents);
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }
        
        [TestMethod]
        public void Deserialize_ParenthesesInsteadOfBraces_ThrowsFileFormatException()
        {
            const string FileContents = @"[BicycleSerializableModel(""Type"":""Racing"";" +
                @"""WheelsDiameter"":""15"";""Manufacturer"":""abc"";""Model"":""d4443"";""Driver"":" +
                @"PersonSerializableModel(""Name"":""wwww"";""Age"":""30"";);)]";
            string filename = CreateTextFile(FileContents);
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }
        
        [TestMethod]
        public void Deserialize_EmptyFile_ThrowsFileFormatException()
        {
            string filename = CreateTextFile();
            ISerializer serializer = CreateTextSerializer();

            Action actual = () => serializer.Deserialize(filename);

            Assert.ThrowsException<FileFormatException>(actual);
        }
    }
}