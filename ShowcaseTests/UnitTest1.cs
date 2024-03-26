namespace ShowcaseTests {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void Test1() {
            Assert.Pass();
        }

        [Test]
        public void Test2() {
            string a = "testing";
            string aExpectedResult = "esting";
            string expectedResult = "t";

            string result = a.Remove(0,1);

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(a, aExpectedResult);
        }
    }
}